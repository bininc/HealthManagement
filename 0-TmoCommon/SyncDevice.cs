using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using UsbHid;

namespace TmoCommon
{
    /// <summary>
    /// 同步设备对象类
    /// </summary>
    public class SyncDevice
    {
        #region 指令区
        #region 血压计
        byte[] testBS = { 0xFD, 0xFD, 0x00, 0x00, 0x00, 0x00, 0x0D, 0x0A }; //识别血压计命令
        //  byte[] tureBS = { 0xFD, 0xFD, 0xA5, 0x00, 0x00, 0x00, 0x01, 0xB7, 0x0D, 0x0A }; //识别血压计返回命令
        byte[] startMeasureBS = { 0xFD, 0xFD, 0x05, 0x05, 0x05, 0x05, 0x0D, 0x0A };//开始测量血压命令
        byte[] stopMeasureBS = { 0xFD, 0xFD, 0x05, 0x05, 0x05, 0x05, 0x0D, 0x0A };//停止测量血压命令
        byte[] readMemoryBS = { 0xFD, 0xFD, 0x07, 0x07, 0x07, 0x07, 0x0D, 0x0A };//读取存储数据命令 
        #endregion

        #region 血糖仪
        static string formatCmd = "{{{0}#<checksum>}}";    //命令格式
        string getIdCmd = string.Format(formatCmd, "I");    //取设备ID
        string getVerCmd = string.Format(formatCmd, "V");    //取设备版本号
        string getLarrayCmd = string.Format(formatCmd, "L");    //取记忆数据存储总组数
        string getDvalCmd = string.Format(formatCmd, "D");   //取当前指针指向的测量结果值
        string getPvalCmd = string.Format(formatCmd, "P");      //取测量结果指针命令
        string delK1valCmd = string.Format(formatCmd, "K1");   //删除最先储存测试值命令
        string delKAvalCmd = string.Format(formatCmd, "KA");    //删除所有记忆测试值命令  
        #endregion

        #region 计步器
        byte timeCmd = 0x01;    /*1. 命令：0x5A 0x01 AA BB CC DD EE FF 00 00 00 00 00 00 00 CRC  表示写入PC当前时间到计步器，共16bytes
                                  2. 命令：0x5B 0x01 00 00 00 00 00 00 00 00 00 00 00 00 00 CRC 表示读取计步器当前时间*/
        byte userinfoCmd = 0x02;/*3. 命令：0x5A 0x02 AA BB CC DD EE FF GG HH 00 00 00 00 00 CRC 表示向计步器写入
                                        AA:性别（0表示女性，1表示男性），BB:年龄，CC:身高，DD:体重，EE:步长，FF和GG和HH为目标设定值，高字节在前；
                                        注：目标FF，GG，HH的高位FF的BIT7，BIT6作为目标类型 
                                        BIT7 BIT6= 00时，表示目标类型为步数 
                                        BIT7 BIT6= 01时，表示目标类型为距离 
                                        BIT7 BIT6= 10 时，表示目标类型为卡路里
                                 4.命令: 0x5B 0x02 00 00 00 00 00 00 00 00 00 00 00 00 00 CRC表示从下位机读取性别，年龄，身高，体重，步长，和目标设定值；
                                        下位机在下一步会将应答数据包发给上位机，有效载荷共14个Bytes数据是：XX YY ZZ AA BB CC DD EE ID6 ID5 ID4 ID3 ID2 ID1 
                                        其中XX表示性别（0表示女性，1表示男性），YY表示年龄（5-120），ZZ表示身高（单位cm，1-250），AA表示体重（KG，20-130），
                                        BB表示步长（cm，30-150），CC和DD和EE表示目标设定，高字节在前（范围为1-99999），IDx为6个Bytes的ID码，高字节在前；
                                        注：目标CC，DD，EE的高位CC的BIT7，BIT6作为目标类型
                                        BIT7 BIT6= 00时，表示目标类型为步数
                                        BIT7 BIT6= 01时，表示目标类型为距离
                                        BIT7 BIT6= 10 时，表示目标类型为卡路里（/时间FOR JC-J049）*/
        byte appdataCmd = 0x03;/*5. 命令： 0x5B 0x03 AA 00 00 00 00 00 00 00 00 00 00 00 00 CRC 表示读取下位机的应用数据；
                                    * AA表示具体哪一天的数据，0表示当天，1表示一天前的数据，2表示两天前的数据，依次类推，.....29表示第二十九天前的数据，总共支持三十天；
                                    下位机发送回来的应答数据包跟前面定义不一样，下位机反馈回来的数据格式为：
                                    前8个字节为控制信息，分别为AA BB CC DD EE FF GG HH；HH为前面7个字节的CRC校验；如果AA为0xD4，则表示刚才发送下去的命令CRC出错，命令操作无效，下位机只发送这8个字节的控制信息上来，后面没有其他数据发送上来；
                                *   如果AA为0x2D，则表示刚才的命令正确收到，这时候BB CC DD EE FF GG如下定义：BB，CC，DD分别是该天对应的年月日，EE，FF，GG保留，保留；
                                *   第九个字节开始为下面的数据；应用数据为：时间00:15 的数据6个BYTE,00:30的数据6个BYTE，00:45的数据6个BYTE。。。。。。。。01:00的数据6个BYTE，01:15的数据6个byte依次到23:45 的数据6个BYTE，00:00 6个BYTE算上一天的，（因为数据是23:50到00:00 的数据）
                                *   ，总共有24*4*6=576个字节。应用数据后面还有8个字节的应答信息，前7个字节为0，最后一个字节为CRC字节，是所有的应用数据加上该7个字节的校验。
                                    所以总共数据为：8+576+8=592个字节，分74次发送上来；
                                     6个BYTE分别表示：卡路里（2Bytes，高字节在前） 步数（2Bytes，高字节在前） 距离（2Bytes，高字节在前）
                                    发送命令发送或者读取超过0.5秒，表示传输失败，这时需要重新发送命令给计步器
                                */
        byte deldataCmd = 0x40; /*6.命令： 0x5A 0x40 AA 00 00 00 00 00 00 00 00 00 00 00 00 CRC, 删除指令，AA 表示几天前的数据，0表示当天，1到60表示其他天；
                                        如果CRC校验没错并且成功删除数据，则反馈信息字节为0x2D，如果CRC校验出错，反馈信息字节为0xD4上来PC端，如果CRC正确但是删除数据错误，则反馈信息字节为0xB2；*/
        byte setIdCmd = 0x88;/*7.命令：0x5A 0x88 00 00 ID6 ID5 ID4 ID3 ID2 ID1 00 00 00 00 00 CRC表示设置ID码；
                                    6个Bytes的ID码排列是高字节在前；下位机收到命令后发送应答数据包给上位机。如果CRC校验没错，则反馈信息字节为0x2D，如果CRC校验出错，则反馈信息字节为0xD4*/
        byte chekdataCmd = 0x41;/*8.检测那一天有数据的命令：0x5B 0x41 00 00 00 00 00 00 00 00 00 00 00 00 00 CRC,
                                        这时候应答数据包的有效载荷前8个字节为一个64位的数，高字节在前；该值有64位，bit63和bit61保留，bit60到bit0表示有效信息，bit0表示当天；bit1表示1天前……，bit29表示29天前，
                                        如果某一位为0表示该一天没数据，如果为1表示该天有数据 00100000  11111111 10101010 11100000 */
        byte readdaydata1Cmd = 0x77;/*9.命令0x5B 0x77 0x01 AA 00 00 00 00 00 00 00 00 00 00 00 00 00 CRC为读取某天
                                        （AA为0表示当天，为10表示10天前）显示的总信息的第一部分；
                                        1，前3个为总步数，高字节在前；
                                        2，接着3个字节为跑步步数/有氧步数，高字节在前；
                                        3，接着3个字节表示消耗的卡路里，由于卡路里是非整数，卡路里3个BYTE,取前2个BYTE的16进制转为十进制后除以10得到的数值显示出来（也就是在目前计算结果除以10），第三位改为保留，保留小数点1位
                                        4，
                                        5，接着3个字节是步行距离，高字节在前；
                                        6，接着2个字节是运动时间，例如如果两个字节为12 14，则表示为18:20，也就是18小时20分*/
        byte readdaydata2Cmd = 0x77;/*10.命令0x5B 0x77 0x02 AA 00 00 00 00 00 00 00 00 00 00 00 00 00 CRC为读取某天（AA为0表示当天，为10表示10天前）
                                     * 显示的总信息的第二部分；14个字节的有效载荷数据定义如下：
                                        1，第1个字节为目标达成率，为0到100
                                        2，接着2个字节是运动速度，高字节在前
                                        3，接着3个BYTE是EX，十六进制存储，转为十进制后除以10得到的数显示，保留1位小数点
                                        4，最后8个字节保留，为0；*/
        byte setsleepCmd = 0x99;    /*11.命令：0x5A 0x99 AA 00 00 00 00 00 00 00 00 00 00 00 00 00 CRC表示设置休眠；AA为是否休眠的数据；
                                            其中AA=0，表示不休眠，否则表示当拔开USB后进入休眠状态
                                            界面中要显示的运动数据如下：
                                            - 总步数： Step                 3bytes
                                            - 跑步步数： Step               3bytes
                                            - 消耗卡路里： Kcal             3bytes
                                            - 步行距离： KM                 3bytes
                                            - 运动时间： HH:MM              2bytes
                                            - 每日运动量达成率： %          1bytes
                                            - 运动速度                      2bytes
                                            - EX                            3BYTE

                                            卡路里是1位小数点，做成前2位为整数，第三个BYTE 为小数点后的数 */
        byte resetdogCmd = 0xA5; /*1.total distance 下位机上传的是3个BYTE，以米为单位,显示的时候请除以1000，保留小数点2位
                                    2.读一天数据的卡路里也是2个BYTE 转成10进制后除以10，保留小数点1位
                                    3.读一天数据的DISTANCE 也是以米为单位，请除以1000，保留小数点2位
                                    4.增加一个目标类型，以步数为目标、距离为目标、卡路里为目标等
                                    5.2013-5-22,增加看门狗复位指令，命令为0x5A 0xA5 00 00 00 00 00 00 00 00 00 00 00 00 00 CRC*/
        byte factorCmd = 0x05;/*2.增加两个输入框和两个按钮SET Factor和GET Factor：
                                    两个输入框分别是 
                                    Walking factor(走路因数)：_____(范围：0.03001到0.10000)
                                    Running Factor(跑步因数)：_____(范围：0.07001到0.20000)
                                    按钮SET按下时，将此2个参数发送到下位机，命令格式如下：
                                    5A 05 AA BB CC DD 00 00 00 00 00 00 00 00 00 CRC
                                    其中AA BB 为Walking factor 的值*100000后得到的整数
                                    其中CC DD 为Running factor 的值*100000后得到的整数
 
                                    按钮GET按下时，将读取下位机数据后显示在以上两个方框中，命令格式如下：
                                    5B 05 AA BB CC DD 00 00 00 00 00 00 00 00 00 CRC
                                    其中AA BB 为Walking factor 的值*100000后得到的整数
                                    其中CC DD 为Running factor 的值*100000后得到的整数*/
        #endregion

        #endregion

        Thread th_Analysdata = null; //分析收到数据线程
        bool disposed = false;  //是否释放资源
        /// <summary>
        /// 构造函数
        /// </summary>
        public SyncDevice()
        {
            SyncDeviceTool.listDevices.Add(this);
            this.insertTime = DateTime.Now;
            this.state = SyncDeviceState.Scaned;
            th_Analysdata = new Thread(AnalysisData) { Name = "th_Analysdata", IsBackground = true };
            StateChanged();  //串口改变调用
            th_Analysdata.Start();
        }
        public SyncDevice(SerialPort port)
            : this()
        {
            this.Port = port;
            this.Port.RtsEnable = true;
            this.Port.ReadTimeout = 3000;
            this.Port.Disposed += Port_Disposed;
            this.Port.ErrorReceived += Port_ErrorReceived;
            this.Port.DataReceived += Port_DataReceived;
        }
        public SyncDevice(UsbHidDevice hidDev)
            : this()
        {
            this.HidDev = hidDev;
            this.HidDev.DataReceived += hid_DataReceived;
        }


        /// <summary>
        /// 串口
        /// </summary>
        public SerialPort Port;
        private SyncDeviceType _devType = SyncDeviceType.Unknown;
        /// <summary>
        /// 设备类型
        /// </summary>
        public SyncDeviceType deviceType
        {
            get { return _devType; }
            set
            {
                _devType = value;
                DataTable dt = new DataTable("receiveDataTable");
                DataColumn dcisnormal = new DataColumn("mt_isnormal");
                dcisnormal.DefaultValue = 1;//默认正常
                dt.Columns.AddRange(new DataColumn[] { new DataColumn("user_id"), dcisnormal, new DataColumn("mt_name"), new DataColumn("mt_time"),
                                                        new DataColumn("mt_timestamp"), new DataColumn("dev_user"), new DataColumn("mt_value") });
                receivedDataTable = dt;
            }
        }
        /// <summary>
        /// 串口名字
        /// </summary>
        public string portName
        {
            get
            {
                if (IsHidDev)
                    return "HID";
                else
                    if (Port != null)
                        return Port.PortName;
                    else
                        return null;
            }
        }
        /// <summary>
        /// 是否正在发送命令
        /// </summary>
        public bool SendingCmd = false;
        /// <summary>
        /// 是否正在接收命令
        /// </summary>
        public bool RecingCmd = false;

        /// <summary>
        /// 设备名称
        /// </summary>
        public string deviceName
        {
            get { return TmoShare.GetDescription(deviceType); }
        }
        /// <summary>
        /// 接收到的命令包
        /// </summary>
        public Queue<byte> receivedCmd = new Queue<byte>();
        /// <summary>
        /// 设备插入时间
        /// </summary>
        public DateTime insertTime = DateTime.Now;
        /// <summary>
        /// 上次命令发送时间
        /// </summary>
        public DateTime lastSendTime = DateTime.MinValue;
        /// <summary>
        /// HID设备专用 上次发送命令
        /// </summary>
        byte lastSendCmd = 0;
        /// <summary>
        /// 上次命令状态  -1状态未知 0成功 1失败
        /// </summary>
        int lastCmdState = -1;
        /// <summary>
        /// 上次接收命令时间
        /// </summary>
        public DateTime lastRecTime = DateTime.Now;
        /// <summary>
        /// 解析命令是临时存储
        /// </summary>
        public List<byte> tempCmd = new List<byte>();
        /// <summary>
        /// 接收到的测量结果
        /// </summary>
        public List<List<byte>> receivedData = new List<List<byte>>();
        /// <summary>
        /// 接收到数据的表
        /// </summary>
        public DataTable receivedDataTable = null;
        /// <summary>
        /// 是否收到数据
        /// </summary>
        public bool isRecData
        {
            get { return receivedData.Count > 0; }
        }
        /// <summary>
        /// 接收数据已经完成
        /// </summary>
        public bool isRecDataEnd = false;
        /// <summary>
        /// 读取指针命令是否已发送
        /// </summary>
        public bool isReadPSend = false;
        /// <summary>
        /// 是否读取下一条（血糖仪专用）
        /// </summary>
        public bool isReadPEnd = true;
        /// <summary>
        /// 读取命令是否已发送
        /// </summary>
        public bool isReadSend = false;
        /// <summary>
        /// 本条命令是否已读取完毕
        /// </summary>
        public bool isReadEnd = true;
        /// <summary>
        /// 串口当前状态
        /// </summary>
        public SyncDeviceState state = SyncDeviceState.Unknown;
        /// <summary>
        /// 串口状态文本
        /// </summary>
        public string deviceState
        {
            get
            {
                return TmoShare.GetDescription(state);
            }
        }

        private SyncDeviceSyncState _syncstate = SyncDeviceSyncState.UnSync;    //默认未同步
        /// <summary>
        /// 同步状态
        /// </summary>
        public SyncDeviceSyncState syncState
        {
            get { return _syncstate; }
            set
            {
                _syncstate = value;
            }
        }
        /// <summary>
        ///  同步状态文本
        /// </summary>
        public string syncStateText
        {
            get { return TmoShare.GetDescription(_syncstate); }
        }
        /// <summary>
        /// 操作显示文本
        /// </summary>
        public string operateText
        {
            get
            {
                if (syncState == SyncDeviceSyncState.UnSync && state == SyncDeviceState.Ready)
                    return "点击同步";
                else
                    return " ";
            }
        }

        /// <summary>
        /// 是否是Hid设备
        /// </summary>
        public bool IsHidDev
        {
            get { return Port == null && !string.IsNullOrEmpty(HidDevParh); }
        }
        /// <summary>
        /// Hid设备类
        /// </summary>
        public UsbHidDevice HidDev;
        /// <summary>
        /// hid设备路径
        /// </summary>
        public string HidDevParh { get { if (HidDev != null) return HidDev.DevicePath; else return null; } }
        /// <summary>
        /// 计步器收到数据
        /// </summary>
        /// <param name="data"></param>
        void hid_DataReceived(byte[] data)
        {
            if (data != null)
            {
                if (this.deviceType == SyncDeviceType.Unknown)
                {
                    this.state = SyncDeviceState.Ready;
                    this.deviceType = SyncDeviceType.JBQ;
                    StateChanged();
                }

                for (int i = 0; i < data.Length; i++)
                {
                    receivedCmd.Enqueue(data[i]);
                }
                lastRecTime = DateTime.Now;
            }
        }
        /// <summary>
        /// 状态改变
        /// </summary>
        private void StateChanged()
        {
            SyncDeviceTool.InvokeSyncDeviceChanged(this);
        }
        //上一次确认设备类型命令
        private SyncDeviceType lastTryMsgType = SyncDeviceType.Unknown;
        /// <summary>
        /// 发送设备确认消息
        /// </summary>
        public void SendTrueDevMesage()
        {
            if ((DateTime.Now - lastSendTime).TotalSeconds >= 3)  //3秒发送一次设备探测数据
            {
                if (lastTryMsgType == SyncDeviceType.Unknown || lastTryMsgType == SyncDeviceType.ALKBP)
                {
                    SendCommond(timeCmd);  //发送计步器探测命令
                    lastTryMsgType = SyncDeviceType.JBQ;
                }
                else if (lastTryMsgType == SyncDeviceType.JBQ)
                {
                    SendCommond(getVerCmd); //发送血糖仪探测命令
                    lastTryMsgType = SyncDeviceType.ALKBG;
                }
                else if (lastTryMsgType == SyncDeviceType.ALKBG)
                {
                    SendCommond(testBS);   //发送血压计探测命令
                    lastTryMsgType = SyncDeviceType.ALKBP;
                }
            }
        }
        /// <summary>
        /// 端口释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Port_Disposed(object sender, EventArgs e)
        {
            Port = null;
            Close();
        }
        /// <summary>
        /// 关闭对象清理资源
        /// </summary>
        public void Close()
        {
            disposed = true;
            if (th_Analysdata != null)  //终止线程
            {
                if (th_Analysdata.ThreadState != System.Threading.ThreadState.Unstarted)
                {
                    th_Analysdata.Abort();
                    th_Analysdata.Join();
                }
                th_Analysdata = null;
            }
            if (Port != null)
                Port.Close();
            if (HidDev != null)
                HidDev.Dispose();

            this.state = SyncDeviceState.Removed;
            StateChanged();
            SyncDeviceTool.listDevices.Remove(this);//移除串口集合
        }
        /// <summary>
        /// 端口错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            ;
            if (!SyncDeviceTool.errPortName.ContainsKey(Port.PortName))
                SyncDeviceTool.errPortName.Add(Port.PortName, 0);
            else
            {
                SyncDeviceTool.errPortName[Port.PortName] += 1;
                if (SyncDeviceTool.errPortName[Port.PortName] > 5)
                    UserMessageBox.MessageError("设备出错，请尝试重新插拔！");
            }
            Port.Close();
            //关闭串口 
        }

        /// <summary>
        /// 接收到数据事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int count = Port.BytesToRead;
            for (int i = 0; i < count; i++)
            {
                byte data = (byte)Port.ReadByte();
                receivedCmd.Enqueue(data);
            }
            lastRecTime = DateTime.Now;
        }

        /// <summary>
        /// 向血压计发送命令
        /// </summary>
        /// <param name="bsSend">命令字节数组</param>
        /// <param name="interval">每个字节之间发送间隔（毫秒）</param>
        public void SendCommond(byte[] bsSend, int interval = 65)
        {
            if (bsSend == null || bsSend.Length == 0) return;
            if (Port == null) return;

            Thread th = new Thread(() =>
            {
                if (!SendingCmd) //防止同一端口多次 写入数据
                {
                    SendingCmd = true;
                    try
                    {
                        if ((DateTime.Now - lastSendTime).TotalSeconds >= 2) //两条命令间隔不小于2s
                            for (int i = 0; i < bsSend.Length; i++)
                            {
                                Port.Write(new byte[] { bsSend[i] }, 0, 1);
                                Thread.Sleep(interval);
                            }
                    }
                    catch { }
                    lastSendTime = DateTime.Now;
                    SendingCmd = false;
                }
            });
            th.Name = "th_SendALKCommond_" + Port.PortName;
            th.IsBackground = true;
            isRecDataEnd = false;
            th.Start();
            Thread.Sleep(500);
        }

        /// <summary>
        /// 向血糖仪发送命令
        /// </summary>
        /// <param name="sendCmd">命令字符串</param>
        /// <param name="interval">每个字节之间发送间隔（毫秒）</param>
        public void SendCommond(string sendCmd, int interval = 30)
        {
            if (string.IsNullOrWhiteSpace(sendCmd)) return;
            if (Port == null) return;

            List<byte> lbsSend = new List<byte>();
            sendCmd = sendCmd.Replace("<checksum>", "@");
            string cmd = sendCmd.Substring(1, sendCmd.IndexOf('#') - 1);
            foreach (char c in sendCmd.ToArray())
            {      //  string cmd = string.Format(formatCmd, "I", (char)(((byte)'I') ^  ^ ));
                if (c == '@')
                {
                    byte cs = 0;
                    foreach (char i in cmd.ToArray())
                        cs ^= (byte)i;
                    cs = (byte)(cs ^ (byte)'#' ^ 0x80);
                    lbsSend.Add(cs);
                }
                else
                {
                    lbsSend.Add((byte)c);
                }
            }

            byte[] bsSend = lbsSend.ToArray();
            Thread th = new Thread(() =>
            {
                if (!SendingCmd) //防止同一端口多次 写入数据
                {
                    SendingCmd = true;
                    try
                    {
                        if ((DateTime.Now - lastSendTime).TotalMilliseconds > 30)   //两条命令间隔大于30秒
                            Port.Write(bsSend, 0, bsSend.Length);
                    }
                    catch { }
                    lastSendTime = DateTime.Now;
                    SendingCmd = false;
                }
            });
            th.Name = "th_SendALKCommond_" + Port.PortName;
            th.IsBackground = true;
            isRecDataEnd = false;
            th.Start();
            Thread.Sleep(interval);
        }
        /// <summary>
        /// 向几部器发送命令
        /// </summary>
        /// <param name="sendCmd"></param>
        /// <param name="readType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool SendCommond(byte sendCmd, bool readType = true, params byte[] parameters)
        {
            if (HidDev == null) return false;
            try
            {
                lastSendCmd = sendCmd;
                lastCmdState = -1;
                lastSendTime = DateTime.Now;
                if (readType)
                {
                    bool suc = HidDev.SendReadCommandMessage(sendCmd, parameters);
                    return suc;
                }
                else
                {
                    bool suc = HidDev.SendWriteCommandMessage(sendCmd, parameters);
                    lastSendCmd += 1;   //加1 表示写数据
                    return suc;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 解析接收到数数据
        /// </summary>
        void AnalysisData()
        {
            while (!disposed)
            {
                try
                {
                    if (receivedCmd.Count == 0) continue;
                    if (!RecingCmd)
                    {
                        RecingCmd = true;
                        while (receivedCmd.Count > 0)
                        {
                            byte data = receivedCmd.Peek();  //取出一个字节

                            if (tempCmd.Count == 0)  //上条命令接收完整
                            {
                                if (data == 0xFD)  //血压计头码
                                {
                                    try
                                    {
                                        for (int i = 0; i < 10; i++) //正常返回数据格式为10个字节
                                        {
                                            data = receivedCmd.Dequeue();
                                            tempCmd.Add(data);
                                        }
                                    }
                                    catch
                                    {   //队列中的数据不够10个
                                        continue;
                                    }
                                }
                                else if (data == 0x7b) //血糖仪头码
                                {
                                    try
                                    {
                                        do
                                        {
                                            data = receivedCmd.Dequeue();
                                            tempCmd.Add(data);
                                        } while (data != 0x7d);
                                    }
                                    catch
                                    {   //命令不完整
                                        continue;
                                    }
                                }
                                else if (IsHidDev)  //计步器头码
                                {
                                    try
                                    {
                                        if (lastSendCmd != appdataCmd)
                                        {
                                            for (int i = 0; i < 16; i++) //正常返回数据格式为16个字节
                                            {
                                                data = receivedCmd.Dequeue();
                                                tempCmd.Add(data);
                                            }
                                        }
                                        else
                                        {
                                            lastCmdState = 2;
                                            int len = receivedCmd.Count;
                                            for (int i = 0; i < len; i++)
                                            {
                                                data = receivedCmd.Dequeue();
                                                tempCmd.Add(data);
                                            }
                                            if (tempCmd.Count != 784)
                                                throw new Exception("应用数据不全");
                                        }
                                    }
                                    catch
                                    {   //队列中的数据不够16个
                                        continue;
                                    }
                                }
                                else
                                {
                                    //非头码丢弃
                                    receivedCmd.Dequeue();
                                    continue;
                                }
                            }
                            else
                            {
                                data = tempCmd[0];
                                //上条命令不完整
                                if (data == 0xFD)
                                {
                                    try
                                    {
                                        for (int i = tempCmd.Count; i < 10; i++) //正常返回数据格式为10个字节
                                        {
                                            data = receivedCmd.Dequeue();
                                            tempCmd.Add(data);
                                        }
                                    }
                                    catch
                                    {   //队列中的数据不够10个
                                        continue;
                                    }
                                }
                                else if (data == 0x7b)
                                {
                                    try
                                    {
                                        do
                                        {
                                            data = receivedCmd.Dequeue();
                                            tempCmd.Add(data);
                                        } while (data != 0x7d);
                                    }
                                    catch
                                    {   //命令不完整
                                        continue;
                                    }
                                }
                                else if (IsHidDev)  //计步器头码
                                {
                                    try
                                    {
                                        if (lastSendCmd != appdataCmd)
                                        {
                                            for (int i = tempCmd.Count; i < 16; i++) //正常返回数据格式为16个字节
                                            {
                                                data = receivedCmd.Dequeue();
                                                tempCmd.Add(data);
                                            }
                                        }
                                        else
                                        {
                                            lastCmdState = 2;
                                            int len = receivedCmd.Count;
                                            for (int i = 0; i < len; i++)
                                            {
                                                data = receivedCmd.Dequeue();
                                                tempCmd.Add(data);
                                            }
                                            if (tempCmd.Count != 784)
                                                throw new Exception("应用数据不全");
                                        }
                                    }
                                    catch
                                    {   //队列中的数据不够16个
                                        continue;
                                    }
                                }
                                else
                                {
                                    receivedCmd.Dequeue();   //不支持的命令抛弃
                                    tempCmd.Clear();
                                    continue;
                                }
                            }


                            if (tempCmd[0] == 0xFD && data == 0x0A)   //尾码
                            {
                                if (tempCmd[0] == 0xFD && tempCmd[1] == 0xFD && tempCmd[8] == 0x0D && tempCmd[9] == 0x0A)   //识别为血压计
                                {
                                    if (state == SyncDeviceState.Scaned)  //未识别
                                    {
                                        state = SyncDeviceState.Ready;
                                        deviceType = SyncDeviceType.ALKBP;
                                        StateChanged();  //通知改变
                                    }
                                    else
                                    {
                                        if (tempCmd[2] == 0x5B || tempCmd[2] == 0x5D || tempCmd[2] == 0x5C || tempCmd[2] == 0x5E) //测量数据和时间
                                        {
                                            receivedData.Add(tempCmd.ToList());
                                        }
                                    }
                                    if (tempCmd[2] == 0xA5)
                                    {
                                        isRecDataEnd = true;
                                        state = SyncDeviceState.Ready;
                                    }
                                }
                            }
                            else if (tempCmd[0] == 0x7b && data == 0x7d) //血糖仪尾码
                            {
                                if (tempCmd.First() == 0x7b && tempCmd.Last() == 0x7d && tempCmd.Contains(0x23))    //识别为血糖仪
                                {
                                    if (state == SyncDeviceState.Scaned)  //未识别
                                    {
                                        state = SyncDeviceState.Ready;
                                        deviceType = SyncDeviceType.ALKBG;
                                        StateChanged();    //通知改变
                                    }
                                    else
                                    {
                                        if (tempCmd[1] == 0x4C) //读取已存储数据个数
                                        {
                                            receivedData.Add(tempCmd.ToList());
                                        }
                                        else if (tempCmd[1] == 0x44) //接收测量数据
                                        {
                                            receivedData.Add(tempCmd.ToList());
                                            isReadEnd = true;
                                        }
                                        else if (tempCmd[1] == (byte)'A' && tempCmd[2] == (byte)'C' && tempCmd[3] == (byte)'K')    //取下一条指针成功
                                        {
                                            isReadPEnd = true;
                                        }
                                        else if (tempCmd[1] == (byte)'N' && tempCmd[2] == (byte)'A' && tempCmd[3] == (byte)'K') //结束存储
                                        {
                                            isRecDataEnd = true;
                                            state = SyncDeviceState.Ready;
                                        }
                                    }
                                }
                            }
                            else if (IsHidDev)    //识别为计步器
                            {
                                byte sum = 0;
                                byte crc = 0;
                                if (lastSendCmd != appdataCmd)
                                {
                                    for (int i = 0; i < tempCmd.Count - 1; i++)
                                    {
                                        sum += tempCmd[i];
                                    }
                                    crc = (byte)(sum & 0xFF);
                                }
                                else
                                {
                                    sum = 0;
                                    crc = 0;
                                    for (int i = 8; i < 783; i++)
                                    {
                                        sum += tempCmd[i];
                                    }
                                    crc = (byte)(sum & 0xFF);
                                }
                                if (crc == tempCmd.LastOrDefault())    //crc正确
                                {
                                    if (tempCmd[0] == 0x2D) //命令执行成功
                                    {
                                        receivedData.Add(tempCmd.ToList());
                                        lastCmdState = 0;
                                    }
                                    else
                                    {
                                        lastCmdState = 1;
                                    }

                                    if (lastSendCmd == timeCmd)   //上一次是读取时间命令
                                    {
                                        //同步计步器时间
                                        SendCommond(timeCmd, false, (byte)(DateTime.Now.Year % 100), (byte)DateTime.Now.Month, (byte)DateTime.Now.Day, (byte)DateTime.Now.Hour, (byte)DateTime.Now.Minute, (byte)DateTime.Now.Second);
                                    }
                                }
                                else
                                    lastCmdState = 1;
                            }
                            tempCmd.Clear();//丢弃解析过的命令
                        }
                    }
                    RecingCmd = false;

                }
                catch { }
                finally
                {
                    Thread.Sleep(50);
                }
            }
        }

        /// <summary>
        /// 开始同步数据
        /// </summary>
        public void SyncData()
        {
            if (deviceType == SyncDeviceType.Unknown) return;

            syncState = SyncDeviceSyncState.ReadData; //读取数据
            state = SyncDeviceState.Busyed;
            StateChanged();

            switch (deviceType)
            {
                case SyncDeviceType.ALKBP:    //血压计
                    ReadSyncDataALKBP();
                    break;
                case SyncDeviceType.ALKBG:   //血糖仪
                    ReadSyncDataALKBG();
                    break;
                case SyncDeviceType.JBQ:    //计步器
                    ReadSyncDataJBQ();
                    break;
            }

            #region 同步数据
            syncState = SyncDeviceSyncState.SyncData; //同步数据
            StateChanged();
            string submitXml = TmoShare.getXMLFromDataTable(receivedDataTable);   //转化成xml
            submitXml = StringPlus.CompressString(submitXml);   //压缩字符串
            bool subSuccess = SyncDeviceTool.InvokeSubmitDataMethod(this, submitXml);
            #endregion

            #region 同步完成
            if (subSuccess)
            {
                syncState = SyncDeviceSyncState.Synced;   //同步完成
                state = SyncDeviceState.Commpleted;
                StateChanged();
            }
            else
            {
                syncState = SyncDeviceSyncState.SyncFailed;
                state = SyncDeviceState.Commpleted;
                StateChanged();
                throw new Exception("同步失败！");
            }
            #endregion
        }

        /// <summary>
        /// 读取爱立康血压计 同步数据
        /// </summary>
        public void ReadSyncDataALKBP()
        {
            #region 读取设备数据

            do
            {
                receivedData.Clear();
                receivedDataTable.Clear();
                SendCommond(readMemoryBS);
                Thread.Sleep(5000);
            } while (!isRecData);
            while (!isRecDataEnd)
            {
                if ((DateTime.Now - lastRecTime).TotalSeconds > 20)   //没收到返回码20秒后 结束等待
                {
                    state = SyncDeviceState.Ready;
                    StateChanged();
                    break;
                }
                Thread.Sleep(1000);
            }
            #endregion

            #region 处理数据
            syncState = SyncDeviceSyncState.DealData;//处理数据 
            StateChanged();
            if (isRecData)   //读取到数据
            {
                //设置登录ID默认值
                receivedDataTable.Columns["user_id"].DefaultValue = TmoComm.login_userInfo.user_id;
                byte lastFlags = 0;
                receivedData.ForEach(x =>
                {
                    if (x[2] == 0x5B || x[2] == 0x5D)   //数据指令
                    {
                        //解析数据 
                        string dev_user = x[2].ToString("X2");
                        int sbp = x[3]; //收缩压
                        DataRow dr_sbp = receivedDataTable.NewRow();
                        dr_sbp["mt_name"] = "sbp";
                        dr_sbp["mt_value"] = sbp;
                        dr_sbp["dev_user"] = dev_user;
                        receivedDataTable.Rows.Add(dr_sbp);
                        int dbp = x[4];   //舒张压
                        DataRow dr_dbp = receivedDataTable.NewRow();
                        dr_dbp["mt_name"] = "dbp";
                        dr_dbp["mt_value"] = dbp;
                        dr_dbp["dev_user"] = dev_user;
                        receivedDataTable.Rows.Add(dr_dbp);
                        int hr = x[5];  //心率
                        int hr_buqi = x[6] == 0xAA ? 0 : 1;    //是否心率不齐
                        DataRow dr_hr = receivedDataTable.NewRow();
                        dr_hr["mt_name"] = "hr";
                        dr_hr["mt_value"] = hr;
                        dr_hr["mt_isnormal"] = hr_buqi;
                        dr_hr["dev_user"] = dev_user;
                        receivedDataTable.Rows.Add(dr_hr);
                    }
                    else if (x[2] == 0x5C || x[2] == 0x5E)
                    {   //解析时间 
                        string dev_user = x[2] == 0x5C ? "5B" : "5D";
                        string ym = x[3].ToString("X2");
                        int year = Convert.ToByte(("0x" + ym[0]), 16) + 2013;
                        int month = Convert.ToByte(("0x" + ym[1]), 16);
                        int day = x[4];
                        int hour = x[5];
                        int minute = x[6];
                        DateTime mt_time = Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}:{4}:00", year, month, day, hour, minute));
                        string timesatmp = TmoShare.DateTime2TimeStamp(mt_time).ToString();

                        int rowCount = receivedDataTable.Rows.Count;
                        if (rowCount >= 3)
                        {
                            for (int i = rowCount - 1; i >= rowCount - 3; i--)
                            {
                                if (receivedDataTable.Rows[i]["dev_user"].ToString() == dev_user)
                                {
                                    receivedDataTable.Rows[i]["mt_time"] = mt_time;
                                    receivedDataTable.Rows[i]["mt_timestamp"] = timesatmp;
                                }
                            }

                        }
                    }
                    lastFlags = x[2];
                });
            }
            #endregion

            #region 优化数据 错误数据处理
            syncState = SyncDeviceSyncState.OptData;  //优化数据
            StateChanged();
            receivedDataTable.DefaultView.Sort = "dev_user,mt_time";  //进行排序
            receivedDataTable = receivedDataTable.DefaultView.ToTable();
            for (int i = 0; i < receivedDataTable.Rows.Count; i++)
            {
                DataRow dr = receivedDataTable.Rows[i];
                if (dr["mt_time"] == null || string.IsNullOrWhiteSpace(dr["mt_time"].ToString()))
                {

                }
            }
            #endregion
        }

        /// <summary>
        /// 读取爱立康血糖仪 同步数据
        /// </summary>
        public void ReadSyncDataALKBG()
        {
            #region 读取设备数据
            do
            {
                receivedData.Clear();
                receivedDataTable.Clear();
                isReadPEnd = isReadEnd = true;
                isReadSend = false;
                SendCommond(getLarrayCmd);
                Thread.Sleep(5000);
            } while (!isRecData); //
            while (!isRecDataEnd) //等待数据读取完
            {
                if (isReadPEnd && isReadEnd && !isReadSend)
                {
                    isReadEnd = false;
                    SendCommond(getDvalCmd);  //读取数据命令
                    isReadSend = true;
                    isReadPSend = false;
                }
                else if (isReadEnd && isReadPEnd && !isReadPSend)
                {
                    isReadPEnd = false;
                    SendCommond(getPvalCmd); //移动到下条数据
                    isReadPSend = true;
                    isReadSend = false;
                }
                else if (isReadPEnd && !isReadEnd)
                {
                    isReadEnd = false;
                    SendCommond(getDvalCmd);  //读取数据命令
                    isReadSend = true;
                    isReadPSend = false;
                }
                else if (isReadEnd && !isReadPEnd)
                {
                    isReadPEnd = false;
                    SendCommond(getPvalCmd); //移动到下条数据
                    isReadPSend = true;
                    isReadSend = false;
                }

                if ((DateTime.Now - lastRecTime).TotalSeconds > 20)   //没收到返回码20秒后 结束等待
                {
                    state = SyncDeviceState.Ready;
                    StateChanged();
                    break;
                }

                Thread.Sleep(970);
            }
            #endregion

            #region 处理数据
            syncState = SyncDeviceSyncState.DealData;//处理数据 
            StateChanged();
            if (isRecData)   //读取到数据
            {
                //设置登录ID默认值
                receivedDataTable.Columns["user_id"].DefaultValue = TmoComm.login_userInfo.user_id;
                List<byte> tmpCmd = receivedData.First();
                if (tmpCmd[1] == 0x4C)
                {
                    int count = int.Parse(new string(new char[] { (char)tmpCmd[2], (char)tmpCmd[3], (char)tmpCmd[4] }));
                    receivedData.RemoveAt(0);
                }
                receivedData.ForEach(x =>
                {
                    if (x[1] == 0x44)   //数据指令
                    {
                        //解析数据 
                        int val = int.Parse(new string(new char[] { (char)x[2], (char)x[3], (char)x[4], (char)x[5] }));
                        string bg = Math.Round(val / (double)18, 2).ToString("f2"); //血糖
                        bg = bg.Remove(bg.IndexOf('.') + 2);

                        string year = "20" + new string(new char[] { (char)x[11], (char)x[12] });
                        string month = new string(new char[] { (char)x[13], (char)x[14] });
                        string day = new string(new char[] { (char)x[15], (char)x[16] });
                        string hour = new string(new char[] { (char)x[17], (char)x[18] });
                        string minute = new string(new char[] { (char)x[19], (char)x[20] });
                        DateTime mt_time = DateTime.Now;
                        bool suc = DateTime.TryParse(string.Format("{0}-{1}-{2} {3}:{4}:00", year, month, day, hour, minute), out mt_time);
                        if (!suc)
                        {
                            mt_time = Convert.ToDateTime("2000-01-01 00:00:00");
                        }

                        string timesatmp = TmoShare.DateTime2TimeStamp(mt_time).ToString();

                        DataRow dr_bg = receivedDataTable.NewRow();
                        dr_bg["mt_name"] = "bg";
                        dr_bg["mt_value"] = bg;
                        dr_bg["mt_time"] = mt_time;
                        dr_bg["mt_timestamp"] = timesatmp;
                        receivedDataTable.Rows.Add(dr_bg);
                    }
                });
            }
            #endregion

            #region 优化数据 错误数据处理
            syncState = SyncDeviceSyncState.OptData;  //优化数据
            StateChanged();
            receivedDataTable.DefaultView.Sort = "mt_time";  //进行排序
            receivedDataTable = receivedDataTable.DefaultView.ToTable(true);
            #endregion
        }
        /// <summary>
        /// 读取计步器同步数据
        /// </summary>
        public void ReadSyncDataJBQ()
        {
            #region 读取设备数据
            do
            {
                TmoShare.WriteLog("开始读取计步器可用数据");
                receivedData.Clear();
                receivedDataTable.Clear();
                bool suc = SendCommond(chekdataCmd);
                if (suc)
                    TmoShare.WriteLog("可用数据命令 发送成功");
                else
                {
                    TmoShare.WriteLog("可用数据命令 发送失败");
                }
                Thread.Sleep(5000);
            } while (!isRecData);
            string chekdataStr = null;
            int dayIndex = -1;
            Dictionary<int, DateTime> mt_times = new Dictionary<int, DateTime>();
            while (!isRecDataEnd) //等待数据读取完
            {
                if (lastSendCmd == chekdataCmd && chekdataStr == null)
                {
                    if (lastCmdState == 0)
                    { //读取有数据的天 
                        chekdataStr = null;
                        //Int64 n64 = BitConverter.ToInt64(receivedData.FirstOrDefault().ToArray(), 1);
                        //chekdataStr = Convert.ToString(n64, 2);
                        for (int i = 4; i > 0; i--)
                        {
                            string jizhi = Convert.ToString(receivedData.FirstOrDefault()[i], 2);
                            //string jizhi = Convert.ToString(255, 2);   //现在默认读取全部
                            while (jizhi.Length < 8)
                            {
                                jizhi = "0" + jizhi;
                            }
                            chekdataStr += new string(jizhi.Reverse().ToArray());
                        }
                        for (int i = 8; i > 4; i--)
                        {
                            string jizhi = Convert.ToString(receivedData.FirstOrDefault()[i], 2);
                            //string jizhi = Convert.ToString(255, 2);   //现在默认读取全部
                            while (jizhi.Length < 8)
                            {
                                jizhi = "0" + jizhi;
                            }
                            chekdataStr += new string(jizhi.Reverse().ToArray());
                        }
                        receivedData.Clear();
                        if (string.IsNullOrWhiteSpace(chekdataStr))
                            lastCmdState = 1;
                    }
                    else if (lastCmdState == 1)
                    { //失败  重发
                        TmoShare.WriteLog("可用数据命令 发送失败 重发");
                        receivedData.Clear();
                        SendCommond(chekdataCmd);
                    }
                }
                else if (lastSendCmd == appdataCmd || lastSendCmd == chekdataCmd)
                {
                    if (lastCmdState == 0)
                    {
                        do
                        {
                            dayIndex++;
                            if (dayIndex >= 15 /*chekdataStr.Length - 3*/) //测试机貌似是支持15天存储的
                            {
                                isRecDataEnd = true;
                                break;
                            }
                        } while (chekdataStr[dayIndex] == '0');
                        if (!isRecDataEnd)
                        {
                            if (!mt_times.ContainsKey(dayIndex))
                                mt_times.Add(dayIndex, DateTime.Now.Date.AddDays(-dayIndex));
                            SendCommond(readdaydata1Cmd, true, 0x01, (byte)dayIndex);
                        }
                    }
                    else if (lastCmdState == 1)
                    {
                        SendCommond(appdataCmd, true, (byte)dayIndex);
                    }
                    else if (lastCmdState == -1)
                    {
                        if ((DateTime.Now - lastSendTime).TotalMilliseconds > 5000)
                        {
                            SendCommond(appdataCmd, true, (byte)dayIndex);
                        }
                    }
                }
                else if (lastSendCmd == readdaydata1Cmd)
                {
                    if (lastCmdState == 0)
                    {
                        SendCommond(appdataCmd, true, (byte)(dayIndex));
                    }
                    else if (lastCmdState == 1)
                    {
                        SendCommond(readdaydata1Cmd, true, 0x01, (byte)dayIndex);
                    }
                    else if (lastCmdState == -1)
                    {
                        if ((DateTime.Now - lastSendTime).TotalMilliseconds > 5000)
                        {
                            SendCommond(readdaydata1Cmd, true, 0x01, (byte)dayIndex);
                        }
                    }
                }

                if (!Debugger.IsAttached && (DateTime.Now - lastSendTime).TotalSeconds > 30)   //没收到返回码30秒后 结束等待
                {
                    state = SyncDeviceState.Ready;
                    StateChanged();
                    break;
                }

                Thread.Sleep(500);
            }
            #endregion

            #region 处理数据
            syncState = SyncDeviceSyncState.DealData;//处理数据 
            StateChanged();
            if (isRecData)   //读取到数据
            {
                receivedDataTable.Clear();
                //设置登录ID默认值
                receivedDataTable.Columns["user_id"].DefaultValue = TmoComm.login_userInfo.user_id;
                for (int i = 0; i < receivedData.Count; i++)
                {
                    List<byte> data = receivedData[i];
                    //解析数据
                    DateTime mt_time = mt_times[i / 2];
                    string timesatmp = TmoShare.DateTime2TimeStamp(mt_time).ToString();
                    if (data.Count == 16)
                    {
                        // if (data[0] == data[15]) continue;  //跳过空数据
                        string totalStep = BitConverter.ToInt32(new byte[] { data[3], data[2], data[1], 0 }, 0).ToString();
                        string runStep = BitConverter.ToInt32(new byte[] { data[6], data[5], data[4], 0 }, 0).ToString();
                        string kcal = (BitConverter.ToInt32(new byte[] { data[8], data[7], 0, 0 }, 0) / (float)10).ToString();
                        string runDistance = Math.Round(BitConverter.ToInt32(new byte[] { data[12], data[11], data[10], 0 }, 0) / (double)1000, 2).ToString();
                        string runTime = (data[13] * 60 + data[14]).ToString();
                        //if (runTime == "0") continue;

                        DataRow dr_totalStep = receivedDataTable.NewRow();
                        dr_totalStep["mt_name"] = "totalStep";
                        dr_totalStep["mt_value"] = totalStep;
                        dr_totalStep["mt_time"] = mt_time;
                        dr_totalStep["mt_timestamp"] = timesatmp;
                        receivedDataTable.Rows.Add(dr_totalStep);

                        DataRow dr_runStep = receivedDataTable.NewRow();
                        dr_runStep["mt_name"] = "runStep";
                        dr_runStep["mt_value"] = runStep;
                        dr_runStep["mt_time"] = mt_time;
                        dr_runStep["mt_timestamp"] = timesatmp;
                        receivedDataTable.Rows.Add(dr_runStep);

                        DataRow dr_kcal = receivedDataTable.NewRow();
                        dr_kcal["mt_name"] = "kcal";
                        dr_kcal["mt_value"] = kcal;
                        dr_kcal["mt_time"] = mt_time;
                        dr_kcal["mt_timestamp"] = timesatmp;
                        receivedDataTable.Rows.Add(dr_kcal);

                        DataRow dr_runDistance = receivedDataTable.NewRow();
                        dr_runDistance["mt_name"] = "runDistance";
                        dr_runDistance["mt_value"] = runDistance;
                        dr_runDistance["mt_time"] = mt_time;
                        dr_runDistance["mt_timestamp"] = timesatmp;
                        receivedDataTable.Rows.Add(dr_runDistance);

                        DataRow dr_runTime = receivedDataTable.NewRow();
                        dr_runTime["mt_name"] = "runTime";
                        dr_runTime["mt_value"] = runTime;
                        dr_runTime["mt_time"] = mt_time;
                        dr_runTime["mt_timestamp"] = timesatmp;
                        receivedDataTable.Rows.Add(dr_runTime);
                    }
                    else
                    {
                        //float ksum = 0;
                        //int stepsum = 0;
                        //double dissum = 0;
                        DateTime c_time = mt_time;
                        List<string> kcal24List = new List<string>();
                        List<string> totalStep24List = new List<string>();
                        List<string> runDistance24List = new List<string>();
                        List<string> sleep24List = new List<string>();
                        for (int j = 8; j < data.Count - 8; j += 8) //每15分钟 8个字节
                        {
                            c_time = c_time.AddMinutes(15);
                            byte bt1 = data[j];
                            string jz1 = Byte2BinStr(bt1);
                            byte bt2 = data[j + 1];
                            string jz2 = Byte2BinStr(bt2);
                            byte bt3 = data[j + 2];
                            string jz3 = Byte2BinStr(bt3);
                            byte bt4 = data[j + 3];
                            string jz4 = Byte2BinStr(bt4);
                            byte bt5 = data[j + 4];
                            string jz5 = Byte2BinStr(bt5);
                            byte bt6 = data[j + 5];
                            string jz6 = Byte2BinStr(bt6);
                            byte bt7 = data[j + 6];
                            string jz7 = Byte2BinStr(bt7);
                            byte bt8 = data[j + 7];
                            string jz8 = Byte2BinStr(bt8);

                            bool sleep = (jz5.FirstOrDefault() == '1' && jz6.FirstOrDefault() == '1');

                            int kcaltmp = BitConverter.ToInt32(new byte[] { bt2, bt1, 0, 0 }, 0);
                            string kcal24 = sleep ? "0" : (kcaltmp / (float)10).ToString();
                            kcal24List.Add(kcal24);
                            //ksum += float.Parse(kcal24);

                            int totalSteptmp = BitConverter.ToInt32(new byte[] { bt4, bt3, 0, 0 }, 0);
                            string totalStep24 = sleep ? "0" : totalSteptmp.ToString();
                            totalStep24List.Add(totalStep24);
                            //stepsum += int.Parse(totalStep24);

                            int runDistancetmp = BitConverter.ToInt32(new byte[] { bt8, bt7, 0, 0 }, 0);
                            string runDistance24 = sleep ? "0" : Math.Round(runDistancetmp / (double)1000, 2).ToString();
                            runDistance24List.Add(runDistance24);
                            //dissum += double.Parse(runDistance24);

                            string m2 = sleep ? Convert.ToInt32(jz1.Substring(1), 2).ToString() : "255";
                            sleep24List.Add(m2);
                            string m4 = sleep ? Convert.ToInt32(jz2.Substring(1), 2).ToString() : "255";
                            sleep24List.Add(m4);
                            string m6 = sleep ? Convert.ToInt32(jz3.Substring(1), 2).ToString() : "255";
                            sleep24List.Add(m6);
                            string m8 = sleep ? Convert.ToInt32(jz4.Substring(1), 2).ToString() : "255";
                            sleep24List.Add(m8);
                            string m10 = sleep ? Convert.ToInt32(jz5.Substring(1), 2).ToString() : "255";
                            sleep24List.Add(m10);
                            string m12 = sleep ? Convert.ToInt32(jz6.Substring(1), 2).ToString() : "255";
                            sleep24List.Add(m12);
                            string m14 = sleep ? Convert.ToInt32(jz7.Substring(1), 2).ToString() : "255";
                            sleep24List.Add(m14);

                            if (c_time.Minute % 2 == 0)
                            {
                                string m16 = sleep ? Convert.ToInt32(jz8.Substring(1), 2).ToString() : "255";
                                sleep24List.Add(m16);
                            }
                        }

                        DataRow dr_kcal24 = receivedDataTable.NewRow();
                        dr_kcal24["mt_name"] = "kcal24";
                        dr_kcal24["mt_value"] = StringPlus.GetArrayStr(kcal24List, ",");
                        dr_kcal24["mt_time"] = mt_time;
                        dr_kcal24["mt_timestamp"] = timesatmp;
                        receivedDataTable.Rows.Add(dr_kcal24);

                        DataRow dr_totalStep24 = receivedDataTable.NewRow();
                        dr_totalStep24["mt_name"] = "totalStep24";
                        dr_totalStep24["mt_value"] = StringPlus.GetArrayStr(totalStep24List, ",");
                        dr_totalStep24["mt_time"] = mt_time;
                        dr_totalStep24["mt_timestamp"] = timesatmp;
                        receivedDataTable.Rows.Add(dr_totalStep24);

                        DataRow dr_runDistance24 = receivedDataTable.NewRow();
                        dr_runDistance24["mt_name"] = "runDistance24";
                        dr_runDistance24["mt_value"] = StringPlus.GetArrayStr(runDistance24List, ",");
                        dr_runDistance24["mt_time"] = mt_time;
                        dr_runDistance24["mt_timestamp"] = timesatmp;
                        receivedDataTable.Rows.Add(dr_runDistance24);

                        DataRow dr_sleep24 = receivedDataTable.NewRow();
                        dr_sleep24["mt_name"] = "sleep24";
                        dr_sleep24["mt_value"] = StringPlus.GetArrayStr(sleep24List, ",");
                        dr_sleep24["mt_time"] = mt_time;
                        dr_sleep24["mt_timestamp"] = timesatmp;
                        receivedDataTable.Rows.Add(dr_sleep24);
                    }
                }
            }
            #endregion

            #region 优化数据 错误数据处理
            syncState = SyncDeviceSyncState.OptData;  //优化数据
            StateChanged();
            #endregion
        }

        private string Byte2BinStr(byte b)
        {
            string jizhi = Convert.ToString(b, 2);
            while (jizhi.Length < 8)
            {
                jizhi = "0" + jizhi;
            }
            return jizhi;
        }
    }
}

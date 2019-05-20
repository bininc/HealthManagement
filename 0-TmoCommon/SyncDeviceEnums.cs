
namespace TmoCommon
{

    /// <summary>
    /// 设备状态
    /// </summary>
    public enum SyncDeviceState
    {
        /// <summary>
        /// 默认值 状态未知
        /// </summary>
        [Description("未知状态")]
        Unknown,
        /// <summary>
        /// 已经监测到设备
        /// </summary>
        [Description("正在识别...")]
        Scaned,
        /// <summary>
        /// 已经识别设备
        /// </summary>
        [Description("准备就绪")]
        Ready,
        /// <summary>
        /// 设备正忙
        /// </summary>
        [Description("设备正忙")]
        Busyed,
        /// <summary>
        /// 同步完成
        /// </summary>
        [Description("同步完成")]
        Commpleted,
        /// <summary>
        /// 已经移除设备
        /// </summary>
        [Description("已经断开")]
        Removed
    }
    /// <summary>
    /// 设备类型
    /// </summary>
    public enum SyncDeviceType
    {
        /// <summary>
        /// 未知设备
        /// </summary>
        [Description("未知设备")]
        Unknown,
        /// <summary>
        /// 血压计
        /// </summary>
        [Description("血压计")]
        ALKBP,
        /// <summary>
        /// 血糖仪
        /// </summary>
        [Description("血糖仪")]
        ALKBG,
        /// <summary>
        /// 计步器
        /// </summary>
        [Description("运动监测仪")]
        JBQ
    }
    /// <summary>
    /// 设备同步状态
    /// </summary>
    public enum SyncDeviceSyncState
    {
        /// <summary>
        /// 未同步
        /// </summary>
        [Description("未同步")]
        UnSync = 0,
        /// <summary>
        /// 已同步
        /// </summary>
        [Description("已同步")]
        Synced = 1,
        /// <summary>
        /// 读取数据
        /// </summary>
        [Description("读取数据")]
        ReadData,
        /// <summary>
        /// 处理数据
        /// </summary>
        [Description("处理数据")]
        DealData,
        /// <summary>
        /// 优化数据
        /// </summary>
        [Description("优化数据")]
        OptData,
        /// <summary>
        /// 同步数据
        /// </summary>
        [Description("同步数据")]
        SyncData,
        /// <summary>
        /// 同步失败
        /// </summary>
        [Description("同步失败")]
        SyncFailed
    }
    /// <summary>
    /// 设备改变委托
    /// </summary>
    public delegate void SyncDeviceChanged(SyncDevice sDev);
    /// <summary>
    /// 设备提交数据委托
    /// </summary>
    /// <returns></returns>
    public delegate bool SubmitDataMethod(SyncDevice sDev, object submitData);
}

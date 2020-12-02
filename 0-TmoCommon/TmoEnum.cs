using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace TmoCommon
{
    /// <summary>
    /// 客户端通信接口方法编号
    /// </summary>
    public enum funCode
    {
        //注意：新添加接口方法需要在此添加枚举

        /// <summary>
        /// 检查网络连接
        /// </summary>
        CheckLink,
        GetPersonData,
        GetGetNewPersonData,
        GetRiskData,
        RiskNewReport,
        RiskMedical,
        RiskSaveMedical,
        GetTimes,
        updateRisk,
        GetReportData,
        GetNewReportData,
        GetItemDataShow,
        GetRiskResult,
        GetIds,
        CheckIDCard,

        #region 新评估

        getTangniao,
        getTest,
        getFeiPang,
        getScreenData,
        reportIn,
        reportUpdate,

        #endregion

        #region 伪实体

        /// <summary>
        /// 伪实体-获取表结构
        /// </summary>
        FakeEntity_GetTableStruct,

        /// <summary>
        /// 伪实体-获取数据
        /// </summary>
        FakeEntity_GetData,

        /// <summary>
        /// 伪实体-获取数据(新)
        /// </summary>
        FakeEntity_GetDataNew,

        /// <summary>
        /// 伪实体-提交数据
        /// </summary>
        FakeEntity_SubmitData,

        /// <summary>
        /// 伪实体-提交数据(新)
        /// </summary>
        FakeEntity_SubmitDataNew,

        /// <summary>
        /// 伪实体-获取分页数据
        /// </summary>
        FakeEntity_GetPageData,

        /// <summary>
        /// 伪实体-是否存在相同值
        /// </summary>
        FakeEntity_ExistSameValue,

        /// <summary>
        /// 伪实体-删除一条数据
        /// </summary>
        FakeEntity_DeleteData,

        /// <summary>
        /// 获取下一个ID
        /// </summary>
        GetNextID,

        #endregion

        #region 问卷信息采集与加载

        AddQuestionnaire, //新增问卷信息
        UpdateQuestionnaire, //修改问卷信息
        DeleteQuestionnaire, //删除问卷信息
        SelectQuestionnaire, //查询问卷信息
        SelectLookQuestionnaire, //浏览问卷信息
        SelectLastQues, //查询最后一次问卷信息
        SelectUserinfo, //查询用户基本信息
        GetPublicList, //根据表明获取字典
        GetDeviceValue, //获取设备导入数据

        GetFistQuestionnaires, //获取筛选问卷
        GetQuestionnaires, //获取筛选问卷
        SaveQuestionnaires, //暂存问卷
        SubmitQuestionnaires, //提交问卷
        DeleteQuestionnaires, //删除问卷

        #endregion

        #region 客户意见

        GetOpinionData,
        GetNewOpinionData,
        UpdateOpinion,
        AddReply,
        AddAsk,

        #endregion

        GetRiskFiveData, //获取前五次体检数据
        GetNewFiveData, //获取前五次体检数据
        GetImetData, //
        SaveReportUP, //提交报告修改的数据
        GetMainData, //获取报告修改后的数据
        SaveReportUPdate, //修改维护报告的结果
        ReportDel, //删除评估报告
        ReportDelNew, //删除问卷
        GetProType, //获取项目类别
        AddProject, //添加方案项目
        AddVideo,
        UpdateVideo,
        GetProjectDic, //获取解决方案字典表
        GetVideoId,
        GeVideoList, //获取解决方案字典表
        InProResult, //生成解决方案
        GetProResult, //获取个人用户生成的解决方案
        unpdtePersonPro, //修改个人解决方案
        updateAll, //修改个人解决方案
        DelPerProre, //删除个人解决方案
        DelProject,
        delVideoid,
        DelPush,
        GetPushCount,
        GetdocInfo,
        GetPushlist,
        lookPush,
        TuijianZhi,
        TuijianUpdate,
        getTuiData,
        GettuiDataUser,
        InsertAttach,
        GetAttach,
        DelAttach,
        UpdateAttch,

        #region 指标录入

        medicQuery,
        medicadd,
        delmedic,
        updatemedic,
        checkname,

        #endregion

        /// <summary>
        /// 检查健康师登录权限
        /// </summary>
        CheckDocAuth,

        /// <summary>
        /// 获取延伸服务信息
        /// </summary>
        GetServiceData,

        /// <summary>
        /// 修改延伸服务支付状态
        /// </summary>
        UpdatePayType,

        /// <summary>
        /// 延伸服务退费
        /// </summary>
        BackService,

        /// <summary>
        /// 获取新延伸服务信息
        /// </summary>
        GetNewServiceData,

        /// <summary>
        /// 修改新延伸服务支付状态
        /// </summary>
        UpdateNewPayType,

        /// <summary>
        /// 新延伸服务退费
        /// </summary>
        NewBackService,

        /// <summary>
        /// 添加监测数据
        /// </summary>
        AddMonitorData,
        GetProjectData,
        GetProjectDataPerson,
        GetMonitorData,
        GetMonitorDataBy,
        GetItemData,
        GetpushMsgData,
        GetMonitorData24,

        #region 健康阅读

        /// <summary>
        /// 新增健康阅读
        /// </summary>
        OptionalAdd,
        GetArticleData,
        OptionalUpdate,
        OptionalSelect,
        OptionalDelete,

        #endregion

        #region 关于我们

        AddOrUpdateAboutUs,
        LoadAuoutUs,
        Getdis_dic,

        #endregion

        #region 进销存

        GetStockList,
        GetPurchasesList,
        GetSellList,
        AddSell,
        AddPurchase,
        UpdateState,
        AddProduct,
        DeleteProduct,

        #endregion

        #region 积分商城

        GetNurDiaryList,
        GetSportDiaryList,
        GetTargetDiaryList,
        GetTargetAppendList,
        GetManagermentList,
        GetExpenseDetial,
        GetPointsGoodsList,
        GetPharmacyList,
        GetLivingList,
        UpdateStatePT,
        CreatePointsUser,
        MedicalInADD,
        GetMedicalIn,
        GetMedicalInUser,
        delMedicalIn,
        AddTargetAppend,
        AddPharmacyRecord,
        AddNurDiary,
        AddSportDiary,
        AddLivingDiary,

        #endregion

        #region 微信

        PushAddWxMsg,
        PushAddWeiXinAnswer,
        GetBindId,

        #endregion

        #region 积分中心

        #endregion

        #region 健康干预

        AddPushList,
        AddIntervene,

        #endregion

        #region 膳食

        GetnurtypeItem,
        GetHotDic,
        GetNurData,
        SaveNurData,
        UpdateNurData,
        DeleNurData,
        GetPersonNurData,
        InputPersonNur,
        GetPNurData,

        #endregion

        //保存健康行动计划
        SaveActionPlan,
        /// <summary>
        /// 未知
        /// </summary>
        UnKnow,
        /// <summary>
        /// 获得用户信息
        /// </summary>
        GetUserInfo,
    }

    public class FuncMainParam
    {
        public string CheckData { get; set; } = string.Empty;
        public string CheckKey { get; set; } = string.Empty;
        public funCode FunCode { get; set; } = funCode.UnKnow;
        public object[] FunParams { get; set; } = new object[0];
    }
    
    /// <summary>
    /// 已有服务枚举
    /// </summary>
    public enum Services
    {
        /// <summary>
        /// 示例服务
        /// </summary>
        [Description("示例服务")] Demo,

        /// <summary>
        /// 业务服务
        /// </summary>
        [Description("业务服务")] BizService,

        /// <summary>
        /// 数据库服务
        /// </summary>
        [Description("数据库服务")] DataBase,

        /// <summary>
        /// 数据推送服务
        /// </summary>
        [Description("数据推送服务")] PushData,

        /// <summary>
        /// 计划服务
        /// </summary>
        [Description("计划服务")] PlanService,

        /// <summary>
        /// TCP服务
        /// </summary>
        [Description("TCP服务")] TCPService,

        /// <summary>
        /// 设备服务
        /// </summary>
        [Description("设备服务")] DevService
    }

    /// <summary>
    ///  数据操作状态    
    /// </summary>
    public enum DBState
    {
        Select,
        Insert,
        Update,
        Delete,
        None
    }


    /// <summary>
    /// 数据操作类型
    /// </summary>
    public enum DBOperateType
    {
        /// <summary>
        /// 新建添加
        /// </summary>
        Add = 0,

        /// <summary>
        /// 修改编辑
        /// </summary>
        Update = 1,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 2,

        /// <summary>
        /// 查看
        /// </summary>
        View
    }

    /// <summary>
    ///  数据库类型    
    /// </summary>
    public enum DBType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unkonwn,

        /// <summary>
        /// SQLServer数据库
        /// </summary>
        SQLSERVER,

        /// <summary>
        /// ORACLE数据库
        /// </summary>
        ORACLE,

        /// <summary>
        /// MySQL数据库
        /// </summary>
        MYSQL,

        /// <summary>
        /// SQLLite数据库
        /// </summary>
        SQLITE,

        /// <summary>
        /// ACCESS数据库
        /// </summary>
        ACCESS
    }

    /// <summary>
    /// 客户端类型
    /// </summary>
    public enum ClientType
    {
        /// <summary>
        /// 医生端
        /// </summary>
        [Description("TmoClient")] TmoClient,

        /// <summary>
        /// 移动监测导入工具
        /// </summary>
        [Description("MonitorTool")] MonitorTool
    }

    /// <summary>
    /// 推送类型
    /// </summary>
    public enum PushType
    {
        /// <summary>
        ///0 医生弹框通知
        /// </summary>
        [Description("医生弹窗")] doc_notification,

        /// <summary>
        ///1 医生短信提醒
        /// </summary>
        [Description("医生短信")] doc_sms,

        /// <summary>
        ///2 医生微信提醒
        /// </summary>
        [Description("医生微信")] doc_wechat,

        /// <summary>
        ///3 医生电子邮件
        /// </summary>
        [Description("医生电子邮件")] doc_mail,

        /// <summary>
        ///4 用户站内信提醒
        /// </summary>
        [Description("站内信")] user_notification,

        /// <summary>
        ///5 用户短信提醒
        /// </summary>
        [Description("用户短信")] user_sms,

        /// <summary>
        ///6 用户微信提醒
        /// </summary>
        [Description("微信")] user_wechat,

        /// <summary>
        ///7 用户电子邮件
        /// </summary>
        [Description("电子邮件")] user_mail,

        /// <summary>
        /// 8 无
        /// </summary>
        [Description("无")] None
    }

    /// <summary>
    /// 数据操作类型
    /// </summary>
    public enum eOperateType
    {
        /// <summary>
        /// 新建
        /// </summary>
        Add = 0,

        /// <summary>
        /// 修改
        /// </summary>
        Update = 1,

        /// <summary>
        /// 浏览
        /// </summary>
        Browse = 2,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 3,

        /// <summary>
        /// 执行
        /// </summary>
        Execute,

        /// <summary>
        /// 选择器
        /// </summary>
        Seletor,

        /// <summary>
        /// None
        /// </summary>
        None
    }

    #region 旧代码

    public enum eCacheType
    {
        /// <summary>
        /// 业务缓存
        /// </summary>
        BLL,

        /// <summary>
        /// 静态缓存
        /// </summary>
        Static,

        /// <summary>
        /// 常量缓存
        /// </summary>
        Const,
    }

    /// <summary>
    /// 报告产出操作类型
    /// </summary>
    public enum eReportExportType
    {
        /// <summary>
        /// 空操作
        /// </summary>
        None = 0,

        /// <summary>
        /// 打印
        /// </summary>
        Print = 1,

        /// <summary>
        /// 导出为pdf
        /// </summary>
        ExtToPdf = 2,

        /// <summary>
        /// 导出成Excel 03格式
        /// </summary>
        ExtToXls = 3,

        /// <summary>
        /// 导出成Excel 07+格式
        /// </summary>
        ExtToXlsx = 4,

        /// <summary>
        /// 导出Word RTF 格式
        /// </summary>
        ExtToRtf = 5,

        /// <summary>
        /// 导出PNG 格式
        /// </summary>
        ExtToPng = 6,

        /// <summary>
        /// 导出XML格式
        /// </summary>
        ExtToXml = 7,

        /// <summary>
        /// 导出Html Url 网址
        /// </summary>
        ExtToHtmlUrl = 8,

        /// <summary>
        /// 导出Html Zip 压缩包
        /// </summary>
        ExtToHtmlZip = 9
    }

    /// <summary>
    /// 系统架构类型
    /// </summary>
    public enum eSystemModeType
    {
        /// <summary>
        /// Windows窗体应用程序
        /// </summary>
        Winform,

        /// <summary>
        /// 网络服务
        /// </summary>
        WebService,

        /// <summary>
        /// 手机应用服务
        /// </summary>
        AppService
    }


    #region 图片类型

    /// <summary>
    /// 标题名枚举
    /// </summary>
    public enum TitleTypes
    {
        /// <summary>
        /// 主窗体标题
        /// </summary>
        FrmMainTitle,
        Orther
    }

    #endregion

    public enum eFormMode
    {
        MaxSize,
        MiddleSize,
        MinSize,
        None,
    }

    public enum eProgressType
    {
        /// <summary>
        /// 未设置
        /// </summary>
        None = 0,

        /// <summary>
        /// 未执行准备中
        /// </summary>
        Prepare = 1,

        /// <summary>
        /// 进行中
        /// </summary>
        Proceed = 2,

        /// <summary>
        /// 已完成
        /// </summary>
        Complete = 3,

        /// <summary>
        /// 废除
        /// </summary>
        Abolish = 4
    }

    /// <summary>
    /// 接口类型
    /// </summary>
    public enum eInterfaceType
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,

        /// <summary>
        /// 系统体检
        /// </summary>
        Meidcal = 1,

        /// <summary>
        /// 文件
        /// </summary>
        File = 2,

        /// <summary>
        /// 默认空
        /// </summary>
        None = 3
    }

    /// <summary>
    /// 问卷类型（风险评估问卷、心理评估问卷、健康档案）
    /// </summary>
    public enum eQuestionType
    {
        /// <summary>
        /// 空类型
        /// </summary> 
        None = 0,

        /// <summary>
        /// 健康风险评估综合版
        /// </summary>
        RiskEvalute = 1,

        /// <summary>
        /// 心理评估
        /// </summary>
        PsychologyEvalute = 2,

        /// <summary>
        /// 个人健康档案
        /// </summary>
        HealthArchives = 3
    }


    /// <summary>
    /// 体检问卷类型（自定义体检或健康档案）
    /// </summary>
    public enum eMedicalSource
    {
        /// <summary>
        /// 空
        /// </summary>
        None = 0,

        /// <summary>
        /// 自定义体检类型那个
        /// </summary>
        CustomMedical = 1,

        /// <summary>
        /// 健康档案
        /// </summary>
        HealthArchive = 2
    }

    /// <summary>
    /// 问卷模块类型
    /// </summary>
    public enum eQCategoryType
    {
        /// <summary>
        /// 用户基本信息
        /// </summary>
        UserData = 0,

        /// <summary>
        /// 个人疾病史
        /// </summary>
        Disease = 1,

        /// <summary>
        /// 家族疾病史
        /// </summary>
        Family_History = 2,

        /// <summary>
        /// 吸烟
        /// </summary>
        Smoke = 3,

        /// <summary>
        /// 膳食
        /// </summary>
        Nur = 4,

        /// <summary>
        /// 运动
        /// </summary>
        Sport = 5,

        /// <summary>
        /// 睡眠
        /// </summary>
        Sleep = 6,

        /// <summary>
        /// 心理状况
        /// </summary>
        Spirit = 7,

        /// <summary>
        /// 居住环境
        /// </summary>
        Live = 8,

        /// <summary>
        /// 体检信息
        /// </summary>
        Medical = 9,

        /// <summary>
        /// 月经史和生育史
        /// </summary>
        Menstruation_Birth = 10,

        /// <summary>
        /// 空类型
        /// </summary>
        None = 100
    }

    /// <summary>
    /// 查询类型
    /// </summary>
    public enum eDateSpanType
    {
        Custom = 0, //自定义
        Today = 1, //当天
        Yestoday = 2, //昨天
        Week = 3, //本周
        Month = 4, //本月
        Year = 5, //本年
        All = 6, //所有记录
        History = 7 //历史完成记录
    }

    /// <summary>
    /// 验证结果类型
    /// </summary>
    public enum eValidateState
    {
        Default,
        OK,
        Error,
        Gray
    }

    /// <summary>
    /// 系统背景图片枚举
    /// </summary>
    public enum BackGroundImageTypes
    {
        /// <summary>
        /// 登录闪屏
        /// </summary>
        bgImg_SP,

        /// <summary>
        /// 关于
        /// </summary>
        bgImg_About,

        /// <summary>
        ///登录页面
        /// </summary>
        bgImg_Login,

        /// <summary>
        /// 系统图标
        /// </summary>
        bgImg_Ico,

        /// <summary>
        /// 登录按钮图片
        /// </summary>
        bgImg_LoginButton,

        /// <summary>
        /// 退出登录图片
        /// </summary>
        bgImg_LoginCancelButton,

        /// <summary>
        /// 所有窗体的背景图片
        /// </summary>
        bgImg_BackGroundImag
    }

    /// <summary>
    /// 代理类别
    /// </summary>
    public enum ProxyType
    {
        Socks5,
        http,
        SOCKS4,
        None
    }

    public enum eReportType
    {
        /// <summary>
        /// 套餐内报告
        /// </summary>
        InsidePackageReport,

        /// <summary>
        /// 套餐外报告
        /// </summary>
        OutSidePackageReport,

        /// <summary>
        /// 体检报告
        /// </summary>
        MecialReport,

        /// <summary>
        /// 心里评估报告
        /// </summary>
        PressureReport,

        /// <summary>
        /// 人群统计报告
        /// </summary>
        CrowdReport
    }

    public enum EnumTableStyle
    {
        GreenTeader = 1,
        GreenDetail = 2,
        BlueHeader = 3,
        BlueDetail = 4
    }

    public enum FileType
    {
        /// <summary>
        /// 图片
        /// </summary>
        Image = 0,

        /// <summary>
        /// 文件
        /// </summary>
        File = 1
    }

    #endregion
}
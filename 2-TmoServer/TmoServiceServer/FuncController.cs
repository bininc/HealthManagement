using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using TmoCommon;
using TmoCommon.SocketLib;

namespace TmoServiceServer
{
    /// <summary>
    /// 客户端Remoting调用类
    /// </summary>
    public class FuncController : ApiController
    {
        /// <summary>
        /// 客户端调用事件
        /// </summary>
        public static event Action<string> OnInvokedMain;

        #region 通信服务入口

        /// <summary>
        /// 客户端通信接口
        /// </summary>
        [HttpPost]
        [ActionName("InvokeMain")]
        public object InvokeMain(string checkData, string checkKey, funCode funCode, params object[] funParams)
        {
            InvokeEvent(funCode, checkData, checkKey, funParams);
            //默认返回码 
            object returnObj = "err_Unkonwn"; //未知错误
            try
            {
                #region 加密狗验证

                #endregion

                #region 客户端校验

                #endregion

                #region 处理不同的方法请求

                switch (funCode)
                {
                    case funCode.CheckLink: //连接检查
                        returnObj = FunctionClass.CheckLink();
                        break;
                    case funCode.CheckIDCard:
                        string IdCardxml = funParams[0].ToString();
                        returnObj = FunctionClass.CheckIDCard(IdCardxml);
                        break;
                    case funCode.GetPersonData:
                        string Personxml = funParams[0].ToString();
                        returnObj = FunctionClass.GetPersonData(Personxml);
                        break;
                    case funCode.GetGetNewPersonData:
                        returnObj = FunctionClass.GetGetNewPersonData(funParams[0].ToString());
                        break;
                    case funCode.RiskNewReport:
                        returnObj = FunctionClass.RiskNewReport(funParams[0].ToString(), funParams[1].ToString());
                        break;
                    case funCode.RiskSaveMedical:
                        returnObj = FunctionClass.RiskSaveMedical(funParams[0].ToString(), funParams[1].ToString());
                        break;
                    case funCode.GetNewReportData:
                        returnObj = FunctionClass.GetNewReportData(funParams[0].ToString());
                        break;
                    case funCode.GetRiskData:
                        string riskxml = funParams[0].ToString();
                        returnObj = FunctionClass.GetRiskData(riskxml);
                        break;
                    case funCode.RiskMedical:
                        string Riskxml = funParams[0].ToString();
                        returnObj = FunctionClass.RiskMedical(Riskxml);
                        break;
                    case funCode.GetIds:
                        returnObj = FunctionClass.GetIds(funParams[0].ToString(), funParams[1].ToString());
                        break;
                    case funCode.GetTimes:
                        string user_id = funParams[0].ToString();
                        returnObj = FunctionClass.GetTimes(user_id);
                        break;
                    case funCode.updateRisk:
                        string user_ids = funParams[0].ToString();
                        string user_times = funParams[1].ToString();
                        returnObj = FunctionClass.updateRisk(user_ids, user_times);
                        break;
                    case funCode.GetReportData:
                        string Pxml = funParams[0].ToString();
                        returnObj = FunctionClass.GetReportData(Pxml);
                        break;
                    case funCode.GetRiskResult:
                        returnObj = FunctionClass.GetRiskResult(funParams[0].ToString(), funParams[1].ToString());
                        break;
                    case funCode.InsertAttach:
                        returnObj = FunctionClass.InsertAttach(funParams[0] as byte[], funParams[1].ToString(), funParams[2].ToString(),
                            funParams[3].ToString());
                        break;
                    case funCode.GetAttach:
                        returnObj = FunctionClass.GetAttach(funParams[0].ToString(), funParams[1].ToString(), funParams[2].ToString());
                        break;
                    case funCode.UpdateAttch:
                        returnObj = FunctionClass.UpdateAttch(funParams[0].ToString(), funParams[1] as byte[], funParams[2].ToString());
                        break;
                    case funCode.DelAttach:
                        returnObj = FunctionClass.DelAttach(funParams[0].ToString(), funParams[1].ToString());
                        break;

                    #region 实体相关

                    case funCode.FakeEntity_GetTableStruct:
                        returnObj = FunctionClass.GetTableStruct(funParams[0].ToString());
                        break;
                    case funCode.FakeEntity_GetData:
                        returnObj = FunctionClass.GetData(funParams[0].ToString());
                        break;
                    case funCode.FakeEntity_GetDataNew:
                        returnObj = FunctionClass.GetData(funParams[0]);
                        break;
                    case funCode.FakeEntity_SubmitData:
                        returnObj = FunctionClass.SubmitData(funParams[0].ToString());
                        break;
                    case funCode.FakeEntity_SubmitDataNew:
                        returnObj = FunctionClass.SubmitData(funParams[0]);
                        break;
                    case funCode.FakeEntity_GetPageData:
                        returnObj = FunctionClass.GetPageData(funParams[0].ToString());
                        break;
                    case funCode.FakeEntity_ExistSameValue:
                        returnObj = FunctionClass.ExistSameValue(funParams[0].ToString(), funParams[1].ToString(), funParams[2].ToString(), funParams[3],
                            bool.Parse(funParams[4].ToString()));
                        break;
                    case funCode.FakeEntity_DeleteData:
                        returnObj = FunctionClass.DelData(funParams);
                        break;

                    #endregion

                    #region 新版问卷

                    case funCode.GetFistQuestionnaires:
                        returnObj = FunctionClass.GetFistQuestionnaires(funParams);
                        break;
                    case funCode.GetQuestionnaires:
                        returnObj = FunctionClass.GetQuestionnaires(funParams);
                        break;
                    case funCode.SaveQuestionnaires:
                        returnObj = FunctionClass.SaveQuestionnaires(funParams);
                        break;
                    case funCode.SubmitQuestionnaires:
                        returnObj = FunctionClass.SubmitQuestionnaires(funParams);
                        break;
                    case funCode.DeleteQuestionnaires:
                        returnObj = FunctionClass.DeleteQuestionnaires(funParams);
                        break;

                    #endregion

                    #region 推荐列表

                    case funCode.TuijianZhi:
                        returnObj = FunctionClass.InputDicUser(funParams[0].ToString());
                        break;
                    case funCode.TuijianUpdate:
                        returnObj = FunctionClass.UpdateDicUser(funParams[0].ToString());
                        break;
                    case funCode.getTuiData:
                        returnObj = FunctionClass.getTuiData();
                        break;
                    case funCode.GettuiDataUser:
                        returnObj = FunctionClass.GetDataUser(funParams[0].ToString(), funParams[1].ToString());
                        break;

                    #endregion

                    case funCode.AddQuestionnaire:
                        string quesxml = funParams[0].ToString();
                        returnObj = FunctionClass.AddQuestionnaire(quesxml);
                        break;
                    case funCode.UpdateQuestionnaire:
                        string quesupxml = funParams[0].ToString();
                        returnObj = FunctionClass.UpdateQuestionnaire(quesupxml);
                        break;
                    case funCode.DeleteQuestionnaire:
                        string userId = funParams[0].ToString();
                        string userTimes = funParams[1].ToString();
                        returnObj = FunctionClass.DeleteQuestionnaire(userId, userTimes);
                        break;
                    case funCode.SelectQuestionnaire:
                        string quesUserID = funParams[0].ToString();
                        string times = funParams[1].ToString();
                        returnObj = FunctionClass.SelectQuestionnaire(quesUserID, times);
                        break;
                    case funCode.SelectLookQuestionnaire:
                        string quesUserID1 = funParams[0].ToString();
                        string times1 = funParams[1].ToString();
                        returnObj = FunctionClass.SelectLookQuestionnaire(quesUserID1, times1);
                        break;
                    case funCode.SelectLastQues:
                        string identity = funParams[0].ToString();
                        returnObj = FunctionClass.SelectLastQues(identity);
                        break;
                    case funCode.SelectUserinfo:
                        string userID = funParams[0].ToString();
                        returnObj = FunctionClass.SelectUserinfo(userID);
                        break;
                    case funCode.GetDeviceValue:
                        string dic = funParams[0].ToString();
                        string identitys = funParams[1].ToString();
                        returnObj = FunctionClass.GetDeviceValue(dic, identitys);
                        break;
                    case funCode.GetPublicList:
                        string tableName = funParams[0].ToString();
                        string condition = funParams[1].ToString();
                        returnObj = FunctionClass.GetPublicList(tableName, condition);
                        break;
                    case funCode.GetRiskFiveData:
                        string user_ID = funParams[0].ToString();
                        returnObj = FunctionClass.GetRiskFiveData(user_ID);
                        break;
                    case funCode.GetNewFiveData:
                        returnObj = FunctionClass.GetNewFiveData(funParams[0].ToString(), funParams[1].ToString());
                        break;
                    case funCode.GetImetData:
                        returnObj = FunctionClass.GetImetData(funParams[0].ToString(), funParams[1].ToString());
                        break;
                    case funCode.SaveReportUP:
                        string bloodreason = funParams[0].ToString();
                        string bloodadvice = funParams[1].ToString();
                        string pressurereason = funParams[2].ToString();
                        string pressureadvice = funParams[3].ToString();
                        string user_iD = funParams[4].ToString();
                        string user_Times = funParams[5].ToString();
                        string bloodlipid_reason = funParams[6].ToString();
                        string bloodlipid_advice = funParams[7].ToString();
                        string zhuanjia = funParams[8].ToString();
                        returnObj = FunctionClass.SaveReportUP(user_iD, user_Times, bloodreason, bloodadvice, pressurereason, pressureadvice, bloodlipid_reason,
                            bloodlipid_advice, zhuanjia, funParams[9].ToString(), funParams[10].ToString(), funParams[11].ToString(), funParams[12].ToString(),
                            funParams[13].ToString(), funParams[14].ToString(), funParams[15].ToString(), funParams[16].ToString(), funParams[17].ToString(),
                            funParams[18].ToString());
                        break;
                    case funCode.GetMainData:
                        returnObj = FunctionClass.GetMainData(funParams[0].ToString(), funParams[1].ToString());
                        break;
                    case funCode.SaveReportUPdate:
                        string service_id = funParams[0].ToString();
                        string bloodreasonu = funParams[1].ToString();
                        string bloodadviceu = funParams[2].ToString();
                        string pressurereasonu = funParams[3].ToString();
                        string pressureadviceu = funParams[4].ToString();
                        string bloodlipid_reason1 = funParams[7].ToString();
                        string bloodlipid_advice2 = funParams[8].ToString();
                        string azhuanjia = funParams[9].ToString();
                        returnObj = FunctionClass.SaveReportUPdate(service_id, funParams[5].ToString(), funParams[6].ToString(), bloodreasonu, bloodadviceu,
                            pressurereasonu, pressureadviceu, bloodlipid_reason1, bloodlipid_advice2, azhuanjia, funParams[10].ToString(),
                            funParams[11].ToString(), funParams[12].ToString(), funParams[13].ToString(), funParams[14].ToString(), funParams[15].ToString(),
                            funParams[16].ToString(), funParams[17].ToString(), funParams[18].ToString(), funParams[19].ToString());
                        break;
                    case funCode.ReportDel:
                        returnObj = FunctionClass.ReportDel(funParams[0].ToString(), funParams[1].ToString());
                        break;
                    case funCode.ReportDelNew:
                        returnObj = FunctionClass.ReportDelNew(funParams[0].ToString(), funParams[1].ToString());
                        break;
                    case funCode.GetProType:
                        returnObj = FunctionClass.GetProType();
                        break;
                    case funCode.AddProject:
                        string xmlData = funParams[0].ToString();
                        returnObj = FunctionClass.AddProject(xmlData);
                        break;
                    case funCode.UpdateVideo:
                        returnObj = FunctionClass.UpdateVideo(funParams[0].ToString());
                        break;
                    case funCode.delVideoid:
                        returnObj = FunctionClass.DelVideo(funParams[0].ToString());
                        break;
                    case funCode.AddVideo:
                        returnObj = FunctionClass.AddVideo(funParams[0].ToString());
                        break;
                    case funCode.GetProjectDic:
                        string projecttype = funParams[0].ToString();
                        string project = funParams[1].ToString();
                        returnObj = FunctionClass.GetProjectDic(projecttype, project, funParams[2].ToString());
                        break;
                    case funCode.GeVideoList:
                        string videName = funParams[0].ToString();
                        returnObj = FunctionClass.GeVideoList(videName);
                        break;
                    case funCode.GetVideoId:
                        string videoID = funParams[0].ToString();
                        returnObj = FunctionClass.GetVideoId(videoID);
                        break;

                    case funCode.InProResult:
                        string inputproxml = funParams[0].ToString();
                        returnObj = FunctionClass.InProResult(inputproxml);
                        break;
                    case funCode.GetProResult:
                        returnObj = FunctionClass.GetProResult(funParams[0].ToString(), funParams[1].ToString(), funParams[2].ToString());
                        break;
                    case funCode.unpdtePersonPro:
                        returnObj = FunctionClass.unpdtePersonPro(funParams[0].ToString(), funParams[1].ToString());
                        break;

                    case funCode.updateAll:
                        returnObj = FunctionClass.unpdteProAll(funParams[0].ToString());
                        break;
                    case funCode.DelPerProre:
                        returnObj = FunctionClass.DelPerProre(funParams[0].ToString(), funParams[1].ToString(), funParams[2].ToString());
                        break;
                    case funCode.GetNextID:
                        returnObj = FunctionClass.GetNextID(funParams);
                        break;
                    case funCode.CheckDocAuth:
                        returnObj = FunctionClass.CheckDocAuth(funParams);
                        break;
                    case funCode.GetServiceData:
                        string Servicexml = funParams[0].ToString();
                        returnObj = FunctionClass.GetServiceData(Servicexml);
                        break;
                    case funCode.UpdatePayType:
                        string payxml = funParams[0].ToString();
                        returnObj = FunctionClass.UpdatePayType(payxml);
                        break;
                    case funCode.BackService:
                        string user_id1 = funParams[0].ToString();
                        string user_time1 = funParams[1].ToString();
                        returnObj = FunctionClass.BackService(user_id1, user_time1);
                        break;
                    case funCode.GetNewServiceData:
                        string Servicexmlnew = funParams[0].ToString();
                        returnObj = FunctionClass.GetNewServiceData(Servicexmlnew);
                        break;
                    case funCode.UpdateNewPayType:
                        string payxmlnew = funParams[0].ToString();
                        returnObj = FunctionClass.UpdateNewPayType(payxmlnew);
                        break;
                    case funCode.NewBackService:
                        string user_idnew = funParams[0].ToString();
                        string user_timenew = funParams[1].ToString();
                        returnObj = FunctionClass.NewBackService(user_idnew, user_timenew);
                        break;
                    case funCode.AddMonitorData:
                        returnObj = FunctionClass.AddMonitorData(funParams);
                        break;
                    case funCode.GetProjectData:
                        string Pxmls = funParams[0].ToString();
                        returnObj = FunctionClass.GetProjectData(Pxmls);
                        break;
                    case funCode.GetProjectDataPerson:
                        returnObj = FunctionClass.GetProjectDataPerson(funParams[0].ToString());
                        break;
                    case funCode.GetMonitorData:
                        string id = funParams[0].ToString();
                        returnObj = FunctionClass.GetMonitorData(id);
                        break;
                    case funCode.GetMonitorDataBy:
                        returnObj = FunctionClass.GetMonitorDataBy(funParams[0].ToString());
                        break;
                    case funCode.GetMonitorData24:
                        returnObj = FunctionClass.GetMonitorData24(funParams[0].ToString());
                        break;
                    case funCode.GetItemData:
                        returnObj = FunctionClass.GetItemData();
                        break;
                    case funCode.GetItemDataShow:
                        string Itemxml = funParams[0].ToString();
                        returnObj = FunctionClass.GetItemDataShow(Itemxml);
                        break;
                    case funCode.DelProject:
                        returnObj = FunctionClass.DelProject(funParams[0].ToString());
                        break;

                    case funCode.GetOpinionData:
                        string xml = funParams[0].ToString();
                        returnObj = FunctionClass.GetOpinionData(xml);
                        break;
                    case funCode.GetNewOpinionData:
                        string xmlnew = funParams[0].ToString();
                        returnObj = FunctionClass.GetNewOpinionData(xmlnew);
                        break;
                    case funCode.UpdateOpinion:
                        string adviseID = funParams[0].ToString();
                        string askContent = funParams[1].ToString();
                        string docCode = funParams[2].ToString();
                        returnObj = FunctionClass.UpdateOpinion(adviseID, askContent, docCode);
                        break;
                    case funCode.AddReply:
                        string con_id = funParams[0].ToString();
                        string reply_content = funParams[1].ToString();
                        string doc_id = funParams[2].ToString();
                        returnObj = FunctionClass.AddReply(con_id, reply_content, doc_id);
                        break;
                    case funCode.AddAsk:
                        string dsxmlask = funParams[0].ToString();
                        returnObj = FunctionClass.AddAsk(dsxmlask);
                        break;
                    case funCode.OptionalAdd:
                        string dsxml = funParams[0].ToString();
                        returnObj = FunctionClass.OptionalAdd(dsxml);
                        break;
                    case funCode.OptionalUpdate:
                        string dsxmlou = funParams[0].ToString();
                        returnObj = FunctionClass.OptionalUpdate(dsxmlou);
                        break;
                    case funCode.OptionalDelete:
                        string dsxmlod = funParams[0].ToString();
                        returnObj = FunctionClass.OptionalDelete(dsxmlod);
                        break;
                    case funCode.OptionalSelect:
                        string dsxmlos = funParams[0].ToString();
                        returnObj = TmoShare.getXMLFromDataSet(FunctionClass.OptionalSelect(dsxmlos));
                        break;
                    case funCode.GetArticleData:
                        string Servicexmls = funParams[0].ToString();
                        returnObj = FunctionClass.GetArticleData(Servicexmls);
                        break;
                    case funCode.AddOrUpdateAboutUs:
                        string doc_code = funParams[0].ToString();
                        string hos_code = funParams[1].ToString();
                        string aboutxmls = funParams[2].ToString();
                        returnObj = FunctionClass.AddOrUpdateAboutUs(doc_code, hos_code, aboutxmls);
                        break;
                    case funCode.LoadAuoutUs:
                        string fieldname = funParams[0].ToString();
                        string hos_codes = funParams[1].ToString();
                        returnObj = FunctionClass.LoadAuoutUs(fieldname, hos_codes);
                        break;
                    case funCode.Getdis_dic:

                        returnObj = FunctionClass.Getdis_dic();
                        break;
                    case funCode.GetStockList:
                        string stockxmls = funParams[0].ToString();
                        returnObj = FunctionClass.GetStockList(stockxmls);
                        break;
                    case funCode.GetPurchasesList:
                        string purchasesxmls = funParams[0].ToString();
                        returnObj = FunctionClass.GetPurchasesList(purchasesxmls);
                        break;
                    case funCode.GetSellList:
                        string sellxmls = funParams[0].ToString();
                        returnObj = FunctionClass.GetSellList(sellxmls);
                        break;
                    case funCode.AddSell:
                        string sellxmlss = funParams[0].ToString();
                        returnObj = FunctionClass.AddSell(sellxmlss);
                        break;
                    case funCode.AddPurchase:
                        string purchasexml = funParams[0].ToString();
                        returnObj = FunctionClass.AddPurchase(purchasexml);
                        break;
                    case funCode.AddProduct:
                        string productxml = funParams[0].ToString();
                        returnObj = FunctionClass.AddProduct(productxml);
                        break;
                    case funCode.DeleteProduct:
                        string productID = funParams[0].ToString();
                        returnObj = FunctionClass.DeleteProduct(productID);
                        break;
                    case funCode.AddPushList:
                        returnObj = FunctionClass.AddPushList(funParams[0].ToString());
                        break;
                    case funCode.AddIntervene:
                        returnObj = FunctionClass.AddIntervene(funParams);
                        break;
                    case funCode.UpdateState:
                        string name = funParams[0].ToString();
                        string state = funParams[1].ToString();
                        string sellID = funParams[2].ToString();
                        returnObj = FunctionClass.UpdateState(name, state, sellID);
                        break;
                    case funCode.GetnurtypeItem:
                        returnObj = FunctionClass.GetnurtypeItem();
                        break;
                    case funCode.GetHotDic:
                        returnObj = FunctionClass.GetHotDic();
                        break;
                    case funCode.GetNurData:
                        string nurparentId = funParams[0].ToString();
                        string nurhotId = funParams[1].ToString();
                        returnObj = FunctionClass.GetNurData(nurparentId, nurhotId);
                        break;
                    case funCode.SaveNurData:
                        string parentid = funParams[0].ToString();
                        string hotid = funParams[1].ToString();
                        string nurcoutent = funParams[2].ToString();
                        returnObj = FunctionClass.SaveNurData(parentid, hotid, nurcoutent);
                        break;
                    case funCode.UpdateNurData:
                        string upid = funParams[0].ToString();
                        string uparentid = funParams[1].ToString();
                        string uhotid = funParams[2].ToString();
                        string unurcoutent = funParams[3].ToString();
                        returnObj = FunctionClass.UpdateNurData(upid, uparentid, uhotid, unurcoutent);
                        break;
                    case funCode.DeleNurData:
                        string Nurid = funParams[0].ToString();
                        returnObj = FunctionClass.DeleNurData(Nurid);
                        break;
                    case funCode.GetPersonNurData:
                        string personId = funParams[0].ToString();
                        returnObj = FunctionClass.GetPersonNurData(personId);
                        break;
                    case funCode.InputPersonNur:
                        returnObj = FunctionClass.InputPersonNur(funParams[0].ToString(), funParams[1].ToString(), funParams[2].ToString());
                        break;
                    case funCode.GetPNurData:
                        returnObj = FunctionClass.GetPNurData(funParams[0].ToString(), funParams[1].ToString());
                        break;
                    case funCode.GetpushMsgData:
                        returnObj = FunctionClass.GetpushMsgData(funParams[0].ToString());
                        break;
                    case funCode.DelPush:
                        returnObj = FunctionClass.DelPush(funParams[0].ToString());
                        break;
                    case funCode.GetPushCount:
                        returnObj = FunctionClass.GetPushCount(funParams[0].ToString(), funParams[1].ToString(), funParams[2].ToString());
                        break;
                    case funCode.GetdocInfo:
                        returnObj = FunctionClass.GetdocInfo(funParams[0].ToString());
                        break;
                    case funCode.GetPushlist:
                        returnObj = FunctionClass.GetPushlist(funParams[0].ToString());
                        break;
                    case funCode.lookPush:
                        returnObj = FunctionClass.lookPush(funParams[0].ToString(), funParams[1].ToString());
                        break;

                    #region 积分商城

                    case funCode.GetNurDiaryList:
                        string nurdiaryxmls = funParams[0].ToString();
                        returnObj = FunctionClass.GetNurDiaryList(nurdiaryxmls);
                        break;
                    case funCode.GetSportDiaryList:
                        string sportdiaryxmls = funParams[0].ToString();
                        returnObj = FunctionClass.GetSportDiaryList(sportdiaryxmls);
                        break;
                    case funCode.GetTargetDiaryList:
                        string targetdiaryxmls = funParams[0].ToString();
                        returnObj = FunctionClass.GetTargetDiaryList(targetdiaryxmls);
                        break;
                    case funCode.GetTargetAppendList:
                        string targetappendxmls = funParams[0].ToString();
                        returnObj = FunctionClass.GetTargetAppendList(targetappendxmls);
                        break;
                    case funCode.GetLivingList:
                        string lvingdairyxmls = funParams[0].ToString();
                        returnObj = FunctionClass.GetLivingList(lvingdairyxmls);
                        break;
                    case funCode.GetPharmacyList:
                        string pharmacyxmls = funParams[0].ToString();
                        returnObj = FunctionClass.GetPharmacyList(pharmacyxmls);
                        break;
                    case funCode.GetManagermentList:
                        string managermentxmls = funParams[0].ToString();
                        returnObj = FunctionClass.GetManagermentList(managermentxmls);
                        break;
                    case funCode.GetExpenseDetial:
                        string changexmls = funParams[0].ToString();
                        returnObj = FunctionClass.GetExpenseDetial(changexmls);
                        break;
                    case funCode.GetPointsGoodsList:
                        string goodsxmls = funParams[0].ToString();
                        returnObj = FunctionClass.GetPointsGoodsList(goodsxmls);
                        break;

                    case funCode.AddTargetAppend:
                        string targetxml = funParams[0].ToString();
                        returnObj = FunctionClass.AddTargetAppend(targetxml);
                        break;
                    case funCode.MedicalInADD:
                        returnObj = FunctionClass.MedicalInADD(funParams[0].ToString());
                        break;
                    case funCode.GetMedicalIn:
                        returnObj = FunctionClass.GetMedicalIn(funParams[0].ToString());
                        break;
                    case funCode.delMedicalIn:
                        returnObj = FunctionClass.delMedicalIn(funParams[0].ToString());
                        break;
                    case funCode.GetMedicalInUser:
                        returnObj = FunctionClass.GetMedicalInUser(funParams[0].ToString(), funParams[1].ToString());
                        break;
                    case funCode.AddPharmacyRecord:
                        string pharmacytxml = funParams[0].ToString();
                        returnObj = FunctionClass.AddPharmacyRecord(pharmacytxml);
                        break;
                    case funCode.AddNurDiary:
                        string nurxml = funParams[0].ToString();
                        returnObj = FunctionClass.AddNurDiary(nurxml);
                        break;
                    case funCode.AddSportDiary:
                        string sportxml = funParams[0].ToString();
                        returnObj = FunctionClass.AddSportDiary(sportxml);
                        break;
                    case funCode.AddLivingDiary:
                        string livingxml = funParams[0].ToString();
                        returnObj = FunctionClass.AddLivingDiary(livingxml);
                        break;
                    case funCode.UpdateStatePT:
                        string states = funParams[0].ToString();
                        string detailID = funParams[1].ToString();
                        returnObj = FunctionClass.UpdateStatePT(states, detailID);
                        break;
                    case funCode.CreatePointsUser:
                        string userid = funParams[0].ToString();
                        returnObj = FunctionClass.CreatePointsUser(userid);
                        break;

                    #endregion

                    #region 指标录入

                    case funCode.medicQuery:
                        returnObj = FunctionClass.medicQuery();
                        break;
                    case funCode.medicadd:
                        returnObj = FunctionClass.addmedic(funParams[0].ToString());
                        break;
                    case funCode.delmedic:
                        returnObj = FunctionClass.delmedic(funParams[0].ToString());
                        break;
                    case funCode.updatemedic:
                        returnObj = FunctionClass.updatemedic(funParams[0].ToString());
                        break;
                    case funCode.checkname:
                        returnObj = FunctionClass.checkname(funParams[0].ToString());
                        break;

                    #endregion

                    #region 新报告数据读取

                    case funCode.getTangniao:
                        returnObj = FunctionClass.getTangniao(funParams[0].ToString(), funParams[1].ToString(), funParams[2].ToString());
                        break;
                    case funCode.getTest:
                        returnObj = FunctionClass.getTest(funParams[0].ToString(), funParams[1].ToString(), funParams[2].ToString());
                        break;
                    case funCode.reportIn:
                        returnObj = FunctionClass.reportIn(funParams[0].ToString(), funParams[1].ToString(), funParams[2].ToString(), funParams[3].ToString(),
                            funParams[4].ToString(), funParams[5].ToString());
                        break;
                    case funCode.reportUpdate:
                        returnObj = FunctionClass.reportUpdate(funParams[0].ToString(), funParams[1].ToString(), funParams[2].ToString());
                        break;
                    case funCode.getFeiPang:
                        returnObj = FunctionClass.getFeiPang(funParams[0].ToString(), funParams[1].ToString(), funParams[2].ToString());
                        break;
                    case funCode.getScreenData:
                        returnObj = FunctionClass.getScreenData(funParams[0].ToString(), funParams[1].ToString(), funParams[2].ToString());
                        break;

                    #endregion

                    #region 微信

                    case funCode.PushAddWxMsg:
                        returnObj = FunctionClass.PushAddWxMsg(funParams[0].ToString());
                        break;
                    case funCode.PushAddWeiXinAnswer:
                        returnObj = FunctionClass.PushAddWeiXinAnswer(funParams[0].ToString());
                        break;
                    case funCode.GetBindId:
                        returnObj = FunctionClass.GetBindId(funParams[0].ToString());
                        break;

                    #endregion

                    case funCode.SaveActionPlan: //保存健康行动计划
                        returnObj = FunctionClass.SaveActionPlan(funParams);
                        break;
                }

                #endregion
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog("InvokeMain错误 funCode:" + funCode, ex);
                returnObj = ex.Message;
            }

            return returnObj;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        private string getIPAddress()
        {
            System.Net.IPAddress ip = System.Runtime.Remoting.Messaging.CallContext.GetData("ClientIPAddress") as System.Net.IPAddress;
            if (ip != null)
            {
                return ip.ToString();
            }

            return null;
        }

        /// <summary>
        /// 调用事件
        /// </summary>
        /// <param name="fun"></param>
        private void InvokeEvent(funCode fun, string docid, string docloginid, object[] args)
        {
            DocInfo doc = null;
            if (!string.IsNullOrWhiteSpace(docid))
            {
                TCPServerClient tsc = TCPServer.Instance.Clients.FirstOrDefault(c => c.DocInfo != null && c.DocInfo.doc_id.ToString() == docid);
                if (tsc != null)
                {
                    doc = tsc.DocInfo;
                }
            }

            if (doc == null && !string.IsNullOrWhiteSpace(docloginid))
            {
                TCPServerClient tsc = TCPServer.Instance.Clients.FirstOrDefault(c => c.DocInfo != null && c.DocInfo.doc_loginid == docloginid);
                if (tsc != null)
                {
                    doc = tsc.DocInfo;
                }
            }

            string docName = "";
            if (doc != null)
            {
                docName = doc.doc_name + "(" + doc.doc_id + ")";
            }

            StringBuilder sbArg = new StringBuilder();
            if (args.Length > 0)
            {
                foreach (object o in args)
                {
                    if (o != null)
                    {
                        if (o is string)
                            sbArg.Append(o + " ");
                        else
                            sbArg.Append(JsonConvert.SerializeObject(o) + " ");
                    }
                }
            }

            string msg = DateTime.Now.ToFormatTimeStr() + " [" + getIPAddress() + "] " + docName + "-> " + fun;
            if (sbArg.Length > 0)
                msg = msg + ":" + sbArg;
            LogHelper.WriteInfo(msg);
            if (msg.Length > 90)
                msg = msg.Substring(0, 90) + "...";

            if (OnInvokedMain != null)
            {
                OnInvokedMain(msg);
            }
        }

        #endregion
    }
}
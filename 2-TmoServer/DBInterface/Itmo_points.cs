using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBInterface
{
    public interface Itmo_points
    {
        bool CreatePointsUser(string user_id);

        bool DelPointsUser(string user_id);

        DataSet GetDetailPoints(DataTable dtQuery);

        string SignDayly(string user_id);

        int GetSignDays(string user_id);

        DataSet GetPointsDate(string user_code);


        DataSet GetPointsRank();
        int GetUserCount();
        int GetUserRank(string user_id);
        DataSet GetPointsGoods(string goods_id);

        string AddNurDiary(DataSet dsNurDiary);
        string AddSportDiary(DataSet dsSportDiary);
        string AddManagement(DataSet dsManagement);

        string BindDevice(string user_id, string device_id, string device_type);
        string BindMobile(string user_id, string mobile);

        string AddTargetDiary(DataSet dsTargetDiary);
        string AddTargetAppend(DataSet dsTargetDiary);

        string AddPharmacyRecord(DataSet dsTargetDiary);
        string AddLivingDiary(DataSet dsTargetDiary);
        string FriendsShare(string  user_id);

        bool UpdateStatePT(string state, string detail_id);

        #region 查询消费明细
        DataSet GetExpenseDetial(DataTable dtQuery);
        #endregion

        #region 查询购物车
        DataSet GetAhoppingChat(DataTable dtQuery);
        #endregion

        #region 删除购物车
        bool DelShoppingChat(string detail_id);
        #endregion

        #region 下单
        bool AddShoppingChat(string user_id, string goods_id, string goods_num);
        #endregion

        #region 结算
        bool DealShoppingChat(string detail_id, string address_id);
        #endregion

        #region 每日日志
        bool isSignToday(string user_id);
        bool isNurToday(string user_id);
        bool isSportToday(string user_id);
        bool isTargetToday(string user_id);
        bool isTargetAppendToday(string user_id);
        bool isManagerToday(string user_id);
        bool isPharmacyToday(string user_id);
        bool isLivingToday(string user_id);

        DataSet GetNurDiaryList(DataTable dtQuery);
        DataSet GetSportDiaryList(DataTable dtQuery);
        DataSet GetTargetDiaryList(DataTable dtQuery);
        DataSet GetTargetAppendList(DataTable dtQuery);
        DataSet GetManagermentList(DataTable dtQuery);
        DataSet GetPharmacyList(DataTable dtQuery);
        DataSet GetLivingList(DataTable dtQuery);

        DataSet SelectDiaryPublic(string table, string user_id, string time);
        
        #endregion
    }
}

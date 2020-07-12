using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;

namespace DBBLL
{
    public class tmo_pointsManager : Itmo_points
    {
        #region 单例模式
        private static tmo_pointsManager _instance = null;
        public static tmo_pointsManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_pointsManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_points dal = null;
        #endregion

        #region 构造函数
        public tmo_pointsManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_points>();
        }
        #endregion

        #region 创建积分用户(注册积分)
        public bool CreatePointsUser(string user_id)
        {
            return dal.CreatePointsUser(user_id);
        }
        #endregion

        #region 删除积分用户
        public bool DelPointsUser(string user_id)
        {
            return dal.DelPointsUser(user_id);
        }
        #endregion

        #region 获取用户详细列表
        public DataSet GetDetailPoints(DataTable dtQuery)
        {
            return dal.GetDetailPoints(dtQuery);
        }
        #endregion

        #region 签到积分
        public string SignDayly(string user_id)
        {
            return dal.SignDayly(user_id);
        }
        #endregion

        #region 膳食日志积分
        public string AddNurDiary(DataSet dsNurDiary)
        {
            return dal.AddNurDiary(dsNurDiary);
        }
        #endregion

        #region 运动日志积分
        public string AddSportDiary(DataSet dsSportDiary)
        {
            return dal.AddSportDiary(dsSportDiary);
        }
        #endregion

        #region 管理建议积分
        public string AddManagement(DataSet dsManagement)
        {
            return dal.AddManagement(dsManagement);
        }
        #endregion

        #region 用药记录积分
        public string AddPharmacyRecord(DataSet dsManagement)
        {
            return dal.AddPharmacyRecord(dsManagement);
        }
        #endregion

        #region 指标日志积分
        public string AddTargetDiary(DataSet dsTargetDiary)
        {
            return dal.AddTargetDiary(dsTargetDiary);
        }
        #endregion

        #region 指标补充积分
        public string AddTargetAppend(DataSet dsTargetDiary)
        {
            return dal.AddTargetAppend(dsTargetDiary);
        }
        #endregion

        #region 起居记录积分
        public string AddLivingDiary(DataSet dsTargetDiary)
        {
            return dal.AddLivingDiary(dsTargetDiary);
        }
        #endregion

        #region 设备绑定积分
        public string BindDevice(string user_id, string device_id, string device_type)
        {
            return dal.BindDevice(user_id, device_id, device_type);
        }
        #endregion

        #region 手机绑定积分
        public string BindMobile(string user_id, string mobile)
        {
            return dal.BindMobile(user_id, mobile);
        }
        #endregion

        #region 分享好友积分
        public string FriendsShare(string user_id)
        {
            return dal.FriendsShare(user_id);
        }
        #endregion

        #region 商城兑换
        #region 下单
        public bool AddShoppingChat(string user_id, string goods_id, string goods_num)
        {
            return dal.AddShoppingChat(user_id, goods_id, goods_num);
        }
        #endregion
        #region 结算
        public bool DealShoppingChat(string detail_id, string address_id)
        {
            return dal.DealShoppingChat(detail_id, address_id);
        }
        #endregion
        #endregion

        #region 获取连续签到天数
        public int GetSignDays(string user_id)
        {
            return dal.GetSignDays(user_id);
        }
        #endregion

        #region 查询积分信息
        public DataSet GetPointsDate(string user_code)
        {
            return dal.GetPointsDate(user_code);
        }
        #endregion

        #region 查询消费信息
        public DataSet GetExpenseDetial(DataTable dtQuery)
        {
            return dal.GetExpenseDetial(dtQuery);
        }
        #endregion

        #region 修改发货，收货状态
        public bool UpdateStatePT(string state, string detail_id)
        {
            return dal.UpdateStatePT(state, detail_id);
        }
        #endregion

        #region 查询购物车信息
        public DataSet GetAhoppingChat(DataTable dtQuery)
        {
            return dal.GetAhoppingChat(dtQuery);
        }
        #endregion

        #region 删除购物车信息
        public bool DelShoppingChat(string detail_id)
        {
            return dal.DelShoppingChat(detail_id);
        }
        #endregion

        #region 查询今天是否签到
        public bool isSignToday(string user_id)
        {
            return dal.isSignToday(user_id);
        }
        #endregion

        #region 查询积分排行
        public DataSet GetPointsRank()
        {
            return dal.GetPointsRank();
        }
        #endregion

        #region 查询用户总数
        public int GetUserCount()
        {
            return dal.GetUserCount();
        }
        #endregion

        #region 查询用户排名
        public int GetUserRank(string user_id)
        {
            return dal.GetUserRank(user_id);
        }
        #endregion

        #region 查询商品信息
        public DataSet GetPointsGoods(string goods_id)
        {
            return dal.GetPointsGoods(goods_id);
        }
        #endregion

        #region 今天是否填写膳食日志
        public bool isNurToday(string user_id)
        {
            return dal.isNurToday(user_id);
        }
        #endregion

        #region 今天是否填写运动日志
        public bool isSportToday(string user_id)
        {
            return dal.isSportToday(user_id);
        }
        #endregion

        #region 今天是否填写指标日志
        public bool isTargetToday(string user_id)
        {
            return dal.isTargetToday(user_id);
        }
        #endregion

        #region 今天是否填写指标补充
        public bool isTargetAppendToday(string user_id)
        {
            return dal.isTargetAppendToday(user_id);
        }
        #endregion

        #region 今天是否填写管理建议
        public bool isManagerToday(string user_id)
        {
            return dal.isManagerToday(user_id);
        }
        #endregion

        #region 今天是否填写用药记录
        public bool isPharmacyToday(string user_id)
        {
            return dal.isPharmacyToday(user_id);
        }
        #endregion

        #region 今天是否填写起居记录
        public bool isLivingToday(string user_id)
        {
            return dal.isLivingToday(user_id);
        }
        #endregion

        #region 膳食日志列表查询
        public DataSet GetNurDiaryList(DataTable dtQuery)
        {
            return dal.GetNurDiaryList(dtQuery);
        }
        #endregion

        #region 运动日志列表查询
        public DataSet GetSportDiaryList(DataTable dtQuery)
        {
            return dal.GetSportDiaryList(dtQuery);
        }
        #endregion

        #region 指标日志列表查询
        public DataSet GetTargetDiaryList(DataTable dtQuery)
        {
            return dal.GetTargetDiaryList(dtQuery);
        }
        #endregion

        #region 指标补充列表查询
        public DataSet GetTargetAppendList(DataTable dtQuery)
        {
            return dal.GetTargetAppendList(dtQuery);
        }
        #endregion

        #region 用药记录列表查询
        public DataSet GetPharmacyList(DataTable dtQuery)
        {
            return dal.GetPharmacyList(dtQuery);
        }
        #endregion

        #region 管理建议列表查询
        public DataSet GetManagermentList(DataTable dtQuery)
        {
            return dal.GetManagermentList(dtQuery);
        }
        #endregion

        #region 起居记录列表查询
        public DataSet GetLivingList(DataTable dtQuery)
        {
            return dal.GetLivingList(dtQuery);
        }
        #endregion

        #region 通用记录查询
        public DataSet SelectDiaryPublic(string table, string user_id, string time)
        {
            return dal.SelectDiaryPublic(table, user_id, time);
        }
        #endregion

    }
}

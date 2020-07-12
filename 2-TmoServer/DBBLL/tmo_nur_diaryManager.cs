using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;

namespace DBBLL
{
    public class tmo_nur_diaryManager : Itmo_nur_diary
    {
        #region 单例模式
        private static tmo_nur_diaryManager _instance;

        public static tmo_nur_diaryManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new tmo_nur_diaryManager();
                }
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_nur_diary dal = null;
        #endregion

        #region 构造函数
        public tmo_nur_diaryManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_nur_diary>();
        }
        #endregion

        #region 方法
        public bool AddNurDiary(DataSet dsNurDiary)
        {
            return dal.AddNurDiary(dsNurDiary);
        }

        public bool UpdataNurDiary(DataSet dsNurDiary)
        {
            return dal.UpdataNurDiary(dsNurDiary);
        }

        public DataSet GetNurDiary(string user_id, string diary_date)
        {
            return dal.GetNurDiary(user_id, diary_date);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;
using DBUtility.MySQL;

namespace DBDAL.MySqlDal
{
    public class tmo_nur_diaryDal:Itmo_nur_diary
    {
        public bool AddNurDiary(DataSet dsNurDiary)
        {

            return true;
        }

        public bool UpdataNurDiary(DataSet dsNurDiary)
        {
            return true;
        }

        public DataSet GetNurDiary(string user_id, string diary_date)
        {
            string sql = "select nur_guid,diary_date,user_id,diary_content,staple_food,coarse_food,vegetable,fruit,pickles,cure,fry,soy_salt,cook,meal_num,points from tmo_nur_diary where user_id='" + user_id + "' and diary_date='"+diary_date+"'";
            DataSet ds = MySQLHelper.Query(sql);
            return ds;
        }
       
    }
}

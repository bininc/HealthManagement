using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using DBModel;
using DBUtility.MySQL;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    public class tmo_interveneDal : Itmo_intervene
    {
        public bool SetInterveneFailed(string inte_id, string inte_reason)
        {
            //干预执行状态 1 - 未执行 2 - 执行中 3 - 执行成功 4 - 执行失败
            if (string.IsNullOrWhiteSpace(inte_id)) return false;
            string sql = string.Format("update tmo_intervene set inte_status='4',inte_reason='{0}' where inte_id='{1}'", inte_reason, inte_id);
            int count = MySQLHelper.ExecuteSql(sql);
            return count > 0;
        }

        public bool SetInterveneSuccess(string inte_id)
        {
            //干预执行状态 1 - 未执行 2 - 执行中 3 - 执行成功 4 - 执行失败
            if (string.IsNullOrWhiteSpace(inte_id)) return false;
            string sql = string.Format("update tmo_intervene set inte_status='3',inte_exectime=sysdate(),inte_reason='' where inte_id='{0}'", inte_id);
            int count = MySQLHelper.ExecuteSql(sql);
            return count > 0;
        }

        public bool SetInterveneExecing(string inte_id)
        {
            //干预执行状态 1 - 未执行 2 - 执行中 3 - 执行成功 4 - 执行失败
            if (string.IsNullOrWhiteSpace(inte_id)) return false;
            string sql = string.Format("update tmo_intervene set inte_status='2' where inte_id='{0}'", inte_id);
            int count = MySQLHelper.ExecuteSql(sql);
            return count > 0;
        }


        public bool AddIntervene(DBModel.tmo_intervene model)
        {
            if (model == null) return false;
            model.input_time = DateTime.Now;
            List<tmo_intervene> list = new List<tmo_intervene>();
            List<string> users = StringPlus.GetStrArray(model.user_id, ",");
            List<string> address = StringPlus.GetStrArray(model.inte_addr, ",");
            if (users.Count > 1)
            {
                for (var i = 0; i < users.Count; i++)
                {
                    tmo_intervene newmodel = TmoShare.DeepCopy<tmo_intervene>(model);
                    newmodel.inte_id = TmoShare.GetGuidString();
                    newmodel.user_id = users[i];
                    newmodel.inte_addr = address[i];
                    list.Add(newmodel);
                }
            }
            else
            {
                list.Add(model);
            }

            var dics = ModelConvertHelper<tmo_intervene>.ConvertModelToDictionaries(list);
            return MySQLHelper.AddDatas("tmo_intervene", dics);
        }
    }
}

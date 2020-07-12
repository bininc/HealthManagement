using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBInterface
{
    public interface Itmo_nur_diary
    {
        bool AddNurDiary(DataSet dsNurDiary);

        bool UpdataNurDiary(DataSet dsNurDiary);

        DataSet GetNurDiary(string user_id, string diary_date);
    }
}

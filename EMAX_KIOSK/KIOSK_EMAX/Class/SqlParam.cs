using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIOSK_EMAX
{
    public class SqlParam
    {
        public string sProcName = "";
        private DataTable _dt = new DataTable();

        public SqlParam(string sProcName)
        {
            this.sProcName = sProcName;

            _dt.Columns.Add("param");
            _dt.Columns.Add("value");
            _dt.Columns["param"].DataType = typeof(string);
            _dt.Columns["value"].DataType = typeof(object);
        }

        public void AddParam(string sParam, object sValue)
        {
            if (sProcName == "")
                return;

            DataRow row = _dt.NewRow();
            row["param"] = sParam;
            row["value"] = sValue;

            _dt.Rows.Add(row);
        }

        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("EXEC " + sProcName);

            if (_dt == null)
            {
                return "";
            }
            else if (_dt.Rows.Count == 0)
            {
                return sb.ToString();
            }

            for(int i = 0; i < _dt.Rows.Count; i++)
            {
                sb.Append(" @" + _dt.Rows[i][0].ToString() + "= '" + _dt.Rows[i][1].ToString() + "',");
            }

            //마지막 "," 제외
            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public SqlParameter[] SetSqlParameter()
        {
            SqlParameter[] SqlParameterList = new SqlParameter[_dt.Rows.Count];

            for(int i = 0; i < _dt.Rows.Count; i++)
            {
                SqlParameter SqlParameter = new SqlParameter(_dt.Rows[i][0].ToString(), _dt.Rows[i][1]);

                SqlParameterList[i] = SqlParameter;
            }

            return SqlParameterList;
        }
    }
}

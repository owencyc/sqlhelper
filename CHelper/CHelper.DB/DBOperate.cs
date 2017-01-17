using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHelper.DB
{
    public class DBOperate
    {
        public string ConnectionStringName;
        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        public DataTable GetData_Page(int pageSize,int pageIndex, string tableName, out int TotalNum)
        {
            Database db = DatabaseFactory.CreateDatabase(GetConnectionString("SQL_String"));
            DbCommand com = db.GetStoredProcCommand("DBPager");
            db.AddInParameter(com, "@pageSize", DbType.Int32, pageSize);
            db.AddInParameter(com, "@pageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(com, "@tableName", DbType.String, tableName);
            db.AddOutParameter(com, "@TotalNum", DbType.Int32,4);
            DataTable dt=db.ExecuteDataSet(com).Tables[0];
            TotalNum = Convert.ToInt32(com.Parameters["@TotalNum"].Value);
            return dt;
        }
    }
}

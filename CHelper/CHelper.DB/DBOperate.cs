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
    class DBOperate
    {
        public string ConnectionStringName;
        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        public void GetData_Page()
        {
            Database db = DatabaseFactory.CreateDatabase(GetConnectionString("SQL_String"));
            DbCommand com = db.GetStoredProcCommand("AddUserInfo");
            db.AddInParameter(com, "@UserName", DbType.String, "zgshi");
            db.AddInParameter(com, "@Pwd", DbType.String, "123456");
            db.ExecuteNonQuery(com);

        }
    }
}

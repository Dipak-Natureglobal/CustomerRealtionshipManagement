using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace CustomerRealtionshipManagement.Models
{
    public static class DapperORM
    {
        private static string connectionstring = "Server=service11carchat24test.chatlead.com,15387; Database=Dipak;User id=Colonist8084; Password=a@QYxYg6fSfxAAL#;TrustServerCertificate=True";
        public static void ExecuteWithoutReturn(string ProcedureName, DynamicParameters para = null)
        {

            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                sqlcon.Execute(ProcedureName, para, commandType: CommandType.StoredProcedure);

            }
        }
    
        public static T ExecuteReturnScalar<T>(string ProcedureName, DynamicParameters para = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                return (T)Convert.ChangeType(sqlcon.Execute(ProcedureName, para, commandType: CommandType.StoredProcedure), typeof(T));
            }
        }
        public static IEnumerable<T> ReturnList<T>(string ProcedureName, DynamicParameters para = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                return sqlcon.Query<T>(ProcedureName, para, commandType: CommandType.StoredProcedure);
            }
        }

    }
}


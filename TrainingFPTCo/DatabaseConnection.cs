using Microsoft.Data.SqlClient;

namespace TrainingFPTCo
{
    public class DatabaseConnection
    {
        public static string GetStrConnection()
        {
            string strConnection = @"Data Source=NGUYENTHANH63B6;Initial Catalog=Training;Integrated Security=True;Trust Server Certificate=True;";
            return strConnection;
        }

        public static SqlConnection GetSqlConnection()
        {
            string strConnection = DatabaseConnection.GetStrConnection();
            SqlConnection connection = new SqlConnection(strConnection);
            return connection;
        }
    }
}

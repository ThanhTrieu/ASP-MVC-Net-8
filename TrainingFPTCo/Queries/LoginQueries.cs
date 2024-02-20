using Microsoft.Data.SqlClient;
using TrainingFPTCo.Models;

namespace TrainingFPTCo.Queries
{
    public class LoginQueries
    {
        // chua cac logic sql xu ly voi database
        public LoginViewModel CheckLoginUser(string? username, string? password)
        {
            LoginViewModel dataUser = new LoginViewModel();
            using (SqlConnection conn = DatabaseConnection.GetSqlConnection())
            {
                string querySql = "SELECT * FROM users WHERE username = @username AND password = @password";
                // @username va @password : tham so truyen vao cau lenh sql voi gia tri dc nhan tu 2 bien string username va string password

                // tao 1 doi tuong command de thuc thi cau lenh sql
                SqlCommand cmd = new SqlCommand(querySql, conn);
                // xu ly truyen gia tri vao cho tham so trong sql
                cmd.Parameters.AddWithValue("@username", username ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@password", password ?? (object)DBNull.Value);
                // mo ket noi toi database
                conn.Open();
                // thuc thi lenh sql
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // do du lieu tu table trong database vao model minh da dinh nghia
                        dataUser.Id = reader["Id"].ToString();
                        dataUser.UserName = reader["Username"].ToString();
                        dataUser.Email = reader["Email"].ToString();
                        dataUser.RolesId = reader["RolesId"].ToString();
                        dataUser.Phone = reader["Phone"].ToString();
                        dataUser.ExtraCode = reader["ExtraCode"].ToString();
                        dataUser.FullName = reader["FullName"].ToString();
                    }
                    // ngat ket noi toi database
                    conn.Close();
                }
            }
            return dataUser;
        }
    }
}

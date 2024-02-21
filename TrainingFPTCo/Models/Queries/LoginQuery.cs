using Microsoft.Data.SqlClient;

namespace TrainingFPTCo.Models.Queries
{
    public class LoginQuery
    {
        // viet ham kiem tra dang nhap vao he thong
        public LoginViewModel CheckUserLogin(string? username, string? password)
        {
            LoginViewModel dataUser = new LoginViewModel();
            using (SqlConnection conn = Database.GetSqlConnection())
            {
                string querySql = "SELECT * FROM [Users] WHERE [Username] = @username AND [Password] = @password";
                // @usernam va @password : tham so truyen cau lenh sql
                SqlCommand cmd = new SqlCommand(querySql, conn);
                cmd.Parameters.AddWithValue("@username", username ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@password", password ?? (object)DBNull.Value);
                // mo ket noi database
                conn.Open();
                // thuc thi cau lenh sql
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // do du lieu thu table users sang model LoginViewModel
                        dataUser.Id = reader["Id"].ToString();
                        dataUser.UserName = reader["Username"].ToString();
                        dataUser.ExtraCode = reader["ExtraCode"].ToString();
                        dataUser.Email = reader["Email"].ToString();
                        dataUser.RolesId = reader["RolesId"].ToString();
                        dataUser.Phone = reader["Phone"].ToString();
                        dataUser.FullName = reader["FullName"].ToString();
                    }
                    // ngat ket noi database
                    conn.Close();
                }
            }
            return dataUser;
        }
    }
}

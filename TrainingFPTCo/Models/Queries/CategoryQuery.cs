using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrainingFPTCo.Models.Queries
{
    public class CategoryQuery
    {
        public int InsertItemCategory(
            string nameCategory,
            string description,
            string image,
            string status
        ) {
            int lastInsertId = 0;
            string sqlInsertCategory = "INSERT INTO [Categories]([Name],[Description], [PosterImage], [ParentId], [Status], [CreatedAt]) VALUES(@nameCategory, @description, @image, @parentId, @status, @createdAt) SELECT SCOPE_IDENTITY()";
            // SELECT SCOPE_IDENTITY(): lay ra id vua dc them moi.
            using (SqlConnection connection = Database.GetSqlConnection())
            {
                connection.Open(); // mo ket noi database
                SqlCommand command = new SqlCommand( sqlInsertCategory, connection );
                command.Parameters.AddWithValue("@nameCategory", nameCategory ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@description", description ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@image", image ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@parentId", 0);
                command.Parameters.AddWithValue("@status", status ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@createdAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                lastInsertId = Convert.ToInt32( command.ExecuteScalar() );
                connection.Close();// ngat ket noi database
            }
            return lastInsertId;
        }

        public List<CategoryDetail> GetAllCategories()
        {
            List<CategoryDetail> category = new List<CategoryDetail>();

            using (SqlConnection conn = Database.GetSqlConnection())
            {
                string sqlData = "SELECT * FROM [Categories]";
                conn.Open();
                SqlCommand command = new SqlCommand(sqlData, conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CategoryDetail cate = new CategoryDetail();
                        cate.Id = Convert.ToInt32(reader["Id"]);
                        cate.Name = reader["Name"].ToString();
                        cate.Description = reader["Description"].ToString();
                        cate.PosterNameImage = reader["PosterImage"].ToString();
                        cate.Status = reader["Status"].ToString();
                        cate.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                        category.Add(cate);
                    }
                    conn.Close();
                }
            }
            return category;
        }
    }
}

using Microsoft.Data.SqlClient;

namespace TrainingFPTCo.Models.Queries
{
    public class CategoryQuery
    {
        public bool UpdateCategoryById (
            string nameCategory,
            string description,
            string image,
            string status,
            int id
        )
        {
            bool statusUpdate = false;
            using (SqlConnection connection = Database.GetSqlConnection())
            {
                string sqlQuery = "UPDATE [Categories] SET [Name] = @nameCategory, [Description] = @description, [PosterImage] = @posterImage, [Status] = @status, [UpdatedAt] = @updatedAt WHERE [Id] = @id AND [DeletedAt] IS NULL";
                connection.Open();
                SqlCommand cmd = new SqlCommand( sqlQuery, connection );
                cmd.Parameters.AddWithValue("@nameCategory", nameCategory ?? DBNull.Value.ToString());
                cmd.Parameters.AddWithValue("@description", description ?? DBNull.Value.ToString());
                cmd.Parameters.AddWithValue("@posterImage", image ?? DBNull.Value.ToString());
                cmd.Parameters.AddWithValue("@status", status ?? DBNull.Value.ToString());
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@updatedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
                connection.Close();
                statusUpdate = true;
            }
            return statusUpdate;
        }

        public bool DeleteItemCatgoryById(int id = 0)
        {
            bool statusDelete = false;
            using (SqlConnection connection = Database.GetSqlConnection())
            {
                // khong xoa cung du lieu
                // xoa mem du lieu, danh dau truong DeletedAt(update sql)
                string sqlQuery = "UPDATE [Categories] SET [DeletedAt] = @deletedAt WHERE [Id] = @id";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@deletedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.ExecuteNonQuery(); // thuc thi cau lenh sql
                connection.Close(); // ngat ket noi toi database
                statusDelete = true;
            }
            return statusDelete;
        }

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

        public CategoryDetail GetDataCategoryById(int id = 0)
        {
            CategoryDetail categoryDetail = new CategoryDetail();
            using (SqlConnection connection = Database.GetSqlConnection())
            {
                string sqlQuery = "SELECT * FROM [Categories] WHERE [Id] = @id AND [DeletedAt] IS NULL";
                connection.Open(); // mo ket noi toi database
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categoryDetail.Id = Convert.ToInt32(reader["Id"]);
                        categoryDetail.Name = reader["Name"].ToString();
                        categoryDetail.Description = reader["Description"].ToString();
                        categoryDetail.PosterNameImage = reader["PosterImage"].ToString();
                        categoryDetail.Status = reader["Status"].ToString();
                    }
                    connection.Close(); // ngat ket noi toi database
                }
            }
            return categoryDetail;
        }

        public List<CategoryDetail> GetAllCategories(string? SearchString, string? FilterStatus)
        {
            List<CategoryDetail> category = new List<CategoryDetail>();

            using (SqlConnection conn = Database.GetSqlConnection())
            {
                // chi lay ra danh sach khong bi xoa
                string sqlData = string.Empty;
                if (FilterStatus != null)
                {
                    sqlData = "SELECT * FROM [Categories] WHERE [Name] LIKE @keyWord AND [DeletedAt] IS NULL AND [Status] = @status";
                }
                else
                {
                    sqlData = "SELECT * FROM [Categories] WHERE [Name] LIKE @keyWord AND [DeletedAt] IS NULL";
                }
                conn.Open();
                SqlCommand command = new SqlCommand(sqlData, conn);
                command.Parameters.AddWithValue("@keyWord", "%"+SearchString+"%" ?? DBNull.Value.ToString());
                if (FilterStatus != null)
                {
                    command.Parameters.AddWithValue("@status", FilterStatus ?? DBNull.Value.ToString());
                }
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

using Microsoft.Data.SqlClient;

namespace TrainingFPTCo.Models.Queries
{
    public class CourseQuery
    {
        public List<CourseDetail> GetAllDataCourses()
        {
            List<CourseDetail> courses = new List<CourseDetail>();
            using (SqlConnection connection = Database.GetSqlConnection())
            {
                string sql = "SELECT * FROM [Courses] WHERE [DeletedAt] IS NULL";
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CourseDetail detail = new CourseDetail();
                    }
                }
            }
            return courses;
        }

        public int InsertCourse(
            string nameCourse,
            int categoryId,
            string? description,
            DateTime startDate,
            DateTime? endDate,
            string status,
            string imageCourse
        )
        {
            int IdCourse = 0;
            using (SqlConnection connection = Database.GetSqlConnection())
            {
                string sqlQuery = "INSERT INTO [Courses]([CategoryId], [Name], [Description],[Image], [StartDate], [EndDate], [Status], [CreatedAt]) VALUES(@CategoryId, @Name, @Description, @Image, @StartDate, @EndDate, @Status, @CreatedAt) SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand( sqlQuery, connection );
                connection.Open();
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                cmd.Parameters.AddWithValue("@Name", nameCourse);
                cmd.Parameters.AddWithValue ("@Description", description ?? DBNull.Value.ToString());
                cmd.Parameters.AddWithValue("@Image", imageCourse);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                IdCourse = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return IdCourse;
        }
    }
}

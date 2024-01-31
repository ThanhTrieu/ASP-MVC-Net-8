using Microsoft.EntityFrameworkCore;

namespace TrainingFPTCo.DBContext
{
    public class TrainingDBContext : DbContext
    {
        public TrainingDBContext(DbContextOptions<TrainingDBContext> options) : base(options) { }

        public DbSet<RolesDBContext> Roles { get; set; }
        public DbSet<CategoryDBContext> Categories { get; set; }
        public DbSet<CourseDBContext> Courses { get; set; }
    }
}

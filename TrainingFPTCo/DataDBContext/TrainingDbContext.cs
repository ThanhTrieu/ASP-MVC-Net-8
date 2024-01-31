using Microsoft.EntityFrameworkCore;

namespace TrainingFPTCo.DataDBContext
{
    public class TrainingDbContext : DbContext
    {
        public TrainingDbContext(DbContextOptions<TrainingDbContext> options) : base(options) { }
    }
}

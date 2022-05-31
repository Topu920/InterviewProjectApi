using Microsoft.EntityFrameworkCore;

namespace InterviewProjectApi.Models
{
    public class DataDbContext:DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Line> Lines { get; set; }
    }
}

using ExampleIdentity.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExampleIdentity.Core.Persistence
{
    public class ExampleEntityContext : DbContext
    {
        public ExampleEntityContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<StudentModel> Student =>Set<StudentModel>();
    }
}

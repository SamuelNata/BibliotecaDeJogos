namespace GameLib.Repository.DbContext
{
    public abstract class BaseDbContext : Microsoft.EntityFrameworkCore.DbContext
    {

        public BaseDbContext() { }
        public BaseDbContext(Microsoft.EntityFrameworkCore.DbContextOptions options) : base(options) {}
    }
}
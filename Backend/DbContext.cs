using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class Db : DbContext
    {
        public Db(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<User> Users => Set<User>();
    }
}

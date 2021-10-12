using Microsoft.EntityFrameworkCore;

namespace mysqltest.Models
{
    public class ClubsContext : DbContext
    {
        public ClubsContext(DbContextOptions options)
          : base(options)
        {
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentClub> StudentClubs { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<VaccinatedUser> VaccinatedUsers { get; set; }
        public DbSet<ClubEvent> ClubEvent { get; set; }
        public DbSet<Employee> Employee { get; set; }
    }
}
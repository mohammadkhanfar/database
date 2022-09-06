using Microsoft.EntityFrameworkCore;

namespace compemp.Models
{
    public class CompEmp : DbContext
    {
        public CompEmp(DbContextOptions<CompEmp> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Company { get; set; }
    }
}

using CoreWebAppApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebAppApi.Data
{
    public class DbCustomer : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=anderson-paula\\SQLEXPRESS;Initial Catalog=CustomerDb;TrustServerCertificate=True;Integrated Security=SSPI;");
        }

       public DbSet<Customer> Customers { get; set; }
    }
}

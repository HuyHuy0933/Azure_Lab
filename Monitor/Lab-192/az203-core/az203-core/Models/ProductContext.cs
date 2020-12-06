using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace az203_core.Models
{
    public class ProductContext : DbContext
    {
        // Replace the database connection string  here
        readonly string connectionstring = "Server=tcp:huyxle.database.windows.net,1433;Initial Catalog=huyxledb;Persist Security Info=False;User ID=huyxle;Password=Huy@0933950159;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(connectionstring);
            base.OnConfiguring(options);
        }

    }
}

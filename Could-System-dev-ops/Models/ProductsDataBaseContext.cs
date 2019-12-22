using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Could_System_dev_ops.Models
{
    public class ProductsDataBaseContext : DbContext
    {
        public DbSet<ProductsModel> Products { get; set; }

        public ProductsDataBaseContext(DbContextOptions<ProductsDataBaseContext> options) : base(options)
        {
            Database.Migrate();
        }
    }
}

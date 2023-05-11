using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Entitiy.Concrete;
using Entity.Concrete;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class NorthwindContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
            //    (@"Server=(localdb)\TestServer; initial catalog=northwind; integrated security=true");
                  (@"Server=(localdb)\MSSQLLocalDB; initial catalog=EGrocery; integrated security=true");

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim > UserOperationClaims { get; set; }

        //public DbSet<Order> Shipping { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;
using System;

namespace GRPCCoreCrudExampleServer
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ProductModel> Products { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

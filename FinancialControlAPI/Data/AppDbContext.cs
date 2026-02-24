using FinancialControlAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialControlAPI.Data
{
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions <AppDbContext> options) 
            : base(options){ } // esse construtor recebe as configurações vinda do program.cs passas essas configurações para o dbcontext base

            public DbSet<User> Users { get; set; }

            public DbSet<Transaction> Transactions { get; set; } 
            
        }
}

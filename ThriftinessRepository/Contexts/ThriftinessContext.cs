using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ThriftinessCore.Entites;

namespace ThriftinessRepository.Contexts
{
    public class ThriftinessContext : DbContext
    {
        public ThriftinessContext(DbContextOptions<ThriftinessContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Expense> Expense { get; set; }
        public DbSet<MonthOfExpense> MonthOfExpense { get; set; }
        public DbSet<SaveGoal> SaveGoal { get; set; }
    }
}
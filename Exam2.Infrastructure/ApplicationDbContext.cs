using Exam2.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam2.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<AboutItem> AboutItems { get; set; }
        private IConfiguration _configuration { get; }

        public ApplicationDbContext(IConfiguration configuration,DbContextOptions options) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MSSQL"));
            base.OnConfiguring(optionsBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var history in this.ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added ||
                    e.State == EntityState.Modified))
                .Select(e => e.Entity as BaseEntity)
                )
            {
                history.UpdatedAt = DateTime.Now;
                if (history.CreatedAt <= DateTime.MinValue)
                {
                    history.CreatedAt = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}

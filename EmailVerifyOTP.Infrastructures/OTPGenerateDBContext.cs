using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using static EmailVerifyOTP.Infrastructures.OTPGenerateDBContext;
using EmailVerifyOTP.Infrastructures.EntityConfig;
using EmailVerifyOTP.Application.Interfaces;
using EmailVerifyOTP.DomainModels.Models;
using EmailVerifyOTP.Application.Model;

namespace EmailVerifyOTP.Infrastructures
{
    public class OTPGenerateDBContext : DbContext, IOtpGenerateDBContext
    {
            public OTPGenerateDBContext(DbContextOptions options) : base(options)
            {
            }

            public DbSet<EmailUserDetails> EmailDetail { get; set; } = null!;

            /// <summary>
            /// OnModelCreating
            /// </summary>
            /// <param name="modelBuilder"></param>
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.ApplyConfiguration(new EmailEntityConfiguration());
                base.OnModelCreating(modelBuilder);
            }

            /// <summary>
            /// SaveChangesAsync
            /// </summary>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
            {

                foreach (EntityEntry<ParentEntity> entry in ChangeTracker.Entries<ParentEntity>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreatedDateTime = DateTime.Now;
                            break;
                    }
                }

                var result = await base.SaveChangesAsync(cancellationToken);
                return result;
            }
        }
}

using EmailVerifyOTP.Application.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EmailVerifyOTP.Infrastructures.EntityConfig
{
    public class EmailEntityConfiguration : IEntityTypeConfiguration<EmailUserDetails>
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<EmailUserDetails> builder)
        {
            builder.HasKey(_ => _.ID);
            builder.Property(_ => _.EmailAddress).HasMaxLength(50);
            builder.Property(_ => _.OtpCode);
            builder.Property(_ => _.CreatedDateTime);
            builder.Property(_ => _.UpdatedDateTime);
            builder.Property(_ => _.RetryCount);
        }
    }
}

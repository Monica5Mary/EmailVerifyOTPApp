using EmailVerifyOTP.Application.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmailVerifyOTP.Application.Interfaces
{
    public interface IOtpGenerateDBContext
    {
        DbSet<EmailUserDetails> EmailDetail { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}

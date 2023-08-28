using System;
using System.Diagnostics.CodeAnalysis;

namespace EmailVerifyOTP.DomainModels.Models
{
    [ExcludeFromCodeCoverage]
    public abstract class ParentEntity
    {
        public Guid ID { get; set; } = new Guid();
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset UpdatedDateTime { get; set; }
    }
}

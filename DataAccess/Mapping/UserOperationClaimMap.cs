using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class UserOperationClaimMap : IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            builder.HasKey(uo => uo.Id);

            builder.HasOne(uo => uo.User)
                   .WithMany(u => u.UserOperationClaims)
                   .HasForeignKey(uo => uo.UserId);

            builder.HasOne(uo => uo.OperationClaim)
                   .WithMany(o => o.UserOperationClaims)
                   .HasForeignKey(uo => uo.OperationClaimId);

            
        }
    }
}

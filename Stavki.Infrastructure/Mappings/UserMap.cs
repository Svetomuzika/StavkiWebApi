using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stavki.Infrastructure.EF.Domains;

namespace Stavki.Infrastructure.Mappings
{
    internal class UserMap : IEntityTypeConfiguration<UserDomain>
    {
        public void Configure(EntityTypeBuilder<UserDomain> builder)
        {
            builder.ToTable("Users").HasKey(p => p.Id);

            builder.HasOne(x => x.UserData)
                .WithOne(c => c.User).HasForeignKey<UserDataDomain>(x => x.UserId);
        }
    }
}

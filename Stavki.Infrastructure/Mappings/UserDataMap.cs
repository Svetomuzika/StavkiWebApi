using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stavki.Infrastructure.EF.Domains;

namespace Stavki.Infrastructure.Mappings
{
    internal class UserDataMap : IEntityTypeConfiguration<UserDataDomain>
    {
        public void Configure(EntityTypeBuilder<UserDataDomain> builder)
        {
            builder.ToTable("UsersData").HasKey(p => p.Id);
        }
    }
}
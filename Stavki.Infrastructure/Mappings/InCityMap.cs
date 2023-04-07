using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stavki.Infrastructure.EF.Domains.Stavki;

namespace Stavki.Infrastructure.Mappings
{
    internal class InCityMap : IEntityTypeConfiguration<InCityDomain>
    {
        public void Configure(EntityTypeBuilder<InCityDomain> builder)
        {
            builder.ToTable("InCity").HasKey(p => p.Id);
        }
    }
}

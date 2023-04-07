using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stavki.Infrastructure.EF.Domains.Stavki;

namespace Stavki.Infrastructure.Mappings
{
    internal class InCityNDSMap : IEntityTypeConfiguration<InCityNDSDomain>
    {
        public void Configure(EntityTypeBuilder<InCityNDSDomain> builder)
        {
            builder.ToTable("InCityNDS").HasKey(p => p.Id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stavki.Infrastructure.EF.Domains;
using Stavki.Infrastructure.EF.Domains.Stavki;

namespace Stavki.Infrastructure.Mappings
{
    internal class NearInCityNDSMap : IEntityTypeConfiguration<NearInCityNDSDomain>
    {
        public void Configure(EntityTypeBuilder<NearInCityNDSDomain> builder)
        {
            builder.ToTable("NearInCityNDS").HasKey(p => p.Id);
        }
    }
}

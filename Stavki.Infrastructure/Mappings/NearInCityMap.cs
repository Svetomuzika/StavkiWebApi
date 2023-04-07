using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stavki.Infrastructure.EF.Domains.Stavki;

namespace Stavki.Infrastructure.Mappings
{
    internal class NearInCityMap : IEntityTypeConfiguration<NearInCityDomain>
    {
        public void Configure(EntityTypeBuilder<NearInCityDomain> builder)
        {
            builder.ToTable("NearInCity").HasKey(p => p.Id);
        }
    }
}

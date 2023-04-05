using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stavki.Infrastructure.EF.Domains;

namespace Stavki.Infrastructure.Mappings
{
    internal class RequestMap : IEntityTypeConfiguration<RequestDomain>
    {
        public void Configure(EntityTypeBuilder<RequestDomain> builder)
        {
            builder.ToTable("Requests").HasKey(p => p.Id);
        }
    }
}

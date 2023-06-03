using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stavki.Infrastructure.EF.Domains;

namespace Stavki.Infrastructure.Mappings
{
    public class NotifyMap : IEntityTypeConfiguration<NotifyDomain>
    {
        public void Configure(EntityTypeBuilder<NotifyDomain> builder)
        {
            builder.ToTable("Notifications").HasKey(p => p.Id);

            builder.HasOne(x => x.Request)
                .WithMany(c => c.Notifications)
                .HasForeignKey(c => c.RequestId);
        }
    }
}

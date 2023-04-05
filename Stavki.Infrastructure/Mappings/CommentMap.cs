using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stavki.Infrastructure.EF.Domains;

namespace Stavki.Infrastructure.Mappings
{
    internal class CommentMap : IEntityTypeConfiguration<CommentDomain>
    {
        public void Configure(EntityTypeBuilder<CommentDomain> builder)
        {
            builder.ToTable("Comments").HasKey(p => p.Id);

            builder.HasOne(x => x.Request)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.RequestId);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stavki.Infrastructure.EF.Domains;

namespace Stavki.Infrastructure.Mappings
{
    internal class RequestHistoryMap : IEntityTypeConfiguration<RequestHistoryDomain>
    {
        public void Configure(EntityTypeBuilder<RequestHistoryDomain> builder)
        {
            builder.ToTable("RequestsHistory").HasKey(p => p.Id);

            builder.HasOne(x => x.Request)
                .WithMany(c => c.RequestHistory)
                .HasForeignKey(c => c.RequestId);

        }
    }
}

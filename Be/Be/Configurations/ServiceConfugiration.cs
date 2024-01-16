using Be.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Be.Configurations
{
    public class ServiceConfugiration:IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.Property(x=>x.FullName).IsRequired().HasMaxLength(256);
            builder.Property(x=>x.Title).IsRequired().HasMaxLength(256);
            builder.Property(x=>x.Facebooklink).IsRequired().HasMaxLength(256);
            builder.Property(x=>x.Twitterlink).IsRequired().HasMaxLength(256);
            builder.Property(x=>x.Googlelink).IsRequired().HasMaxLength(256);
        }
        
    }
}

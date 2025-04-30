using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(i => i.Title).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Price).IsRequired();
            builder.Property(i => i.Category).IsRequired().HasMaxLength(100);
            builder.Property(i => i.Image).IsRequired();
            
            builder.OwnsOne(i => i.Rating, n =>
            {
                n.Property(p => p.Rate);
                n.Property(p => p.Count);
            });
        }
    }
}

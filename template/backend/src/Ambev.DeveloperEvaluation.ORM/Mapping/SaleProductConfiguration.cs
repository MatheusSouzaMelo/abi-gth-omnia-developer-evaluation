using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleProductConfiguration : IEntityTypeConfiguration<SaleProduct>
    {
        public void Configure(EntityTypeBuilder<SaleProduct> builder)
        {
            builder.ToTable("SaleProducts");

            builder.HasKey(sp => sp.Id);
            builder.Property(sp => sp.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            builder.Property(sp => sp.UnitPrice).IsRequired().HasPrecision(18, 2);
            builder.Property(sp => sp.Discount).IsRequired().HasPrecision(18, 2);
            builder.Property(sp => sp.TotalAmount).IsRequired().HasPrecision(18, 2);
            builder.Property(sp => sp.Canceled).IsRequired().HasDefaultValue(false);

            builder.HasOne(sp => sp.CartProduct)
                .WithMany()
                .HasForeignKey(sp => sp.CartProductId)
                .IsRequired();
        }
    }
}

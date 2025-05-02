using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    internal class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.SaleNumber).IsRequired().HasMaxLength(50);
            builder.Property(s => s.SaleDate).IsRequired();
            builder.Property(s => s.CustomerId);
            builder.Property(s => s.Branch).IsRequired().HasMaxLength(50);
            builder.Property(s => s.TotalAmount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(s => s.IsCancelled).IsRequired().HasDefaultValue(false);

            builder.HasMany(s => s.Products)
                .WithOne()
                .HasForeignKey(sp => sp.SaleId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(s => s.Cart)
                .WithOne()
                .HasForeignKey<Sale>(s => s.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Customer).
                WithMany()
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

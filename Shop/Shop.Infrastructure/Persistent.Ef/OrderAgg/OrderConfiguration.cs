using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Persistent.Ef.OrderAgg;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders", "order");

        builder.OwnsOne(a => a.Discount, option =>
           {
               option.Property(b => b.DiscountTitle).HasMaxLength(50);
           });

        builder.OwnsOne(a => a.ShippingMethod, option =>
           {
               option.Property(b => b.ShipppingType).HasMaxLength(50);
           });

        builder.OwnsMany(a => a.Items, option =>
         {
             option.ToTable("Items", "order");
         });

        builder.OwnsOne(a => a.Address, option =>
         {
             option.ToTable("Addresses", "order");

             option.Property(b => b.City)
             .HasMaxLength(35)
             .IsRequired();

             option.Property(b => b.Family)
             .HasMaxLength(50)
             .IsRequired();

             option.Property(b => b.PhoneNumber)
             .HasMaxLength(50)
             .IsRequired();

             option.Property(b => b.Name)
             .HasMaxLength(50)
             .IsRequired();

             option.Property(b => b.NationalCode)
             .HasMaxLength(10)
             .IsRequired();

             option.Property(b => b.PostalCode)
             .HasMaxLength(35)
             .IsRequired();

             option.Property(b => b.PostalAddress)
             .HasMaxLength(100)
             .IsRequired();
         });
    }
}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Persistent.Ef.ProductAgg
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "product");
            builder.HasIndex(a => a.Slug).IsUnique();

            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.Description)
                .IsRequired();

            builder.Property(a => a.ImageName)
                .IsRequired()
                .HasMaxLength(110);

            builder.Property(A => A.Slug)
                .IsRequired()
                .IsUnicode(false);

            builder.OwnsOne(b => b.SeoData, config =>
            {
                config.Property(b => b.MetaDescription)
                    .HasMaxLength(500)
                    .HasColumnName("MetaDescription");

                config.Property(b => b.MetaTitle)
                    .HasMaxLength(500)
                    .HasColumnName("MetaTitle");

                config.Property(b => b.MetaKeyWords)
                    .HasMaxLength(500)
                    .HasColumnName("MetaKeyWords");

                config.Property(b => b.IndexPage)
                    .HasColumnName("IndexPage");

                config.Property(b => b.Canonical)
                    .HasMaxLength(500)
                    .HasColumnName("Canonical");

                config.Property(b => b.Schema)
                    .HasColumnName("Schema");
            });

            builder.OwnsMany(a => a.Images, option =>
           {
               option.ToTable("Images", "product");

               option.Property(a => a.ImageName)
                     .IsRequired()
                     .HasMaxLength(100);
           });

            builder.OwnsMany(a => a.Specifications, option =>
           {
               option.ToTable("Specifications", "product");

               option.Property(a => a.Key)
                     .IsRequired()
                     .HasMaxLength(50);

               option.Property(a => a.Value)
                     .IsRequired()
                     .HasMaxLength(90);
           });
        }
    }
}

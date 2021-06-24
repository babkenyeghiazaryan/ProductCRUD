using InterviewTask.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewTask.Data.Entities
{
    public class Product : IEntityBase
    {
        public Product()
        {
            
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price{ get; set; }
        public bool Available { get; set; }
        public string Description { get; set; }
        public DateTime DateCreatedUTC { get; set; }
    }

    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
           
        }
    }
}

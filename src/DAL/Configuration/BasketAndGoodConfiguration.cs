using System;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    class BasketAndGoodConfiguration : IEntityTypeConfiguration<BasketAndGood>
    {
        public void Configure(EntityTypeBuilder<BasketAndGood> builder)
        {
            builder.HasData
            (
                new BasketAndGood
                {
                }
            );


        }
    }
}

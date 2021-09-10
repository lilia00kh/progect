using System;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasData
            (
                new Basket
                {
                    Id = new Guid("928fa7e1-97ec-4c3f-8d28-7c26ee2231c9"),
                    UserName = "grinch.in.ua@ukr.net"
                }
            );


        }
    }
}

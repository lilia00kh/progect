using System;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class DetailsAboutDeliveryConfiguration : IEntityTypeConfiguration<DetailsAboutDelivery>
    {
        public void Configure(EntityTypeBuilder<DetailsAboutDelivery> builder)
        {
            builder.HasData
            (
                new DetailsAboutDelivery
                {
                }
            );


        }
    }
}

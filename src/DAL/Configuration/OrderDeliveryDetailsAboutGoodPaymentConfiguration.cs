using System;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    class OrderDeliveryDetailsAboutGoodPaymentConfiguration : IEntityTypeConfiguration<OrderDeliveryDetailsAboutGoodPayment>
    {
        public void Configure(EntityTypeBuilder<OrderDeliveryDetailsAboutGoodPayment> builder)
        {
            builder.HasData
            (
                new OrderDeliveryDetailsAboutGoodPayment
                {
                }
            );


        }
    }
}

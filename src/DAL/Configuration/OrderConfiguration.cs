using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DAL.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData
            (
              new Order
              {
                  //Id = new Guid("8d7c2c0a-9628-4700-9cef-472b5691f845"),
                  //User = "lilia00kh@gmail.com",
                  //UserName = "Liliia",
                  //UserEmail = "lilia00kh@gmail.com",
                  //Address = "Horodok",
                  //Phone= "+1111-111-1111",
                  //Status = "new",
                  //BookId = new Guid("e1019ea6-c93e-4fc0-b593-8f5605a1a1ef")
              }

            );


        }
    }
}

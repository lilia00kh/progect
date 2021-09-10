using System;
using System;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class DetailsAboutGoodConfiguration : IEntityTypeConfiguration<DetailsAboutGood>
    {
        public void Configure(EntityTypeBuilder<DetailsAboutGood> builder)
        {
            builder.HasData
            (
                new DetailsAboutGood
                {
                    //Id = new Guid("15c17c09-d39f-46b9-acc9-b306fd5966b3"),
                    //TypeOfGood = "tree",
                    //Count = 1,
                    //Size = 2,
                    //Price = 12
                }
            );


        }
    }
}

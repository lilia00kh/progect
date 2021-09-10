using System;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    class ToyConfiguration : IEntityTypeConfiguration<Toy>
    {
        public void Configure(EntityTypeBuilder<Toy> builder)
        {
            builder.HasData
            (
                new Toy
                {
                    //Id = new Guid("ce420acf-2252-47ef-885c-fe24bdd49bdb"),
                    //Name = "Toy1",
                    //Description = "Very good toy",
                    //Price = 10
                }
            );


        }
    }
}

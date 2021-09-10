using DAL.Configuration;
using DAL.Entities;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class ApplicationDbContext : IdentityDbContext<User>, Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.IDataProtectionKeyContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.ApplyConfiguration(new UserConfiguration());
            //builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new BasketConfiguration());
        }

        public DbSet<Tree> Trees { get; set; }
        public DbSet<TreeSizeAndPrice> TreeSizesAndPrices { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Toy> Toys { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<AnswerToComment> AnswersToComment { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketAndGood> BasketAndGood { get; set; }
        public DbSet<DetailsAboutGood> DetailsAboutGood { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DetailsAboutDelivery> DetailsAboutDelivery { get; set; }
        public DbSet<OrderDeliveryDetailsAboutGoodPayment> OrderDeliveryDetailsAboutGoodPayment { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageAndGood> ImageAndGood { get; set; }
        public DbSet<Recomendation> Recomendations { get; set; }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    }
}

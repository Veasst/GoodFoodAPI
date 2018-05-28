using GoodFoodAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodFoodAPI.Data
{
    public class GoodFoodContext : DbContext
    {
        public GoodFoodContext(DbContextOptions<GoodFoodContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // localDishes
            modelBuilder.Entity<LocalDishes>()
                .HasKey(ld => new { ld.localId, ld.dishId });

            modelBuilder.Entity<LocalDishes>()
                .HasOne(ld => ld.local)
                .WithMany(l => l.localDishes)
                .HasForeignKey(ld => ld.localId);

            modelBuilder.Entity<LocalDishes>()
                .HasOne(ld => ld.dish)
                .WithMany(d => d.localDishes)
                .HasForeignKey(ld => ld.dishId);

            // userDishes
            modelBuilder.Entity<UserDishes>()
                 .HasKey(ld => new { ld.userId, ld.dishId });

            modelBuilder.Entity<UserDishes>()
                .HasOne(ld => ld.user)
                .WithMany(l => l.userDishes)
                .HasForeignKey(ld => ld.userId);

            modelBuilder.Entity<UserDishes>()
                .HasOne(ld => ld.dish)
                .WithMany(d => d.userDishes)
                .HasForeignKey(ld => ld.dishId);

            // userLocals
            modelBuilder.Entity<UserLocals>()
                .HasKey(ld => new { ld.userId, ld.localId });

            modelBuilder.Entity<UserLocals>()
                .HasOne(ld => ld.user)
                .WithMany(l => l.userLocals)
                .HasForeignKey(ld => ld.userId);

            modelBuilder.Entity<UserLocals>()
                .HasOne(ld => ld.local)
                .WithMany(d => d.userLocals)
                .HasForeignKey(ld => ld.localId);

            // orderDishes
            modelBuilder.Entity<OrderDishes>()
                .HasKey(od => new { od.orderId, od.dishId });

            modelBuilder.Entity<OrderDishes>()
                .HasOne(od => od.order)
                .WithMany(o => o.orderDishes)
                .HasForeignKey(od => od.orderId);

            modelBuilder.Entity<OrderDishes>()
                .HasOne(od => od.dish)
                .WithMany(o => o.orderDishes)
                .HasForeignKey(od => od.dishId);

        }

        public DbSet<User> User { get; set; }
        public DbSet<GoodFoodAPI.Models.Dish> Dish { get; set; }
        public DbSet<GoodFoodAPI.Models.Code> Code { get; set; }
        public DbSet<GoodFoodAPI.Models.Local> Local { get; set; }
        public DbSet<GoodFoodAPI.Models.DishType> DishType { get; set; }
        public DbSet<LocalDishes> LocalDishes { get; set; }
        public DbSet<GoodFoodAPI.Models.UserDishes> UserDishes { get; set; }
        public DbSet<GoodFoodAPI.Models.UserLocals> UserLocals { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDishes> OrderDishes {get; set;}
        //public DbSet<GoodFoodAPI.Models.Menu> Menu { get; set; }
    }
}

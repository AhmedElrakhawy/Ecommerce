using ClothBazar.Entities;
using System;
using System.Data.Entity;

namespace ClothBazar.database
{
    public class CBContext : DbContext, IDisposable
    {
        public CBContext() : base("DefaultConnection")
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Config> Configurations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}

using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Security.Domain.Models;
using LearningCenter.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Org.BouncyCastle.Crypto.Paddings;

namespace LearningCenter.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Store> Stores { get; set; }
    public DbSet<StoreImage> StoreImages { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Article> Articles { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        //ARTICLES
        builder.Entity<Article>().ToTable("Articles");
        builder.Entity<Article>().HasKey(p => p.Id);
        builder.Entity<Article>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Article>().Property(p => p.Name).IsRequired();
        builder.Entity<Article>().Property(p => p.Description).IsRequired();
        builder.Entity<Article>().Property(p => p.UserId).IsRequired();

        builder.Entity<Article>()
            .HasOne(a => a.User)
            .WithMany(u => u.ArticlesList)
            .HasForeignKey(a => a.UserId);
        
        
        builder.Entity<Store>().ToTable("Stores");
        builder.Entity<Store>().HasKey(p => p.Id);
        builder.Entity<Store>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Store>().Property(p => p.Name).IsRequired();
        builder.Entity<Store>().Property(p => p.Description).IsRequired();
        builder.Entity<Store>().Property(p => p.Address).IsRequired();
        builder.Entity<Store>().Property(p => p.Encoded64LogoImage).IsRequired();
        
        builder.Entity<Store>()
            .HasMany(s => s.Sales)
            .WithOne(s => s.Store)
            .HasForeignKey(p => p.StoreId);
        
        builder.Entity<Sale>().ToTable("Sales");
        builder.Entity<Sale>().HasKey(p => p.Id);
        builder.Entity<Sale>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Sale>().Property(p => p.PurchaserId).IsRequired();
        builder.Entity<Sale>().Property(p => p.Code).IsRequired();
        builder.Entity<Sale>().Property(p => p.StoreId).IsRequired();
        
        builder.Entity<Sale>()
            .HasMany(s => s.Products)
            .WithMany(p => p.Sales);
        
        builder.Entity<Store>()
            .HasMany(s => s.Products)
            .WithOne(s => s.Store)
            .HasForeignKey(p => p.StoreId);
        
        builder.Entity<Product>().ToTable("Products");
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(150);
        builder.Entity<Product>().Property(p => p.Rating);
        builder.Entity<Product>().Property(p => p.InventoryStatus);
        builder.Entity<Product>().Property(p => p.Price);
        builder.Entity<Product>().Property(p => p.Image);
        builder.Entity<Product>().Property(p => p.Category);
        
        builder.Entity<Store>()
            .HasMany(p => p.StoreImages)
            .WithOne(p => p.Store)
            .HasForeignKey(p => p.StoreId);
        
        builder.Entity<StoreImage>().ToTable("StoreImages");
        builder.Entity<StoreImage>().HasKey(p => p.Id);
        builder.Entity<StoreImage>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<StoreImage>().Property(p => p.Enconded64Image).IsRequired();

        
        // SECURITY
        builder.Entity<Role>().ToTable("Roles");
        builder.Entity<Role>().HasKey(r => r.Id);
        builder.Entity<Role>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Role>().Property(r => r.Name).IsRequired().HasMaxLength(30);
        
        // Relationships
        builder.Entity<Role>()
            .HasMany(r => r.Users)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId);
        
        // Relationships
        builder.Entity<User>()
            .HasMany(p => p.Stores)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        
        builder.Entity<User>()
            .HasMany(p => p.Purchases)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        
        // Constraints
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p => p.Username).IsRequired().HasMaxLength(30);
        builder.Entity<User>().Property(p => p.FirstName).IsRequired();
        builder.Entity<User>().Property(p => p.LastName).IsRequired();
        
        builder.Entity<Purchase>().ToTable("Purchases");
        builder.Entity<Purchase>().HasKey(p => p.Id);
        builder.Entity<Purchase>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Purchase>().Property(p => p.Code).IsRequired().HasMaxLength(30);

        builder.Entity<Purchase>()
            .HasMany(p => p.Products)
            .WithMany(p => p.Purchases);
        

        // Apply Snake Case Naming Convention
        
        builder.UseSnakeCaseNamingConvention();
    }
}
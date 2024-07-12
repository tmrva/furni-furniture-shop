using furni1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using furni1.ViewModels;

namespace furni1.DataAccesLayer
{
	public class AppDbContext: IdentityDbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
        public DbSet<ContentHome> ContentHomes { get; set; }
        public DbSet<ContentProduct> ContentProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<WhyChooseUs> WhyChooseUs { get; set; }
        public DbSet<WeHelpYou> WeHelpYous { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Bio> Bios { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<furni1.ViewModels.UserVM> UserVM { get; set; } = default!;
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}

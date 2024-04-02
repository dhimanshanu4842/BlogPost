using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data
{
    public class BlogDbContext:DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> Options) : base(Options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author()
                {
                    Id = 1,
                    Name = "Test"
                },
                new Author()
                {
                    Id = 2,
                    Name = "Jeffery Achher"
                },
                new Author()
                {
                    Id = 3,
                    Name = "Chetan Bhagat"
                }
                );

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Type = "Test Category"
                },
                new Category()
                {
                    Id = 2,
                    Type = "Drama"
                },
                new Category()
                {
                    Id = 3,
                    Type = "Action"
                }
                );
            modelBuilder.Entity<Post>().HasData(
                new Post()
                {
                    Id = 1,
                    Title = "Demo",
                    Discription = "This is Demo",
                    CategoryId = 1,
                    AuthorId =1,
                    CreatedOn = DateTime.Now
                },
                new Post()
                {
                    Id = 2,
                    Title = "Test",
                    Discription = "This is Test",
                    CategoryId = 1,
                    AuthorId = 2,
                    CreatedOn = DateTime.Now
                },
                new Post()
                {
                    Id = 3,
                    Title = "Temporary",
                    Discription= "This is Temporary",
                    CategoryId = 2,
                    AuthorId = 3,
                    CreatedOn = DateTime.Now
                }
                );
        }
        public DbSet<Post>Posts { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category>Categories { get; set; }

    }
}

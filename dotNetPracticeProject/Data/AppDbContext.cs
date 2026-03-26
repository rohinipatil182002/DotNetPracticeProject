using Microsoft.EntityFrameworkCore;

namespace dotNetPracticeProject.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
                new Currency { Id = 1, Title = "Dinar", Description="Dinar" },
                new Currency { Id = 2, Title = "EUR" , Description="EUR"},
                new Currency { Id = 3, Title = "GBP" , Description="GBP"},
                new Currency { Id = 4, Title = "INR", Description = "Indian INR" },
                new Currency { Id = 5, Title = "DOllar", Description = "Dollar" }
            );

            modelBuilder.Entity<Language>().HasData(
                new Language { Id = 1, Title = "Hindi", Description = "Hindi" },
                new Language { Id = 2, Title = "English", Description = "English" },
                new Language { Id = 3, Title = "Marathi", Description = "Marathi" },
                new Language { Id = 4, Title = "Urdu", Description = "Urdu" },
                new Language { Id = 5, Title = "Panjabi", Description = "Panjabi" }
            );

        }
        public DbSet<Book> Books { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<BookPrice> BookPrices { get; set; }

        public DbSet<Currency> Currencies { get; set; }
    }
}

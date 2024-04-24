using LibraryWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Data
{
    public class ApplicationDbContext(DbContextOptions dbContextOptions) : IdentityDbContext<Librarian>(dbContextOptions)
    {

        public DbSet<Member> Members { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Lend> Lends { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // many to many relationship using Lend as intermediate table

            builder.Entity<Lend>()
                .HasKey(e => e.Id);

            builder.Entity<Lend>()
                .HasOne(e => e.Book)
                .WithMany(p => p.Lends)
                .HasForeignKey(u => u.BookId);

            builder.Entity<Lend>()
                .HasOne(e => e.Member)
                .WithMany(p => p.Borrows)
                .HasForeignKey(u => u.MemberId);
            
            builder.Entity<Lend>()
                .HasOne(e => e.Librarian)
                .WithMany(p => p.Lends)
                .HasForeignKey(u => u.LibrarianId);

            // two roles for librarians => 1. Manager is like an admin and can hire people too , 2. Receptionist can't hire people
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                },
                new IdentityRole
                {
                    Name = "Receptionist",
                    NormalizedName = "Receptionist"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}

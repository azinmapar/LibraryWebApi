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

        public DbSet<BookEdition> BookEditions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // many to many relationship using Lend as intermediate table


            //I'm restricing all delete behaviors becaus I don't want my records be deleted after a member/librarian/book is deleted (although I don't use delete in my controller, I will deactivate them when deleting needed)
            // except between book and edition

            builder.Entity<BookEdition>()
                .HasKey(e => e.EditionId);

            builder.Entity<Lend>()
                .HasKey(e => e.LendId);

            builder.Entity<Lend>()
                .HasOne(e => e.Edition)
                .WithMany(p => p.Lends)
                .HasForeignKey(u => u.EditionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Lend>()
                .HasOne(e => e.Member)
                .WithMany(p => p.Borrows)
                .HasForeignKey(u => u.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Lend>()
                .HasOne(e => e.Librarian)
                .WithMany(p => p.Lends)
                .HasForeignKey(u => u.LibrarianId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BookEdition>()
                .HasOne(e => e.RegisterLibrarian)
                .WithMany(p => p.RegisteredEditions)
                .HasForeignKey(u => u.RegisterLibrarianId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Librarian>()
                .HasOne(e => e.RegisterManager)
                .WithMany(p => p.RegisteredLibrarians)
                .HasForeignKey(u => u.RegisterManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Member>()
                .HasOne(e => e.RegisterLibrarian)
                .WithMany(p => p.RegisteredMembers)
                .HasForeignKey(u => u.RegisterLibrarianId)
                .OnDelete(DeleteBehavior.Restrict);

            // for uniqueness

            builder.Entity<Librarian>()
                .HasIndex(b => b.NationalId)
                .IsUnique();

            builder.Entity<Librarian>()
                .HasIndex(b => b.PhoneNumber)
                .IsUnique();


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
                    NormalizedName = "RECEPTIONIST"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}

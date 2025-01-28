using MeetingRoomBooking.DataAccess.Identity;
using MeetingRoomBooking.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace MeetingRoomBooking.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
        ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole,
        ApplicationUserLogin, ApplicationRoleClaim,
        ApplicationUserToken>

    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public ApplicationDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    x => x.MigrationsAssembly(_migrationAssembly));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            // Seed roles for Admin, User, and Author
            builder.Entity<ApplicationRole>().HasData(
                 new ApplicationRole
                 {
                     Id = Guid.Parse("d0b85c3e-4f68-4a8c-9c92-7aabc1234567"), // Static GUID for Admin role
                     Name = "Admin",
                     NormalizedName = "ADMIN",
                     ConcurrencyStamp = Guid.NewGuid().ToString()
                 },
                 new ApplicationRole
                 {
                     Id = Guid.Parse("f3c852ab-7db6-4f1a-89c9-8d1abc234567"), // Static GUID for User role
                     Name = "User",
                     NormalizedName = "USER",
                     ConcurrencyStamp = Guid.NewGuid().ToString()
                 }
            );

            builder.Entity<ApplicationUser>().HasData(
                    new ApplicationUser
                    {
                           Id = Guid.Parse("a0f85c3e-4f68-4a8c-9c92-7aabc1234567"), // Static GUID for the user
                           UserName = "admin", // UserName
                           NormalizedUserName = "ADMIN", // NormalizedUserName
                           Email = "admin@gmail.com", // Email
                           NormalizedEmail = "ADMIN@GMAIL.COM", // NormalizedEmail
                           EmailConfirmed = true, // EmailConfirmed
                           PhoneNumberConfirmed = false, // PhoneNumberConfirmed (set to false)
                           PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Admin123"), // PasswordHash
                           SecurityStamp = Guid.NewGuid().ToString(), // SecurityStamp
                           ConcurrencyStamp = Guid.NewGuid().ToString(), // ConcurrencyStamp
                           LockoutEnabled = false, // LockoutEnabled (optional, depending on your requirements)
                           TwoFactorEnabled = false // TwoFactorEnabled (optional, depending on your requirements)
                    }
            );

            builder.Entity<IdentityUserRole<Guid>>()
           .HasKey(ur => new { ur.UserId, ur.RoleId });


            builder.Entity<IdentityUserRole<Guid>>().HasData(
                  new IdentityUserRole<Guid>
                  {
                      UserId = Guid.Parse("a0f85c3e-4f68-4a8c-9c92-7aabc1234567"), // Static GUID for the user
                      RoleId = Guid.Parse("d0b85c3e-4f68-4a8c-9c92-7aabc1234567")  // Static GUID for Admin role
                  }
            );

            builder.Entity<IdentityUserRole<Guid>>()
           .HasOne<ApplicationUser>()
           .WithMany()
           .HasForeignKey(ur => ur.UserId)
           .OnDelete(DeleteBehavior.Cascade);

           
        }

        public DbSet<MeetingRoom> meetingRooms { get; set; }

        public DbSet<Booking> Bookings { get; set; }


    }
}

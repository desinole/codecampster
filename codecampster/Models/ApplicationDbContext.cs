using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecamp2018.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Timeslot> Timeslots { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<AttendeeSession> AttendeeSessions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity(typeof(ApplicationUser), x =>
            {
                x.Property<string>("FirstName");
                x.Property<string>("LastName");
                x.Property<string>("Location");
                x.Property<string>("Twitter");
                x.Property<int?>("AvatarID");
                x.Property<bool?>("RSVP");
                x.Property<bool?>("Volunteer");
            });
            builder.Entity(typeof(Event), x =>
            {
                x.Property<int>("ID");
                x.Property<string>("Name");
                x.Property<string>("SocialMediaHashtag");
                x.Property<System.DateTime>("EventStart");
                x.Property<System.DateTime>("EventEnd");
                x.Property<string>("CompleteAddress");
                x.Property<bool>("IsCurrent");
            });
            builder.Entity(typeof(Speaker), x =>
            {
                x.Property<int>("ID");
                x.Property<string>("FullName");
                x.Property<string>("Company");
                x.Property<string>("Title");
                x.Property<string>("Bio");
                x.Property<string>("Twitter");
                x.Property<string>("Website");
                x.Property<string>("Blog");
                x.Property<bool?>("Special");
                x.Property<string>("AvatarURL");
                x.Property<string>("MVPDetails");
                x.Property<string>("AuthorDetails");
                x.Property<string>("NoteToOrganizers");
                x.Property<bool>("IsMvp");
                x.Property<string>("PhoneNumber");
                x.Property<string>("LinkedIn");
            });
            builder.Entity(typeof(Announcement), x =>
            {
                x.Property<int>("ID");
                x.Property<string>("Message");
                x.Property<int>("Rank");
                x.Property<System.DateTime>("PublishOn");
                x.Property<System.DateTime>("ExpiresOn");
            });
            builder.Entity(typeof(Sponsor), x =>
            {
                x.Property<int>("ID");
                x.Property<string>("CompanyName");
                x.Property<string>("SponsorLevel");
                x.Property<string>("Bio");
                x.Property<string>("Twitter");
                x.Property<string>("Website");
                x.Property<string>("AvatarURL");
            });
            builder.Entity(typeof(Session), x =>
            {
                x.Property<int>("SessionID");
                x.Property<string>("Name");
                x.Property<string>("Description");
                x.Property<int>("Level");
                x.Property<int>("SpeakerID");
                x.Property<int?>("CoSpeakerID");
                x.Property<int>("SpeakerID");
                x.Property<int?>("TimeslotID");
                x.Property<bool?>("Special");
            });
            builder.Entity<Session>().HasOne(p => p.Speaker).WithMany(p => p.Sessions);
            builder.Entity<Speaker>().HasMany(p => p.Sessions);
            builder.Entity(typeof(Track), x =>
            {
                x.Property<int>("ID");
                x.Property<string>("Name");
                x.Property<string>("RoomNumber");
            });
            builder.Entity<Session>().HasOne(p => p.Track).WithMany(p => p.Sessions);
            builder.Entity<Track>().HasMany(p => p.Sessions);
            builder.Entity(typeof(Timeslot), x =>
            {
                x.Property<int>("ID");
                x.Property<string>("StartTime");
                x.Property<string>("EndTime");
                x.Property<bool?>("Special");
            });
            builder.Entity<Session>().HasOne(p => p.Timeslot).WithMany(p => p.Sessions);
            builder.Entity<Timeslot>().HasMany(p => p.Sessions);
            builder.Entity(typeof(AttendeeSession), x =>
            {
                x.Property<int>("ID");
                x.Property<string>("ApplicationUserId");
                x.Property<int>("SessionID");
            });
            builder.Entity<AttendeeSession>().HasOne(p => p.RelatedSession);
            builder.Entity<AttendeeSession>().HasOne(p => p.AppUser);
        }

        public void EnsureSeed(string adminUser, string adminPass)
        {
            var userStore = new UserStore<ApplicationUser>(this);
            int records = 0;
            Task<bool> hasRoles = this.Roles.AnyAsync();
            if (!hasRoles.Result)
            {
                var role = new IdentityRole();
                role.Name = "speaker";
                role.NormalizedName = "SPEAKER";
                this.Roles.Add(role);
                records = this.SaveChanges();
                role = new IdentityRole();
                role.Name = "attendee";
                role.NormalizedName = "ATTENDEE";
                this.Roles.Add(role);
                records = this.SaveChanges();
                role = new IdentityRole();
                role.Name = "administrator";
                role.NormalizedName = "ADMINISTRATOR";
                this.Roles.Add(role);
                records = this.SaveChanges();
            }
            var adminRole = this.Roles.Where(r => r.Name == "administrator" && r.NormalizedName == "ADMINISTRATOR").FirstOrDefault();
            Task<bool> anyAdmins = this.UserRoles.AnyAsync(r => r.RoleId == adminRole.Id);
            if (!anyAdmins.Result)
            {
                var user = new ApplicationUser
                {
                    FirstName = adminUser,
                    LastName = adminUser,
                    Email = adminUser,
                    UserName = adminUser,
                    NormalizedEmail = adminUser.ToUpper(),
                    NormalizedUserName = adminUser.ToUpper(),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, adminPass);
                user.PasswordHash = hashed;

                var userResult = userStore.CreateAsync(user);
                var roleResult = userStore.AddToRoleAsync(user, adminRole.NormalizedName);

            }
            Task<bool> containsEvents = this.Events.AnyAsync();
            if (!containsEvents.Result)
            {
                var ccEvent = new Event
                {
                    Name = "Orlando Codecamp 2018",
                    EventStart = DateTime.Parse("2018-03-17 08:00:00"),
                    EventEnd = DateTime.Parse("2018-03-17 17:00:00"),
                    IsCurrent = true,
                    SocialMediaHashtag = "#OrlandoCC",
                    CompleteAddress = "University Partnership Building, Seminole State College (Sanford), 100 Weldon Blvd, Sanford FL 32746",
                    SpeakerRegistrationOpen = true
                };
                this.Events.Add(ccEvent);
                this.SaveChanges();
            }
            Task<bool> containsSponsors = this.Sponsors.AnyAsync();
            if (!containsSponsors.Result)
            {
                var sponsor = new Sponsor
                {
                    CompanyName = "Orlando .NET User Group",
                    SponsorLevel = "Platinum",
                    Bio = "ONETUG was founded by Joel Martinez in 2001. Our goal is to showcase great speakers & content centered around, but not restricted to, the Microsoft .NET stack. We strive to bring together developers from all platforms by holding monthly meetings, Nerd Dinners & an annual Codecamp. Our vision is to collaborate with other tech groups & help build Orlando into a major hub for technology companies & startups.",
                    Twitter = "ONETUG",
                    Website = "http://onetug.org",
                    AvatarURL = "/images/ONETUGlogo.png"
                };
                this.Sponsors.Add(sponsor);
                this.SaveChanges();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.UseInMemoryDatabase();
            // options.UseSqlServer(.["Data:DefaultConnection:ConnectionString"]))
            base.OnConfiguring(options);
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}

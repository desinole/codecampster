using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace codecampster.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Session> Sessions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

	        builder.Entity(typeof (ApplicationUser), x =>
	        {
		        x.Property<string>("FirstName");
				x.Property<string>("LastName");
				x.Property<string>("Location");
				x.Property<string>("Twitter");
				x.Property<int?>("AvatarID");
                x.Property<bool?>("RSVP");
                x.Property<bool?>("Volunteer");
			});
 	        builder.Entity(typeof (Event), x =>
	        {
		        x.Property<int>("ID");
                x.Property<string>("Name");
                x.Property<string>("SocialMediaHashtag");
                x.Property<System.DateTime>("EventStart");
                x.Property<System.DateTime>("EventEnd");
                x.Property<string>("CompleteAddress");
                x.Property<bool>("IsCurrent");
			});
            builder.Entity(typeof(Speaker), x=>
            {
                x.Property<int>("ID");
                x.Property<string>("FullName");
                x.Property<string>("Company");
                x.Property<string>("Title");
                x.Property<string>("Bio");
                x.Property<string>("Twitter");
                x.Property<string>("Website");
                x.Property<string>("Blog");
                x.Property<string>("AvatarURL");
            });
            builder.Entity(typeof(Announcement),x=>
            {
                x.Property<int>("ID");
                x.Property<string>("Message");
                x.Property<int>("Rank");
                x.Property<System.DateTime>("PublishOn");
                x.Property<System.DateTime>("ExpiresOn");
            });
            builder.Entity(typeof(Sponsor), x=>
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
             });
            builder.Entity<Session>().HasOne(p => p.Speaker).WithMany(p => p.Sessions);
       }
       
       public void EnsureSeed()
       {
           Task<bool> containsEvents =  this.Events.AnyAsync();
           if (!containsEvents.Result)
           {
               var ccEvent = new Event{ 
                   Name = "Orlando Codecamp 2016",
                   EventStart = DateTime.Parse("2016-04-02 08:00:00"),
                   EventEnd = DateTime.Parse("2016-04-02 17:00:00"),
                   IsCurrent = true,
                   SocialMediaHashtag = "#OrlandoCC",
                   CompleteAddress = "University Partnership Building, Seminole State College (Sanford), 100 Weldon Blvd, Sanford FL 32746"
                   };
               this.Events.Add(ccEvent);
               this.SaveChanges();
           }
           Task<bool> containsSpeakers = this.Speakers.AnyAsync();
           if (!containsSpeakers.Result)
           {
               var speaker = new Speaker
               {
                   FullName = "SSC Advisory Committee",
                   Company = "Seminole State College",
                   Website = "https://www.seminolestate.edu/",
                   Twitter = "SeminoleState",
                   AvatarURL = "https://pbs.twimg.com/profile_images/529291538325450752/X0zAf03G_400x400.jpeg"
               };
               this.Speakers.Add(speaker);
               this.SaveChanges();
                Task<bool> containsSession = this.Sessions.AnyAsync();
                if (!containsSession.Result)
                {
                    var session = new Session
                    {
                        Name = "Career Panel",
                        Description = "Career Panel session hosted by Seminole State with industry leaders",
                        Level = 1,
                        SpeakerID = speaker.ID
                    };
                    this.Sessions.Add(session);
                    this.SaveChanges();
                }
            }
            Task<bool> containsAnnouncements = this.Announcements.AnyAsync();
           if (!containsAnnouncements.Result)
           {
               var announcement = new Announcement
               {
                   ID = 1, //this is a bug. fix it
                   Message = "Orlando Codecamp 2016 will be held 8am-5pm April 2 2016 at University Partnership Building, Seminole State College (Sanford), 100 Weldon Blvd, Sanford FL 32746",
                   PublishOn = DateTime.Now,
                   ExpiresOn = DateTime.Now.AddYears(1),
                   Rank = 1
               };
               this.Announcements.Add(announcement);
               this.SaveChanges();
                announcement = new Announcement
                {
                    ID = 2, //this is a bug. fix it
                    Message = "Speakers party (sponsor: AgileThought) will be held at 6pm on April 1 2016 at Liam Fitzpatrick in Lake Mary",
                    PublishOn = DateTime.Now,
                    ExpiresOn = DateTime.Now.AddYears(1),
                    Rank = 2
                };
                this.Announcements.Add(announcement);
                this.SaveChanges();
                announcement = new Announcement
                {
                    ID = 3, //this is a bug. fix it
                    Message = "Attendees party will be held at 6pm on April 2 2016 at Liam Fitzpatrick in Lake Mary",
                    PublishOn = DateTime.Now,
                    ExpiresOn = DateTime.Now.AddYears(1),
                    Rank = 3
                };
                this.Announcements.Add(announcement);
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
    }
}

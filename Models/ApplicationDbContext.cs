using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace codecampster.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Speaker> Speakers {get;set;}
        public DbSet<Announcement> Announcements {get;set;}
        public DbSet<Sponsor> Sponsors {get;set;}
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
           }
       }
    }
}

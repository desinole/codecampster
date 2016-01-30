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
       }
       
       public void EnsureSeed()
       {
           Task<bool> result =  this.Events.AnyAsync();
           if (!result.Result)
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
       }
    }
}

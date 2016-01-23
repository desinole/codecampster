using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace codecampster.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
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
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using codecampster.Models;

namespace codecampster.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171201114900_Speaker registration")]
    partial class Speakerregistration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("codecampster.Models.Announcement", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ExpiresOn");

                    b.Property<string>("Message");

                    b.Property<DateTime>("PublishOn");

                    b.Property<int>("Rank");

                    b.HasKey("ID");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("codecampster.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int?>("AvatarID");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Location");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<bool?>("RSVP");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Twitter");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<bool?>("Volunteer");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("codecampster.Models.AttendeeSession", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<int>("SessionID");

                    b.HasKey("ID");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("SessionID");

                    b.ToTable("AttendeeSessions");
                });

            modelBuilder.Entity("codecampster.Models.Event", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("AttendeeRegistrationOpen");

                    b.Property<string>("CompleteAddress");

                    b.Property<DateTime>("EventEnd");

                    b.Property<DateTime>("EventStart");

                    b.Property<bool>("IsCurrent");

                    b.Property<string>("Name");

                    b.Property<string>("SocialMediaHashtag");

                    b.Property<bool?>("SpeakerRegistrationOpen");

                    b.HasKey("ID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("codecampster.Models.Session", b =>
                {
                    b.Property<int>("SessionID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CoSpeakerID");

                    b.Property<string>("CoSpeakers");

                    b.Property<string>("Description");

                    b.Property<bool>("IsApproved");

                    b.Property<string>("KeyWords");

                    b.Property<int>("Level");

                    b.Property<string>("Name");

                    b.Property<int>("SpeakerID");

                    b.Property<bool?>("Special");

                    b.Property<int?>("TimeslotID");

                    b.Property<int?>("TrackID");

                    b.HasKey("SessionID");

                    b.HasIndex("SpeakerID");

                    b.HasIndex("TimeslotID");

                    b.HasIndex("TrackID");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("codecampster.Models.Speaker", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("AuthorDetails");

                    b.Property<string>("AvatarURL");

                    b.Property<string>("Bio");

                    b.Property<string>("Blog");

                    b.Property<string>("Company");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsMvp");

                    b.Property<string>("LinkedIn");

                    b.Property<string>("MVPDetails");

                    b.Property<string>("NoteToOrganizers");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool?>("Special");

                    b.Property<string>("Title");

                    b.Property<string>("Twitter");

                    b.Property<string>("Website");

                    b.HasKey("ID");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("Speakers");
                });

            modelBuilder.Entity("codecampster.Models.Sponsor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AvatarURL");

                    b.Property<string>("Bio");

                    b.Property<string>("CompanyName");

                    b.Property<string>("SponsorLevel");

                    b.Property<string>("Twitter");

                    b.Property<string>("Website");

                    b.HasKey("ID");

                    b.ToTable("Sponsors");
                });

            modelBuilder.Entity("codecampster.Models.Timeslot", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EndTime");

                    b.Property<int>("Rank");

                    b.Property<bool?>("Special");

                    b.Property<string>("StartTime");

                    b.HasKey("ID");

                    b.ToTable("Timeslots");
                });

            modelBuilder.Entity("codecampster.Models.Track", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("RoomNumber");

                    b.HasKey("ID");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("codecampster.Models.AttendeeSession", b =>
                {
                    b.HasOne("codecampster.Models.ApplicationUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("codecampster.Models.Session", "RelatedSession")
                        .WithMany()
                        .HasForeignKey("SessionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("codecampster.Models.Session", b =>
                {
                    b.HasOne("codecampster.Models.Speaker", "Speaker")
                        .WithMany("Sessions")
                        .HasForeignKey("SpeakerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("codecampster.Models.Timeslot", "Timeslot")
                        .WithMany("Sessions")
                        .HasForeignKey("TimeslotID");

                    b.HasOne("codecampster.Models.Track", "Track")
                        .WithMany("Sessions")
                        .HasForeignKey("TrackID");
                });

            modelBuilder.Entity("codecampster.Models.Speaker", b =>
                {
                    b.HasOne("codecampster.Models.ApplicationUser", "AppUser")
                        .WithOne("Speaker")
                        .HasForeignKey("codecampster.Models.Speaker", "ApplicationUserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("codecampster.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("codecampster.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("codecampster.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

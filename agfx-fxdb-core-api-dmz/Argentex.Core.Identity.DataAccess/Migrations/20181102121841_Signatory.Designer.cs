﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Argentex.Core.Identity.DataAccess.Migrations
{
    [DbContext(typeof(SecurityDbContext))]
    [Migration("20181102121841_Signatory")]
    partial class Signatory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("ActivityId");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.ActivityLog", b =>
                {
                    b.Property<long>("ActivityLogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActivityId");

                    b.Property<int?>("AuthUserId");

                    b.Property<long?>("Id");

                    b.Property<bool>("IsSuccess");

                    b.Property<DateTime>("LogDate");

                    b.Property<string>("PrimaryIP")
                        .HasMaxLength(128);

                    b.Property<string>("SecondaryIP")
                        .HasMaxLength(128);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("ActivityLogId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("Id");

                    b.ToTable("ActivityLog");
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.ApplicationRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.ApplicationRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<long>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.ApplicationUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ASPCreationDate");

                    b.Property<string>("ASPNumber");

                    b.Property<int>("AccessFailedCount");

                    b.Property<int?>("ApprovedByAuthUserId");

                    b.Property<int>("AuthUserId");

                    b.Property<DateTime?>("Birthday");

                    b.Property<int>("ClientCompanyContactId");

                    b.Property<int>("ClientCompanyId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Forename")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsApproved");

                    b.Property<bool>("IsAuthorisedSignatory");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsSignatory");

                    b.Property<DateTime?>("LastEmailChange");

                    b.Property<DateTime>("LastPasswordChange")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("LastTelephoneChange");

                    b.Property<DateTime?>("LastUpdate");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("Notes");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PhoneNumberMobile")
                        .HasMaxLength(128);

                    b.Property<string>("PhoneNumberOther")
                        .HasMaxLength(128);

                    b.Property<string>("Position")
                        .HasMaxLength(50);

                    b.Property<bool?>("PrimaryContact");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<int>("UpdatedByAuthUserId");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasAlternateKey("AuthUserId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.ApplicationUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim");
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.ApplicationUserLogin", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<long>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.ApplicationUserRole", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.ApplicationUserToken", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.Country", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodeISO2")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<string>("CodeISO3")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<int>("CodeISO3Numeric");

                    b.Property<int>("CountryGroupId");

                    b.Property<string>("FormalName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int?>("LengthIBAN");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("PhoneCode")
                        .HasMaxLength(25);

                    b.Property<string>("RegexBBAN")
                        .HasMaxLength(256);

                    b.Property<int>("Sequence");

                    b.HasKey("Id");

                    b.HasIndex("CountryGroupId");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.CountryGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("Sequence");

                    b.HasKey("Id");

                    b.ToTable("CountryGroup");
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.PreviousPassword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("PasswordHash");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PreviousPasswords");
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.Report", b =>
                {
                    b.Property<long>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(128);

                    b.HasKey("ReportId");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientId")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<int>("Type");

                    b.Property<long>("UserId");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Token");
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.UserReport", b =>
                {
                    b.Property<long>("ReportId");

                    b.Property<long>("UserId");

                    b.HasKey("ReportId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserReport");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictApplication", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId")
                        .IsRequired();

                    b.Property<string>("ClientSecret");

                    b.Property<string>("ConcurrencyToken")
                        .IsConcurrencyToken();

                    b.Property<string>("ConsentType");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Permissions");

                    b.Property<string>("PostLogoutRedirectUris");

                    b.Property<string>("Properties");

                    b.Property<string>("RedirectUris");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("OpenIddictApplications");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationId");

                    b.Property<string>("ConcurrencyToken")
                        .IsConcurrencyToken();

                    b.Property<string>("Properties");

                    b.Property<string>("Scopes");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("OpenIddictAuthorizations");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictScope", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyToken")
                        .IsConcurrencyToken();

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Properties");

                    b.Property<string>("Resources");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("OpenIddictScopes");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationId");

                    b.Property<string>("AuthorizationId");

                    b.Property<string>("ConcurrencyToken")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset?>("CreationDate");

                    b.Property<DateTimeOffset?>("ExpirationDate");

                    b.Property<string>("Payload");

                    b.Property<string>("Properties");

                    b.Property<string>("ReferenceId");

                    b.Property<string>("Status");

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("AuthorizationId");

                    b.HasIndex("ReferenceId")
                        .IsUnique()
                        .HasFilter("[ReferenceId] IS NOT NULL");

                    b.ToTable("OpenIddictTokens");
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.ActivityLog", b =>
                {
                    b.HasOne("Argentex.Core.Identity.DataAccess.Activity", "Activity")
                        .WithMany("ActivityLogs")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Argentex.Core.Identity.DataAccess.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("Id");
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.ApplicationRoleClaim", b =>
                {
                    b.HasOne("Argentex.Core.Identity.DataAccess.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.ApplicationUserClaim", b =>
                {
                    b.HasOne("Argentex.Core.Identity.DataAccess.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.ApplicationUserLogin", b =>
                {
                    b.HasOne("Argentex.Core.Identity.DataAccess.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.ApplicationUserRole", b =>
                {
                    b.HasOne("Argentex.Core.Identity.DataAccess.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Argentex.Core.Identity.DataAccess.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.ApplicationUserToken", b =>
                {
                    b.HasOne("Argentex.Core.Identity.DataAccess.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.Country", b =>
                {
                    b.HasOne("Argentex.Core.Identity.DataAccess.CountryGroup", "ContryGroup")
                        .WithMany()
                        .HasForeignKey("CountryGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.PreviousPassword", b =>
                {
                    b.HasOne("Argentex.Core.Identity.DataAccess.ApplicationUser", "User")
                        .WithMany("PreviousPasswords")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_PreviousPasswords_User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.Token", b =>
                {
                    b.HasOne("Argentex.Core.Identity.DataAccess.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Argentex.Core.Identity.DataAccess.UserReport", b =>
                {
                    b.HasOne("Argentex.Core.Identity.DataAccess.Report", "Report")
                        .WithMany("UserReports")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Argentex.Core.Identity.DataAccess.ApplicationUser", "User")
                        .WithMany("UserReports")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication", "Application")
                        .WithMany("Authorizations")
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication", "Application")
                        .WithMany("Tokens")
                        .HasForeignKey("ApplicationId");

                    b.HasOne("OpenIddict.Models.OpenIddictAuthorization", "Authorization")
                        .WithMany("Tokens")
                        .HasForeignKey("AuthorizationId");
                });
#pragma warning restore 612, 618
        }
    }
}

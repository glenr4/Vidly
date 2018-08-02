﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Vidly.Models
{
	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser
	{
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}
	}

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Movie> Movies { get; set; }
		public DbSet<MembershipType> MembershipTypes { get; set; }
		public DbSet<Genre> Genres { get; set; }

		/// <summary>
		/// Creates Database for localdb or SQLExpress only
		/// </summary>
		//public ApplicationDbContext()
		//	: base("DefaultConnection", throwIfV1Schema: false) {
		//}

		/// <summary>
		/// Creates the Database if it doesn't exist using the named Connection String
		/// </summary>
		public ApplicationDbContext() : base("name=VidlyConnectionString") {
			Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());

			//Database.SqlQuery
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
	}
}
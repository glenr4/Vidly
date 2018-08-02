namespace Vidly.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class SeedUsers : DbMigration
	{
		public override void Up()
		{
			// Users
			Sql(@"
				INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b0fa2dcf-81c5-43df-97c7-4e2e7d1cc9ee', N'admin@vidly.com', 0, N'AJjRGxIDDYkfpIasT3HYuNFg0+7taXGO9tHX8wm8O04mh6wU0bfIBsHUG6uK7sqVnA==', N'5fb3265d-4f7e-4097-90b0-36d9a9e320a6', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f735624c-990f-42c5-8f5b-c9761f4dfc34', N'guest@vidly.com', 0, N'AFGvOSkCWXVCXGNUGbN0hfUg0oar4z/OmAr4znUrGRQolsUrXARg8NrFdZDqt5PxJw==', N'de17dff5-ed0d-402c-888c-5f4aca5215c1', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')");

			// Roles
			Sql(@"
				INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'ca54248d-2a07-4a0b-8121-cac357a0baa3', N'CanManageMovies')");

			// UserRoles
			Sql(@"
				INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b0fa2dcf-81c5-43df-97c7-4e2e7d1cc9ee', N'ca54248d-2a07-4a0b-8121-cac357a0baa3')");
		}
		
		public override void Down()
		{
		}
	}
}

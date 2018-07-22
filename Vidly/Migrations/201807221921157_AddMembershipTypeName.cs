namespace Vidly.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class AddMembershipTypeName : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.MembershipTypes", "Name", c => c.String(nullable: false, maxLength: 255));

			Sql(@"UPDATE MembershipTypes SET Name = 'Pay as you go' WHERE DurationInMonths = 0;");
			Sql(@"UPDATE MembershipTypes SET Name = 'Monthly' WHERE DurationInMonths = 1;");
			Sql(@"UPDATE MembershipTypes SET Name = 'Quarterly' WHERE DurationInMonths = 3; ");
			Sql(@"UPDATE MembershipTypes SET Name = 'Annual' WHERE DurationInMonths = 12;");
		}
		
		public override void Down()
		{
			DropColumn("dbo.MembershipTypes", "Name");
		}
	}
}

namespace Vidly.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class AddDateOfBirth : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.Customers", "DateOfBirth", c => c.DateTime());

			Sql(@"UPDATE MembershipTypes SET Name = 'Pay as you go' WHERE DurationInMonths = 0;
			UPDATE MembershipTypes SET Name = 'Monthly' WHERE DurationInMonths = 1;
			UPDATE MembershipTypes SET Name = 'Quarterly' WHERE DurationInMonths = 3;
			UPDATE MembershipTypes SET Name = 'Annual' WHERE DurationInMonths = 12;");
		}
		
		public override void Down()
		{
			DropColumn("dbo.Customers", "DateOfBirth");
		}
	}
}

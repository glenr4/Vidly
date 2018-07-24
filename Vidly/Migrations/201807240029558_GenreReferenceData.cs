namespace Vidly.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class GenreReferenceData : DbMigration
	{
		public override void Up()
		{
			Sql(@"INSERT INTO Genres (Name) VALUES ('Kids'), ('Action'), ('Drama'), ('Comedy');");
		}
		
		public override void Down()
		{
		}
	}
}

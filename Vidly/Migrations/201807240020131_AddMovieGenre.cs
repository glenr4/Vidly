namespace Vidly.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class AddMovieGenre : DbMigration
	{
		public override void Up()
		{
			// Delete all Movies first to avoid FK violation
			// TODO: create a better migration: rename table, create new Movie
			// table and copy data across, then drop renamed table

			Sql("DELETE Movies;");

			CreateTable(
				"dbo.Genres",
				c => new
					{
						Id = c.Int(nullable: false, identity: true),
						Name = c.String(nullable: false, maxLength: 255),
					})
				.PrimaryKey(t => t.Id);
			
			AddColumn("dbo.Movies", "GenreId", c => c.Int(nullable: false));
			CreateIndex("dbo.Movies", "GenreId");
			AddForeignKey("dbo.Movies", "GenreId", "dbo.Genres", "Id", cascadeDelete: true);
			DropColumn("dbo.Movies", "Genre");
		}
		
		public override void Down()
		{
			AddColumn("dbo.Movies", "Genre", c => c.String(nullable: false, maxLength: 255));
			DropForeignKey("dbo.Movies", "GenreId", "dbo.Genres");
			DropIndex("dbo.Movies", new[] { "GenreId" });
			DropColumn("dbo.Movies", "GenreId");
			DropTable("dbo.Genres");
		}
	}
}

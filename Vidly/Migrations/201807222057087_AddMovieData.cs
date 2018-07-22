namespace Vidly.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class AddMovieData : DbMigration
	{
		public override void Up()
		{
			Sql(@"UPDATE Movies SET Genre = 'Kids', ReleaseDate = '2000-01-01', AddedDate = '2000-01-30', Stock = 5 WHERE Name = 'Shrek';
			UPDATE Movies SET Genre = 'Kids', ReleaseDate = '2000-01-01', AddedDate = '2000-01-30', Stock = 10 WHERE Name = 'Wall-E';

			INSERT Movies(Name, Genre, ReleaseDate, AddedDate, Stock) VALUES('Terminator 2', 'Action', '1990-06-23', '1990-12-30', 10);
			INSERT Movies(Name, Genre, ReleaseDate, AddedDate, Stock) VALUES('Toy Story', 'Kids', '2005-06-23', '2005-12-30', 20);
			INSERT Movies(Name, Genre, ReleaseDate, AddedDate, Stock) VALUES('There''s Something About Mary', 'Comdey', '2006-06-23', '2006-12-30', 8);");
		}
		
		public override void Down()
		{
		}
	}
}

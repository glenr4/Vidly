using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models {
	public class Movie {
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		public Genre Genre { get; set; }

		public int GenreId { get; set; }

		[DisplayFormat(DataFormatString = "{0:D}")]
		public DateTime ReleaseDate { get; set; }

		[DisplayFormat(DataFormatString = "{0:D}")]
		public DateTime AddedDate { get; set; }

		public int Stock { get; set; }
	}
}
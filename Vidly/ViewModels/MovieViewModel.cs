using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.ViewModels {
	public class MovieViewModel {
		public IEnumerable<Genre> Genres { get; set; }
		public Movie Movie { get; set; }
	}
}
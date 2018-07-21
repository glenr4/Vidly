using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
	public class MoviesController : Controller {
		private List<Movie> _movies = new List<Movie>();
		private List<Customer> _customers = new List<Customer>();

		public MoviesController() {
			_movies.Add(new Movie { Id = 1, Name = "Shrek" });
			_movies.Add(new Movie { Id = 2, Name = "Wall-E" });


			_customers.Add(new Customer { Name = "Customer 1" });
			_customers.Add(new Customer { Name = "Customer 2" });
		}

		// GET: Movies
		public ActionResult Random() {
			var movie = _movies.Find(x => x.Id == 1);

			if (movie == null || _customers == null) {
				return new HttpNotFoundResult();
			}

			var viewModel = new RandomMovieViewModel {
				Movie = movie,
				Customers = _customers
			};

			return View(viewModel);

			// This method ends up being too verbose in Random.cshtml
			// Can also use ViewBag but still have similar problems with casting
			// and a magic property instead of a magic string
			//ViewData["Movie"] = movie;
			//return View();

			// Better to use this:
			//return View(movie);
			// Can also use this as the return object
			//return new ViewResult();
		}

		public ActionResult Edit(int id) {
			return Content("id = " + id);
		}

		//public ActionResult Index(int? pageIndex, string sortBy) {
		//	if (!pageIndex.HasValue) {
		//		pageIndex = 1;
		//	}

		//	if(String.IsNullOrWhiteSpace(sortBy)) {
		//		sortBy = "Name";
		//	}

		//	return Content($"pageIndex={pageIndex} & sortBy={sortBy}");
		//}
		public ActionResult Index() {
			return View(_movies);
		}

		[Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
		public ActionResult ByReleaseYear(int year, int month) {
			return Content(year + "/" + month);
		}

		[Route("Movies/Details/{id}")]
		public ActionResult GetDetail(int id) {
			var movie = _movies.Find(x => x.Id == id);
			if (movie == null) {
				return new HttpNotFoundResult();
			}

			return View("Detail", movie);
		}
	}
}
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
		private ApplicationDbContext _context;

		public MoviesController() {
			_context = new ApplicationDbContext();
		}

		// GET: Movies
		public ActionResult Random() {
			var movie = _context.Movies.Where(x => x.Id == 1).FirstOrDefault();

			if (movie == null) {
				return new HttpNotFoundResult();
			}

			var viewModel = new RandomMovieViewModel {
				Movie = movie,
				Customers = null
			};

			return View(viewModel);
		}

		public ActionResult Edit(int id) {
			return Content("id = " + id);
		}

		
		public ActionResult Index() {
			var movies = _context.Movies.ToList();
			return View(movies);
		}

		[Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
		public ActionResult ByReleaseYear(int year, int month) {
			return Content(year + "/" + month);
		}

		[Route("Movies/Details/{id}")]
		public ActionResult GetDetail(int id) {
			var movie = _context.Movies.Where(x => x.Id == id).FirstOrDefault();
			if (movie == null) {
				return new HttpNotFoundResult();
			}

			return View("Detail", movie);
		}

		protected override void Dispose(bool disposing) {
			_context.Dispose();
		}
	}
}
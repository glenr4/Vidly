﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

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

		[Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult Edit(int id) {
			var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

			if(movie == null) {
				return HttpNotFound();
			}

			var genres = _context.Genres.ToList();
			var viewModel = new MovieFormViewModel {
				Movie = movie,
				Genres = genres
			};

			return View("MovieForm", viewModel);
		}

		
		public ActionResult Index() {
			var movies = _context.Movies
				.Include(m => m.Genre)
				.ToList();			

			return View(movies);
		}

		[Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
		public ActionResult ByReleaseYear(int year, int month) {
			return Content(year + "/" + month);
		}

		[Route("Movies/Details/{id}")]
		public ActionResult GetDetail(int id) {
			var movie = _context.Movies
						.Where(x => x.Id == id)
						.Include(x => x.Genre)
						.FirstOrDefault();

			if (movie == null) {
				return new HttpNotFoundResult();
			}

			return View("Detail", movie);
		}

		[Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult New() {
			var genres = _context.Genres.ToList();

			var viewModel = new MovieFormViewModel {
				Genres = genres,
				Movie = new Movie()
			};

			return View("MovieForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult Save(Movie movie) {
			if (!ModelState.IsValid) {
				var viewModel = new MovieFormViewModel {
					Genres = _context.Genres.ToList(),
					Movie = new Movie()
				};

				return View("MovieForm", viewModel);
			}



			if(movie.Id == 0) {
				_context.Movies.Add(movie);
			}
			else {
				var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
				movieInDb.AddedDate = movie.AddedDate;
				movieInDb.GenreId = movie.GenreId;
				movieInDb.Name = movie.Name;
				movieInDb.ReleaseDate = movie.ReleaseDate;
				movieInDb.Stock = movie.Stock;
			}

			_context.SaveChanges();

			return RedirectToAction("Index", "Movies");

		}

		protected override void Dispose(bool disposing) {
			_context.Dispose();
		}
	}
}
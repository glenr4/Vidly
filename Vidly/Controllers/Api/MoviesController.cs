using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
	public class MoviesController : ApiController
	{
		private ApplicationDbContext _context;

		public MoviesController() {
			_context = new ApplicationDbContext();
		}

		// GET /api/movies
		public IEnumerable<MovieDto> GetMovies() {
			return _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);
		}

		// GET /api/movies/1
		public IHttpActionResult GetMovie(int id) {
			var record = _context.Movies.SingleOrDefault(c => c.Id == id);

			if (record == null) {
				return NotFound();
			}

			return Ok(Mapper.Map<Movie, MovieDto>(record));
		}

		// POST /api/movies
		[HttpPost]
		public IHttpActionResult CreateMovie(MovieDto movieDto) {
			if (!ModelState.IsValid) {
				return BadRequest();
			}

			var movie = Mapper.Map<MovieDto, Movie>(movieDto);
			_context.Movies.Add(movie);
			_context.SaveChanges();

			movieDto.Id = movie.Id;

			return Created(new Uri($"{Request.RequestUri}/{movie.Id}"), movieDto);
		}

		// PUT /api/movie/1
		[HttpPut]
		public IHttpActionResult UpdateMovie(int id, MovieDto movieDto) {
			if (!ModelState.IsValid) {
				return BadRequest();
			}

			var movieInDB = _context.Movies.SingleOrDefault(c => c.Id == id);
			if (movieInDB == null) {
				return NotFound();
			}

			// Update properties
			Mapper.Map(movieDto, movieInDB);

			_context.SaveChanges();

			return StatusCode(HttpStatusCode.NoContent);
		}

		// DELETE /api/movies/1
		[HttpDelete]
		public IHttpActionResult DeleteMovie(int id) {
			var movieInDB = _context.Movies.SingleOrDefault(c => c.Id == id);

			if (movieInDB == null) {
				return NotFound();
			}

			_context.Movies.Remove(movieInDB);
			_context.SaveChanges();

			return StatusCode(HttpStatusCode.NoContent);
		}

		protected override void Dispose(bool disposing) {
			_context.Dispose();
		}
	}
}

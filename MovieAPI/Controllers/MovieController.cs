using MovieAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Repositories;
using MovieAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMongoRepository<Movie> _movieRrepository;
        private readonly ISyncServices<Movie> _movieSyncService;
        public MovieController(IMongoRepository<Movie> movierepository,
            ISyncServices<Movie> movieSyncServices)
        {
            _movieRrepository = movierepository;
            _movieSyncService = movieSyncServices;
        }

        [HttpGet]
        public  List<Movie> GetAllMovies()
        {
            var records = _movieRrepository.GetAllRecords();
            return records;
        }

        [HttpGet("{id}")]
        public Movie GetMovieById(Guid id)
        {
            var result = _movieRrepository.GetRecordById(id);
          
            return result;
        }

        [HttpPost]
        public IActionResult Create (Movie movie)
        {
            movie.LastChangedAt = DateTime.UtcNow;
        var result=_movieRrepository.InsertRecord(movie);
            _movieSyncService.Upsert(movie);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult UPsert(Movie movie)
        {
            if (movie.Id ==Guid.Empty)
            {
                return BadRequest("Empty Id");
            }
            movie.LastChangedAt = DateTime.UtcNow;
            _movieRrepository.UpsertRecord(movie);

            _movieSyncService.Upsert(movie);
            return Ok(movie);
        }

        [HttpPut("sync")]
        public IActionResult UpsertSync(Movie movie)
        {
            var existingMovie = _movieRrepository.GetRecordById(movie.Id);

            if (existingMovie == null || movie.LastChangedAt > existingMovie.LastChangedAt)
            {
                _movieRrepository.UpsertRecord(movie);
            }
            return Ok();
        }

        [HttpDelete("sync")]
        public IActionResult DeleteSync(Movie movie)
        {
            var existingMovie = _movieRrepository.GetRecordById(movie.Id);

            if (existingMovie != null || movie.LastChangedAt > existingMovie.LastChangedAt)
            {
                _movieRrepository.DeleteRecord(movie.Id);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var movie = _movieRrepository.GetRecordById(id);
            if (movie ==null)
            {
                return BadRequest("Movie небыл найден");
            }
            _movieRrepository.DeleteRecord(id);

            movie.LastChangedAt = DateTime.UtcNow;
            _movieSyncService.Delete(movie);

            return Ok("Удален " + id);
        }
    }
}

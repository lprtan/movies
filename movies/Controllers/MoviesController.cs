using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace movies.Controllers
{
    public class MoviesController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            List<Movie> m = new List<Movie>();
            string apiKey = "19cad3e4cec968d8a2934e5b2ed00d7a";
            string baseUrl = "https://api.themoviedb.org/3/movie/popular?api_key=" + apiKey;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = await httpClient.GetAsync(baseUrl))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();

                        dynamic data = JObject.Parse(json);
                        var movies = data.results;

                        foreach (var movie in movies)
                        {
                            var movieName = movie.original_title;
                            var pop = movie.popularity;
                            var date = movie.release_date;
                            var rate = movie.vote_average;

                            m.Add(new Movie { movieName = movieName, popularite = pop, date = date, rate = rate });

                        }
                    }
                }
            }

            return View(m);
        }
    }
}

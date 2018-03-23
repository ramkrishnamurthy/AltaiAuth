using System.Collections.Generic;
using System.Threading;
using System.Web.Http;
using Altai.Api.Models;

namespace Altai.Api.Controllers
{
    [Authorize]
    public class MovieListController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetList()
        {
            var identity = Thread.CurrentPrincipal.Identity;

            var list = new List<Movie>
            {
                new Movie {Name = "The Shawshank Redemption", Year = 1994},
                new Movie {Name = "The Godfather", Year = 1972},
                new Movie {Name = "The Godfather: Part II", Year = 1974}
            };
            return Ok(list);
        }
    }
}
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmesController : ControllerBase
    {

        public static List<Filme> filmes = new List<Filme>();
        public static int id = 0;


        [HttpPost]
        public IActionResult AdicionarFilme([FromBody] Filme filme)
        {
            filme.Id = id++;
            filmes.Add(filme);
            return CreatedAtAction(nameof(RecuperarFilmeId), new {id = filme.Id}, filme);
        }


        [HttpGet]
        public IEnumerable<Filme> RecuperarFilmes([FromQuery] int skip, [FromQuery] int take)
        {
            return filmes.Skip(skip).Take(take);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarFilmeId(int id) {

            var filme = filmes.FirstOrDefault(filme => filme.Id == id);
            if(filmes == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(filme);
            }
        }





    }
}

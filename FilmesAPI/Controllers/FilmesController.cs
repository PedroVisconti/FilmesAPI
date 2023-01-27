using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmesController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;
        public FilmesController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }


        [HttpPost]
        public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDTO)
        {
            Filme filme = _mapper.Map<Filme>(filmeDTO);
            _context.filmes.Add(filme);
            return CreatedAtAction(nameof(RecuperarFilmeId), new {id = filme.Id}, filme);
        }


        [HttpGet]
        public IEnumerable<Filme> RecuperarFilmes([FromQuery] int skip, [FromQuery] int take)
        {
            return _mapper.Map<List<ReadFilmeDto>>(_context.filmes.Skip(skip).Take(take));
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarFilmeId(int id) {

            var filme = _context.filmes.FirstOrDefault(filme => filme.Id == id);
            if(filme == null)
            {
                return NotFound();
            }
            else
            {
                var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
                return Ok(filmeDto);
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarFilme(int id,
        [FromBody] UpdateFilmeDto filmeDto)
        {
            var filme = _context.filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return NotFound();
            }
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
            return NoContent();

        }

        [HttpPut("{id}")]
        public IActionResult AtuializarFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
        {
            var filme = _context.filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return NotFound();
            }

            var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

            patch.ApplyTo(filmeParaAtualizar, ModelState);

            if (!TryValidateModel(filmeParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(filmeParaAtualizar, filme);
            _context.SaveChanges();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarFilme(int id)
        {
            var filme = _context.filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return NotFound();
            }
            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

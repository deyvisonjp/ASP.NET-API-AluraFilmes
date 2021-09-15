using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public ReadFilmeDto ReadFilmeDto { get; private set; }

        public FilmeController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //Antes de usar o banco
        //private static List<Filme> filmes = new List<Filme>();
        //private static int id = 1;

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            // filme será do tipo Filme e foi mapeado a partir de um parâmetro chamado filmeDto.
            Filme filme = _mapper.Map<Filme>(filmeDto);

            _context.Filmes.Add(filme);
            _context.SaveChanges();
            //Além de informarmos que o recurso foi criado, é importante informarmos onde podemos localizá-lo.
            return CreatedAtAction(nameof(RecupeFilmePorId), new
            {
                Id = filme.Id
            }, filme);
            #region 'Antes do banco'
            //Antes de usar o banco
            //filme.Id = id++;
            //filmes.Add(filme);
            #endregion
        }

        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes()
        {
            return _context.Filmes;
        }

        [HttpGet("{id}")]
        public IActionResult RecupeFilmePorId(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                ReadFilmeDto = _mapper.Map<ReadFilmeDto>(filme);
                return NotFound();
            }
            return Ok(filme);
            #region 'SEM C#'
            //O Código acima substitui tudo abaixo ...
            //foreach(Filme filme in filmes)
            //{
            //    if(filme.Id == id)
            //    {
            //        return filme;
            //    }
            //}
            //return null;
            #endregion
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto novoFilmeDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            _mapper.Map(novoFilmeDto, filme);
            _context.SaveChanges();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult RemoveFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

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

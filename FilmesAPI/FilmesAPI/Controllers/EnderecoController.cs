using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Endereco;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FilmesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        
            private AppDbContext _context;
            private IMapper _mapper;

            public EnderecoController(AppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            [HttpPost]
            public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto cinemaDto)
            {
                Endereco endereco = _mapper.Map<Endereco>(cinemaDto);
                _context.Enderecos.Add(endereco);
                _context.SaveChanges();
                return CreatedAtAction(nameof(RecuperaEnderecopPorId), new { Id = endereco.Id }, endereco);
            }
            [HttpGet("{id}")]
            public IActionResult RecuperaEnderecopPorId(int id)
            {
                Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
                if (endereco != null)
                {
                    ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
                    return Ok(enderecoDto);
                }
                return NotFound();
            }
            [HttpGet]
            public IEnumerable<Endereco> RecuperaEnderecos()
            {
                return _context.Enderecos;
            }
            [HttpPut("{id}")]
            public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
            {
                Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
                if (endereco == null)
                {
                    return NotFound();
                }

                _mapper.Map(enderecoDto, endereco);
                _context.SaveChanges();

                return NoContent();
            }
            [HttpDelete("{id}")]
            public IActionResult RemoveEndereco(int id)
            {
                Endereco endereco = _context.Enderecos.FirstOrDefault(c => c.Id == id);
                if (endereco == null)
                {
                    return NotFound();
                }

                _context.Remove(endereco);
                _context.SaveChanges();

                return NoContent();
            }

        }
}

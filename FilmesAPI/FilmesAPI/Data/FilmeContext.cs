using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//papel do DBContext -> Abstrair a lógica de acesso ao banco de dados.
namespace FilmesAPI.Data
{
    public class FilmeContext : DbContext
    {
        public FilmeContext(DbContextOptions<FilmeContext> opt) : base (opt)
        {
            
        }

        public DbSet<Filme> Filmes { get; set; }
    }
}

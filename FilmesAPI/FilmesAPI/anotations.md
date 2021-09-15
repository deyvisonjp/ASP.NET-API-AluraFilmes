Intalados via Nuget
- EntityFramework
- EntityFramework.Tools
- EntityFrameworkSqlServer

Primeira Migração
- Add-Migration DescricaoDaMigrate
Aplicando a Mudanã após confirmação da migration
- Update-Database

- Instalar pacote AutoMapper extensions for ASP.NET Core por meio do Nuget
*Antes: 
```
~~~C#
[HttpPut("{id}")]
        public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto novoFilme)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            filme.Titulo = novoFilme.Titulo;
            filme.Diretor = novoFilme.Diretor;
            filme.Genero = novoFilme.Genero;
            filme.Duracao = novoFilme.Duracao;
            _context.SaveChanges();

            return NoContent();

        }
```
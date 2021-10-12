## Intalados via Nuget
- EntityFramework
- EntityFramework.Tools
- EntityFrameworkSqlServer
- Microsoft.EntityFrameworkCore.Proxies

## Primeira Migração
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

## Relacionamento 1:1
1. Cinema x 1_Endereco
´´´
 protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Endereco>()
                .HasOne(endereco => endereco.Cinema)
                .WithOne(cinema => cinema.Endereco)
                .HasForeignKey<Cinema>(cinema => cinema.EnderecoId);
        }
´´´

## Relacionamento 1:n
- _1_Gerente x N_Cinema_

```
    builder.Entity<Cinema>()
        .HasOne(cinema => cinema.Gerente)
        .WithMany(gerente => gerente.Cinemas)
        .HasForeignKey(cinema => cinema.GerenteId);
```

2. Mapeando e trazendo os dados não redundantes

```
    CreateMap<Gerente, ReadGerenteDto>()
        .ForMember(gerente => gerente.Cinemas, opts => opts
        .MapFrom(gerente => gerente.Cinemas
        .Select(c => new { c.Id, c.Nome, c.Endereco, c.EnderecoId})));
```
Estamos mapeando de um Gerente para um ReadGerenteDto. Para o campo Cinemas, estamos selecionando apenas os campos Id, Nome, Endereco e EnderedoId.

3. Deleção em Cascata
- No modo cascata, se tentarmos deletar um recurso que é dependência de outro, todos os outros recursos que dependem desse serão excluídos também. No modo restrito não conseguiremos efetuar a deleção.
    - Ex. Se Cinema depende de Gerente, ao excluir gerente, todos cinemas serão excluidos 
    -* para contornar a situalçao, mudamos 
        ```
            builder.Entity<Cinema>()
                // ...
                .OnDelete(DeleteBehavior.Restrict);
        ```

## Relacionamento n:n
- _N_Filme x N_Cinema_

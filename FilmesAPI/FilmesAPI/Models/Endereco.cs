using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FilmesAPI.Models
{
    public class Endereco
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Logradouro { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public int Numero { get; set; }

        // modificador de acesso virtual e alterar a sua classe de Startup, a fim de conseguir carregar as propriedades dinamicamente.
        [JsonIgnore]
        public virtual Cinema Cinema { get; set; }
    }
}

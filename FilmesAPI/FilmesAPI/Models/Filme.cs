﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Models
{
    public class Filme
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo título é obrigatório")]
        public string Titulo { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "O genero não deve passar de 60 caracteres")]
        public string Diretor { get; set; }
        [StringLength(30, ErrorMessage = "O genero não deve passar de 30 caracteres")]
        public string Genero { get; set; }
        [Required]
        [Range(1, 250, ErrorMessage = "A duração deve ter no mínimo 1 e no máximo 250 min")]
        public int Duracao { get; set; }
    }
}
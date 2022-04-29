﻿using back_end.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Entidades
{
    public class Genero
    {

        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [StringLength(maximumLength: 50,ErrorMessage ="El campo {0} debe tener 10 maximo de longitud")]
        [primeraLetraMayuscula]
        public string Nombre { get; set; }

        public List<PeliculasGeneros> peliculasGeneros { get; set; }

    }
}

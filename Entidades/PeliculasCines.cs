using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Entidades
{
    public class PeliculasCines
    {
        public int PeliculasId { get; set; }
        public int CineId { get; set; }

        public Pelicula Pelicula { get; set; }
        public Cine Cine { get; set; }
        [StringLength(maximumLength:300)]
        public string Personaje { get; set; }
        public int Orden { get; set; }
    }
}

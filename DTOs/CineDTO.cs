using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.DTOs
{
    public class CineDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        
        public double latitud { get; set; }
       
        public double longitud { get; set; }
    }
}

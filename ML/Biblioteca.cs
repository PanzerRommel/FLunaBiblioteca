using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Biblioteca
    {
        public int IdLibro { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public DateTime AñoPublicacion { get; set; }
        public string Genero { get; set; }
        public decimal Precio { get; set; }
        public byte[] Imagen { get; set; }

        public List<object> Libros { get; set; }
    }
}

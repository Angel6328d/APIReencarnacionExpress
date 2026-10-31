using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Vestigios
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Forma { get; set; } = "";
        public string Efecto { get; set; } = "";
    }
}

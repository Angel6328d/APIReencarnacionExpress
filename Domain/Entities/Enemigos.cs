using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Enemigo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public TipoEnemigo Tipo { get; set; } = TipoEnemigo.Monstruo;
        public int Rating { get; set; } 
        public string Debilidad { get; set; } = "";
        public string Descripcion { get; set; } = "";          
    }

    public enum TipoEnemigo
    {
        Monstruo,
        Jefe
    }
}

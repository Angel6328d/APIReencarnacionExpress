using Domain.Entities;

namespace Domain.dto
{
    // Lo que devuelve la API
    public class EnemigoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public TipoEnemigo Tipo { get; set; }
        public int Rating { get; set; }
        public string Debilidad { get; set; } = "";
        public string Descripcion { get; set; } = "";
    }

    // Lo que recibe la API al crear o editar (sin Id)
    public class GuardarEnemigoDto
    {
        public string Nombre { get; set; } = "";
        public TipoEnemigo Tipo { get; set; }
        public int Rating { get; set; }
        public string Debilidad { get; set; } = "";
        public string Descripcion { get; set; } = "";
    }
}
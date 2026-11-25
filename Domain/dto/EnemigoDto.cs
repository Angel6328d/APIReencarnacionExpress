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

        /// <summary>Imagen principal (la más reciente de tipo Imagen).</summary>
        public string? ImagenUrl { get; set; }

        /// <summary>Icono/miniatura (el más reciente de tipo Icono).</summary>
        public string? IconoUrl { get; set; }
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
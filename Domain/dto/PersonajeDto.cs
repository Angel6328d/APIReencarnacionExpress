namespace Domain.dto
{
    public class PersonajeDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";

        /// <summary>Arte grande (primer medio de tipo Imagen).</summary>
        public string? ImagenUrl { get; set; }

        /// <summary>Miniatura (primer medio de tipo Icono).</summary>
        public string? IconoUrl { get; set; }
    }

    public class GuardarPersonajeDto
    {
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
    }
}
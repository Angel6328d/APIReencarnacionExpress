namespace Domain.dto
{
    public class VestigioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Forma { get; set; } = "";
        public string Efecto { get; set; } = "";

        /// <summary>Imagen principal (la más reciente de tipo Imagen).</summary>
        public string? ImagenUrl { get; set; }

        /// <summary>Icono/miniatura (el más reciente de tipo Icono).</summary>
        public string? IconoUrl { get; set; }
    }

    public class GuardarVestigioDto
    {
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Forma { get; set; } = "";
        public string Efecto { get; set; } = "";
    }
}
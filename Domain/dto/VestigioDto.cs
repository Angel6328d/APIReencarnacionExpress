namespace Domain.dto
{
    public class VestigioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Forma { get; set; } = "";
        public string Efecto { get; set; } = "";
    }

    public class GuardarVestigioDto
    {
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Forma { get; set; } = "";
        public string Efecto { get; set; } = "";
    }
}
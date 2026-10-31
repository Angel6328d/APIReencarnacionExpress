namespace Domain.dto
{
    public class PersonajeDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
    }

    public class GuardarPersonajeDto
    {
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
    }
}
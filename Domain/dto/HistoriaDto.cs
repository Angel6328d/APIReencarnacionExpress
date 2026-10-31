namespace Domain.dto
{
    public class HistoriaDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = "";
    }

    public class GuardarHistoriaDto
    {
        public string Descripcion { get; set; } = "";
    }
}
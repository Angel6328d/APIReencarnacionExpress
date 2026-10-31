namespace Domain.dto
{
    public class RegionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
    }

    public class GuardarRegionDto
    {
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
    }
}
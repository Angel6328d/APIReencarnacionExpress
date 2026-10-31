using Domain.Entities;

namespace Domain.dto
{
    public class MedioDto
    {
        public int Id { get; set; }
        public TipoEntidad Entidad { get; set; }
        public int EntidadId { get; set; }
        public TipoMedio Tipo { get; set; }
        public string Url { get; set; } = "";
        public int Orden { get; set; }
    }
}
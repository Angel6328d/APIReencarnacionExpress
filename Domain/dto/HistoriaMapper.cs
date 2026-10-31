using Domain.dto;
using Domain.Entities;

namespace Domain.Helpers
{
    public static class HistoriaMapper
    {
        public static HistoriaDto ToDto(this Historia h) => new()
        {
            Id = h.Id,
            Descripcion = h.Descripcion
        };

        public static void Aplicar(this Historia h, GuardarHistoriaDto dto)
        {
            h.Descripcion = dto.Descripcion;
        }
    }
}
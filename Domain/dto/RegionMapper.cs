using Domain.dto;
using Domain.Entities;

namespace Domain.Helpers
{
    public static class RegionMapper
    {
        public static RegionDto ToDto(this Region r) => new()
        {
            Id = r.Id,
            Nombre = r.Nombre,
            Descripcion = r.Descripcion
        };

        public static void Aplicar(this Region r, GuardarRegionDto dto)
        {
            r.Nombre = dto.Nombre;
            r.Descripcion = dto.Descripcion;
        }
    }
}
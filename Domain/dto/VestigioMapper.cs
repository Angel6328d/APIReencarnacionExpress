using Domain.dto;
using Domain.Entities;

namespace Domain.Helpers
{
    public static class VestigioMapper
    {
        public static VestigioDto ToDto(this Vestigios v) => new()
        {
            Id = v.Id,
            Nombre = v.Nombre,
            Descripcion = v.Descripcion,
            Forma = v.Forma,
            Efecto = v.Efecto
        };

        public static void Aplicar(this Vestigios v, GuardarVestigioDto dto)
        {
            v.Nombre = dto.Nombre;
            v.Descripcion = dto.Descripcion;
            v.Forma = dto.Forma;
            v.Efecto = dto.Efecto;
        }
    }
}
using Domain.dto;
using Domain.Entities;

namespace Domain.Helpers
{
    public static class EnemigoMapper
    {
        public static EnemigoDto ToDto(this Enemigo e) => new()
        {
            Id = e.Id,
            Nombre = e.Nombre,
            Tipo = e.Tipo,
            Rating = e.Rating,
            Debilidad = e.Debilidad,
            Descripcion = e.Descripcion
        };

        public static void Aplicar(this Enemigo e, GuardarEnemigoDto dto)
        {
            e.Nombre = dto.Nombre;
            e.Tipo = dto.Tipo;
            e.Rating = dto.Rating;
            e.Debilidad = dto.Debilidad;
            e.Descripcion = dto.Descripcion;
        }
    }
}
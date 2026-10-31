using Domain.dto;
using Domain.Entities;

namespace Domain.Helpers
{
    public static class PersonajeMapper
    {
        public static PersonajeDto ToDto(this Personaje p) => new()
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion
        };

        public static void Aplicar(this Personaje p, GuardarPersonajeDto dto)
        {
            p.Nombre = dto.Nombre;
            p.Descripcion = dto.Descripcion;
        }
    }
}
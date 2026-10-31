using Domain.dto;

namespace ReencarnacionExpress.Services.Interfaz
{
    public interface IPersonajeService
    {
        Task<List<PersonajeDto>> ObtenerTodosAsync();
        Task<PersonajeDto?> ObtenerPorIdAsync(int id);
        Task<PersonajeDto> CrearAsync(GuardarPersonajeDto dto);
    }
}
using Domain.dto;

namespace ReencarnacionExpress.Services.Interfaz
{
    public interface IPersonajeService
    {
        Task<List<PersonajeDto>> ObtenerTodosAsync();
        Task<PersonajeDto?> ObtenerPorIdAsync(int id);
        Task<PersonajeDto> CrearAsync(GuardarPersonajeDto dto);
        Task<PersonajeDto?> ActualizarAsync(int id, GuardarPersonajeDto dto);
        Task<bool> EliminarAsync(int id);
    }
}
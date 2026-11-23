using Domain.dto;

namespace ReencarnacionExpress.Services.Interfaz
{
    public interface IVestigioService
    {
        Task<List<VestigioDto>> ObtenerTodosAsync();
        Task<VestigioDto?> ObtenerPorIdAsync(int id);
        Task<VestigioDto> CrearAsync(GuardarVestigioDto dto);
        Task<VestigioDto?> ActualizarAsync(int id, GuardarVestigioDto dto);
        Task<bool> EliminarAsync(int id);
    }
}
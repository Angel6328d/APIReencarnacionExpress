using Domain.dto;

namespace ReencarnacionExpress.Services.Interfaz
{
    public interface IHistoriaService
    {
        Task<List<HistoriaDto>> ObtenerTodosAsync();
        Task<HistoriaDto?> ObtenerPorIdAsync(int id);
        Task<HistoriaDto> CrearAsync(GuardarHistoriaDto dto);
        Task<HistoriaDto?> ActualizarAsync(int id, GuardarHistoriaDto dto);
        Task<bool> EliminarAsync(int id);
    }
}
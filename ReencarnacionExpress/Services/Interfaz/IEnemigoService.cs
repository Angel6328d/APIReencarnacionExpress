using Domain.dto;

namespace ReencarnacionExpress.Services.Interfaz
{
    public interface IEnemigoService
    {
        Task<List<EnemigoDto>> ObtenerTodosAsync();
        Task<EnemigoDto?> ObtenerPorIdAsync(int id);
        Task<EnemigoDto> CrearAsync(GuardarEnemigoDto dto);
        Task<EnemigoDto?> ActualizarAsync(int id, GuardarEnemigoDto dto);
        Task<bool> EliminarAsync(int id);
    }
}
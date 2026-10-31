using Domain.dto;

namespace ReencarnacionExpress.Services.Interfaz
{
    public interface IRegionService
    {
        Task<List<RegionDto>> ObtenerTodosAsync();
        Task<RegionDto?> ObtenerPorIdAsync(int id);
        Task<RegionDto> CrearAsync(GuardarRegionDto dto);
    }
}
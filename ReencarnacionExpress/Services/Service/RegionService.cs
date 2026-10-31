using Domain.dto;
using Domain.Entities;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using ReencarnacionExpress.Context;
using ReencarnacionExpress.Services.Interfaz;

namespace ReencarnacionExpress.Services.Service
{
    public class RegionService : IRegionService
    {
        private readonly AppDbContext _context;

        public RegionService(AppDbContext context) => _context = context;

        public async Task<List<RegionDto>> ObtenerTodosAsync() =>
            await _context.Regiones.AsNoTracking().Select(r => r.ToDto()).ToListAsync();

        public async Task<RegionDto?> ObtenerPorIdAsync(int id)
        {
            var r = await _context.Regiones.FindAsync(id);
            return r?.ToDto();
        }

        public async Task<RegionDto> CrearAsync(GuardarRegionDto dto)
        {
            var r = new Region();
            r.Aplicar(dto);
            _context.Regiones.Add(r);
            await _context.SaveChangesAsync();
            return r.ToDto();
        }
    }
}
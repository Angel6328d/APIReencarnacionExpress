using Domain.dto;
using Domain.Entities;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using ReencarnacionExpress.Context;
using ReencarnacionExpress.Services.Interfaz;

namespace ReencarnacionExpress.Services.Service
{
    public class HistoriaService : IHistoriaService
    {
        private readonly AppDbContext _context;

        public HistoriaService(AppDbContext context) => _context = context;

        public async Task<List<HistoriaDto>> ObtenerTodosAsync() =>
            await _context.Historias.AsNoTracking().Select(h => h.ToDto()).ToListAsync();

        public async Task<HistoriaDto?> ObtenerPorIdAsync(int id)
        {
            var h = await _context.Historias.FindAsync(id);
            return h?.ToDto();
        }

        public async Task<HistoriaDto> CrearAsync(GuardarHistoriaDto dto)
        {
            var h = new Historia();
            h.Aplicar(dto);
            _context.Historias.Add(h);
            await _context.SaveChangesAsync();
            return h.ToDto();
        }
    }
}
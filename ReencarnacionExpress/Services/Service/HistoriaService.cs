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

        private readonly IMedioService _medios;

        public HistoriaService(AppDbContext context, IMedioService medios)
        {
            _context = context;
            _medios = medios;
        }

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

        public async Task<HistoriaDto?> ActualizarAsync(int id, GuardarHistoriaDto dto)
        {
            var x = await _context.Historias.FindAsync(id);
            if (x is null) return null;

            x.Aplicar(dto);
            await _context.SaveChangesAsync();
            return x.ToDto();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var x = await _context.Historias.FindAsync(id);
            if (x is null) return false;

            _context.Historias.Remove(x);
            await _context.SaveChangesAsync();
            await _medios.EliminarDeEntidadAsync(TipoEntidad.Historia, id);   // sus imágenes/videos también
            return true;
        }
    }
}
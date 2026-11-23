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

        private readonly IMedioService _medios;

        public RegionService(AppDbContext context, IMedioService medios)
        {
            _context = context;
            _medios = medios;
        }

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

        public async Task<RegionDto?> ActualizarAsync(int id, GuardarRegionDto dto)
        {
            var x = await _context.Regiones.FindAsync(id);
            if (x is null) return null;

            x.Aplicar(dto);
            await _context.SaveChangesAsync();
            return x.ToDto();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var x = await _context.Regiones.FindAsync(id);
            if (x is null) return false;

            _context.Regiones.Remove(x);
            await _context.SaveChangesAsync();
            await _medios.EliminarDeEntidadAsync(TipoEntidad.Region, id);   // sus imágenes/videos también
            return true;
        }
    }
}
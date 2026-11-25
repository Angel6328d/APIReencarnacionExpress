using Domain.dto;
using Domain.Entities;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using ReencarnacionExpress.Context;
using ReencarnacionExpress.Services.Interfaz;

namespace ReencarnacionExpress.Services.Service
{
    public class EnemigoService : IEnemigoService
    {
        private readonly AppDbContext _context;

        private readonly IMedioService _medios;

        public EnemigoService(AppDbContext context, IMedioService medios)
        {
            _context = context;
            _medios = medios;
        }

        public async Task<List<EnemigoDto>> ObtenerTodosAsync()
        {
            var lista = await _context.Enemigos.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
            var medios = await MediosUrls.DeEntidadAsync(_context, TipoEntidad.Enemigo);
            return lista.Select(x => ConMedios(x.ToDto(), medios[x.Id])).ToList();
        }

        public async Task<EnemigoDto?> ObtenerPorIdAsync(int id)
        {
            var e = await _context.Enemigos.FindAsync(id);
            if (e is null) return null;
            var medios = await MediosUrls.DeEntidadAsync(_context, TipoEntidad.Enemigo, id);
            return ConMedios(e.ToDto(), medios[id]);
        }

        private static EnemigoDto ConMedios(EnemigoDto dto, IEnumerable<Medio> medios)
        {
            (dto.ImagenUrl, dto.IconoUrl) = MediosUrls.Elegir(medios);
            return dto;
        }

        public async Task<EnemigoDto> CrearAsync(GuardarEnemigoDto dto)
        {
            var e = new Enemigo();
            e.Aplicar(dto);
            _context.Enemigos.Add(e);
            await _context.SaveChangesAsync();
            return e.ToDto();
        }

        public async Task<EnemigoDto?> ActualizarAsync(int id, GuardarEnemigoDto dto)
        {
            var x = await _context.Enemigos.FindAsync(id);
            if (x is null) return null;

            x.Aplicar(dto);
            await _context.SaveChangesAsync();
            return x.ToDto();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var x = await _context.Enemigos.FindAsync(id);
            if (x is null) return false;

            _context.Enemigos.Remove(x);
            await _context.SaveChangesAsync();
            await _medios.EliminarDeEntidadAsync(TipoEntidad.Enemigo, id);   // sus imágenes/videos también
            return true;
        }
    }
}
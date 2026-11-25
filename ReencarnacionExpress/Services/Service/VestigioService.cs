using Domain.dto;
using Domain.Entities;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using ReencarnacionExpress.Context;
using ReencarnacionExpress.Services.Interfaz;

namespace ReencarnacionExpress.Services.Service
{
    public class VestigioService : IVestigioService
    {
        private readonly AppDbContext _context;

        private readonly IMedioService _medios;

        public VestigioService(AppDbContext context, IMedioService medios)
        {
            _context = context;
            _medios = medios;
        }

        public async Task<List<VestigioDto>> ObtenerTodosAsync()
        {
            var lista = await _context.Vestigios.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
            var medios = await MediosUrls.DeEntidadAsync(_context, TipoEntidad.Vestigio);
            return lista.Select(x => ConMedios(x.ToDto(), medios[x.Id])).ToList();
        }

        public async Task<VestigioDto?> ObtenerPorIdAsync(int id)
        {
            var v = await _context.Vestigios.FindAsync(id);
            if (v is null) return null;
            var medios = await MediosUrls.DeEntidadAsync(_context, TipoEntidad.Vestigio, id);
            return ConMedios(v.ToDto(), medios[id]);
        }

        private static VestigioDto ConMedios(VestigioDto dto, IEnumerable<Medio> medios)
        {
            (dto.ImagenUrl, dto.IconoUrl) = MediosUrls.Elegir(medios);
            return dto;
        }

        public async Task<VestigioDto> CrearAsync(GuardarVestigioDto dto)
        {
            var v = new Vestigios();
            v.Aplicar(dto);
            _context.Vestigios.Add(v);
            await _context.SaveChangesAsync();
            return v.ToDto();
        }

        public async Task<VestigioDto?> ActualizarAsync(int id, GuardarVestigioDto dto)
        {
            var x = await _context.Vestigios.FindAsync(id);
            if (x is null) return null;

            x.Aplicar(dto);
            await _context.SaveChangesAsync();
            return x.ToDto();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var x = await _context.Vestigios.FindAsync(id);
            if (x is null) return false;

            _context.Vestigios.Remove(x);
            await _context.SaveChangesAsync();
            await _medios.EliminarDeEntidadAsync(TipoEntidad.Vestigio, id);   // sus imágenes/videos también
            return true;
        }
    }
}
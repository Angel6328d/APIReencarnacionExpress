using Domain.dto;
using Domain.Entities;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using ReencarnacionExpress.Context;
using ReencarnacionExpress.Services.Interfaz;

namespace ReencarnacionExpress.Services.Service
{
    public class PersonajeService : IPersonajeService
    {
        private readonly AppDbContext _context;

        public PersonajeService(AppDbContext context) => _context = context;

        public async Task<List<PersonajeDto>> ObtenerTodosAsync()
        {
            var personajes = await _context.Personajes.AsNoTracking().OrderBy(p => p.Id).ToListAsync();

            // Una sola consulta con todos los medios de personajes, agrupados por personaje
            var medios = (await MediosDePersonajes().ToListAsync())
                .ToLookup(m => m.EntidadId);

            return personajes.Select(p => ConMedios(p, medios[p.Id])).ToList();
        }

        public async Task<PersonajeDto?> ObtenerPorIdAsync(int id)
        {
            var p = await _context.Personajes.FindAsync(id);
            if (p is null) return null;

            var medios = await MediosDePersonajes().Where(m => m.EntidadId == id).ToListAsync();
            return ConMedios(p, medios);
        }

        public async Task<PersonajeDto> CrearAsync(GuardarPersonajeDto dto)
        {
            var p = new Personaje();
            p.Aplicar(dto);
            _context.Personajes.Add(p);
            await _context.SaveChangesAsync();
            return p.ToDto();
        }

        private IQueryable<Medio> MediosDePersonajes() =>
            _context.Medios.AsNoTracking()
                .Where(m => m.Entidad == TipoEntidad.Personaje && m.Tipo != TipoMedio.Video)
                .OrderBy(m => m.Orden);

        private static PersonajeDto ConMedios(Personaje p, IEnumerable<Medio> medios)
        {
            var lista = medios.ToList();

            // Compatibilidad: iconos viejos guardados como "Imagen" con "Icono" en el nombre del archivo
            static bool PareceIcono(Medio m) =>
                m.Tipo == TipoMedio.Imagen && m.Url.Contains("icono", StringComparison.OrdinalIgnoreCase);

            var dto = p.ToDto();

            dto.ImagenUrl = lista
                .FirstOrDefault(m => m.Tipo == TipoMedio.Imagen && !PareceIcono(m))?.Url;

            dto.IconoUrl = lista.FirstOrDefault(m => m.Tipo == TipoMedio.Icono)?.Url
                           ?? lista.FirstOrDefault(PareceIcono)?.Url;

            return dto;
        }
    }
}

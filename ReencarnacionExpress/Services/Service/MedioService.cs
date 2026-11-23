using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ReencarnacionExpress.Context;
using ReencarnacionExpress.Services.Interfaz;

namespace ReencarnacionExpress.Services.Service
{
    public class MedioService : IMedioService
    {
        private readonly AppDbContext _context;
        private readonly IArchivoService _archivos;

        public MedioService(AppDbContext context, IArchivoService archivos)
        {
            _context = context;
            _archivos = archivos;
        }

        public async Task<bool> EliminarAsync(int medioId)
        {
            var medio = await _context.Medios.FindAsync(medioId);
            if (medio is null) return false;

            _context.Medios.Remove(medio);
            await _context.SaveChangesAsync();
            await _archivos.EliminarAsync(medio.Ruta);
            return true;
        }

        public async Task EliminarDeEntidadAsync(TipoEntidad entidad, int entidadId)
        {
            var medios = await _context.Medios
                .Where(m => m.Entidad == entidad && m.EntidadId == entidadId)
                .ToListAsync();
            if (medios.Count == 0) return;

            _context.Medios.RemoveRange(medios);
            await _context.SaveChangesAsync();

            foreach (var m in medios)
                await _archivos.EliminarAsync(m.Ruta);
        }
    }
}

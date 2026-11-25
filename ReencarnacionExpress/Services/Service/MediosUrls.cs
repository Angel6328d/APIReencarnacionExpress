using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ReencarnacionExpress.Context;

namespace ReencarnacionExpress.Services.Service
{
    /// <summary>
    /// Calcula la imagen y el icono de cada registro con UNA sola consulta a Medios,
    /// para que la app no tenga que pedir los medios de cada elemento por separado.
    /// Si hay varios, gana el más reciente (mayor Orden / Id).
    /// </summary>
    public static class MediosUrls
    {
        public static async Task<ILookup<int, Medio>> DeEntidadAsync(AppDbContext ctx, TipoEntidad entidad, int? id = null)
        {
            var q = ctx.Medios.AsNoTracking().Where(m => m.Entidad == entidad && m.Tipo != TipoMedio.Video);
            if (id is { } unId) q = q.Where(m => m.EntidadId == unId);
            return (await q.ToListAsync()).ToLookup(m => m.EntidadId);
        }

        public static (string? Imagen, string? Icono) Elegir(IEnumerable<Medio> medios)
        {
            var lista = medios.OrderByDescending(m => m.Orden).ThenByDescending(m => m.Id).ToList();

            // Compatibilidad: iconos antiguos guardados como "Imagen" con "icono" en la URL
            static bool PareceIcono(Medio m) =>
                m.Tipo == TipoMedio.Imagen && m.Url.Contains("icono", StringComparison.OrdinalIgnoreCase);

            var imagen = lista.FirstOrDefault(m => m.Tipo == TipoMedio.Imagen && !PareceIcono(m))?.Url;
            var icono = lista.FirstOrDefault(m => m.Tipo == TipoMedio.Icono)?.Url
                        ?? lista.FirstOrDefault(PareceIcono)?.Url;
            return (imagen, icono);
        }
    }
}

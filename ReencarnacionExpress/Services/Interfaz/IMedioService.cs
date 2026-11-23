using Domain.Entities;

namespace ReencarnacionExpress.Services.Interfaz
{
    public interface IMedioService
    {
        /// <summary>Borra un medio (fila + archivo en Storage). Devuelve false si no existe.</summary>
        Task<bool> EliminarAsync(int medioId);

        /// <summary>Borra todos los medios de una entidad (se usa al eliminar la entidad).</summary>
        Task EliminarDeEntidadAsync(TipoEntidad entidad, int entidadId);
    }
}

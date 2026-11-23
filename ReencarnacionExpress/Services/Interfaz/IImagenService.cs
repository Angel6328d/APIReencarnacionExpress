using Domain.Entities;

namespace ReencarnacionExpress.Services.Interfaz
{
    public record ArchivoSubido(string Url, string Ruta);

    public interface IArchivoService
    {
        Task<ArchivoSubido> SubirAsync(IFormFile archivo, string carpeta, TipoMedio tipo);

        /// <summary>Borra un archivo del bucket. No lanza error si no existe.</summary>
        Task EliminarAsync(string ruta);
    }
}
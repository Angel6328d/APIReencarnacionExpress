using Domain.Entities;

namespace ReencarnacionExpress.Services.Interfaz
{
    public record ArchivoSubido(string Url, string Ruta);

    public interface IArchivoService
    {
        Task<ArchivoSubido> SubirAsync(IFormFile archivo, string carpeta, TipoMedio tipo);
    }
}
using System.Net.Http.Headers;
using Domain.Entities;
using ReencarnacionExpress.Services.Interfaz;

namespace ReencarnacionExpress.Services.Service
{
    public class SupabaseArchivoService : IArchivoService
    {
        private static readonly string[] ImagenesPermitidas =
            { "image/png", "image/jpeg", "image/webp", "image/gif" };
        private static readonly string[] VideosPermitidos =
            { "video/mp4", "video/webm", "video/quicktime" };

        private const long MaxImagen = 5 * 1024 * 1024;     // 5 MB
        private const long MaxVideo = 50 * 1024 * 1024;     // 50 MB

        private readonly HttpClient _http;
        private readonly string _url;
        private readonly string _bucket;
        private readonly string _key;

        public SupabaseArchivoService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _url = config["Supabase:Url"]!.TrimEnd('/');
            _bucket = config["Supabase:Bucket"]!;
            _key = config["Supabase:ServiceKey"]!;
        }

        public async Task<ArchivoSubido> SubirAsync(IFormFile archivo, string carpeta, TipoMedio tipo)
        {
            // Imagen e Icono aceptan los mismos formatos; solo Video es distinto
            var esVideo = tipo == TipoMedio.Video;
            var permitidos = esVideo ? VideosPermitidos : ImagenesPermitidas;
            var maximo = esVideo ? MaxVideo : MaxImagen;

            if (archivo.Length == 0 || archivo.Length > maximo)
                throw new ArgumentException($"El archivo debe pesar entre 1 byte y {maximo / 1024 / 1024} MB.");

            if (!permitidos.Contains(archivo.ContentType))
                throw new ArgumentException($"Formato no permitido. Permitidos: {string.Join(", ", permitidos)}.");

            var ruta = $"{carpeta}/{Guid.NewGuid()}{Path.GetExtension(archivo.FileName)}";

            using var stream = archivo.OpenReadStream();
            using var contenido = new StreamContent(stream);
            contenido.Headers.ContentType = new MediaTypeHeaderValue(archivo.ContentType);

            using var request = Crear(HttpMethod.Post, ruta);
            request.Content = contenido;

            var respuesta = await _http.SendAsync(request);
            if (!respuesta.IsSuccessStatusCode)
            {
                var detalle = await respuesta.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Supabase respondió {(int)respuesta.StatusCode}: {detalle}");
            }

            return new ArchivoSubido($"{_url}/storage/v1/object/public/{_bucket}/{ruta}", ruta);
        }

        private HttpRequestMessage Crear(HttpMethod metodo, string ruta)
        {
            var request = new HttpRequestMessage(metodo, $"{_url}/storage/v1/object/{_bucket}/{ruta}");
            request.Headers.Add("apikey", _key);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _key);
            return request;
        }
    }
}
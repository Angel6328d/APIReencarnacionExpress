using Domain.dto;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReencarnacionExpress.Context;
using ReencarnacionExpress.Services.Interfaz;

namespace ReencarnacionExpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediosController : ControllerBase
    {
        private const long LimiteVideo = 50 * 1024 * 1024;

        private readonly AppDbContext _context;
        private readonly IArchivoService _archivos;
        private readonly IMedioService _medios;

        public MediosController(AppDbContext context, IArchivoService archivos, IMedioService medios)
        {
            _context = context;
            _archivos = archivos;
            _medios = medios;
        }

        /// <summary>Lista las imágenes y videos de una entidad (ordenados).</summary>
        [HttpGet("{entidad}/{id:int}")]
        public async Task<ActionResult<List<MedioDto>>> Listar(TipoEntidad entidad, int id)
        {
            var medios = await _context.Medios
                .AsNoTracking()
                .Where(m => m.Entidad == entidad && m.EntidadId == id)
                .OrderBy(m => m.Orden)
                .Select(m => new MedioDto
                {
                    Id = m.Id,
                    Entidad = m.Entidad,
                    EntidadId = m.EntidadId,
                    Tipo = m.Tipo,
                    Url = m.Url,
                    Orden = m.Orden
                })
                .ToListAsync();

            return Ok(medios);
        }

        [HttpPost("{entidad}/{id:int}/imagen")]
        public Task<IActionResult> SubirImagen(TipoEntidad entidad, int id, IFormFile archivo) =>
            Subir(entidad, id, archivo, TipoMedio.Imagen);

        [HttpPost("{entidad}/{id:int}/icono")]
        public Task<IActionResult> SubirIcono(TipoEntidad entidad, int id, IFormFile archivo) =>
            Subir(entidad, id, archivo, TipoMedio.Icono);

        /// <summary>Cambia el tipo (Imagen/Video/Icono) y/o el orden de un medio existente.</summary>
        [HttpPatch("{medioId:int}")]
        public async Task<ActionResult<MedioDto>> Actualizar(int medioId, ActualizarMedioDto dto)
        {
            var medio = await _context.Medios.FindAsync(medioId);
            if (medio is null) return NotFound($"No existe el medio con id {medioId}.");

            if (dto.Tipo is { } tipo) medio.Tipo = tipo;
            if (dto.Orden is { } orden) medio.Orden = orden;

            await _context.SaveChangesAsync();

            return Ok(new MedioDto
            {
                Id = medio.Id,
                Entidad = medio.Entidad,
                EntidadId = medio.EntidadId,
                Tipo = medio.Tipo,
                Url = medio.Url,
                Orden = medio.Orden
            });
        }

        /// <summary>Reemplaza el archivo de un medio (sube el nuevo y borra el anterior del bucket).</summary>
        [HttpPut("{medioId:int}/archivo")]
        [RequestSizeLimit(LimiteVideo)]
        [RequestFormLimits(MultipartBodyLengthLimit = LimiteVideo)]
        public async Task<ActionResult<MedioDto>> ReemplazarArchivo(int medioId, IFormFile archivo)
        {
            var medio = await _context.Medios.FindAsync(medioId);
            if (medio is null) return NotFound($"No existe el medio con id {medioId}.");

            ArchivoSubido subido;
            try
            {
                subido = await _archivos.SubirAsync(archivo, medio.Entidad.ToString().ToLowerInvariant(), medio.Tipo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(502, ex.Message);
            }

            var rutaAnterior = medio.Ruta;
            medio.Url = subido.Url;
            medio.Ruta = subido.Ruta;
            await _context.SaveChangesAsync();
            await _archivos.EliminarAsync(rutaAnterior);

            return Ok(ADto(medio));
        }

        /// <summary>Elimina un medio (registro + archivo en Storage).</summary>
        [HttpDelete("{medioId:int}")]
        public async Task<IActionResult> Eliminar(int medioId) =>
            await _medios.EliminarAsync(medioId) ? NoContent() : NotFound();

        private static MedioDto ADto(Medio m) => new()
        {
            Id = m.Id,
            Entidad = m.Entidad,
            EntidadId = m.EntidadId,
            Tipo = m.Tipo,
            Url = m.Url,
            Orden = m.Orden
        };

        [HttpPost("{entidad}/{id:int}/video")]
        [RequestSizeLimit(LimiteVideo)]
        [RequestFormLimits(MultipartBodyLengthLimit = LimiteVideo)]
        public Task<IActionResult> SubirVideo(TipoEntidad entidad, int id, IFormFile archivo) =>
            Subir(entidad, id, archivo, TipoMedio.Video);

        private async Task<IActionResult> Subir(TipoEntidad entidad, int id, IFormFile archivo, TipoMedio tipo)
        {
            if (!await ExisteAsync(entidad, id)) return NotFound($"No existe {entidad} con id {id}.");

            ArchivoSubido subido;
            try
            {
                subido = await _archivos.SubirAsync(archivo, entidad.ToString().ToLowerInvariant(), tipo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(502, ex.Message);
            }

            var orden = await _context.Medios
                .Where(m => m.Entidad == entidad && m.EntidadId == id && m.Tipo == tipo)
                .Select(m => (int?)m.Orden)
                .MaxAsync() ?? -1;

            var medio = new Medio
            {
                Entidad = entidad,
                EntidadId = id,
                Tipo = tipo,
                Url = subido.Url,
                Ruta = subido.Ruta,
                Orden = orden + 1
            };

            _context.Medios.Add(medio);
            await _context.SaveChangesAsync();

            return Ok(new MedioDto
            {
                Id = medio.Id,
                Entidad = medio.Entidad,
                EntidadId = medio.EntidadId,
                Tipo = medio.Tipo,
                Url = medio.Url,
                Orden = medio.Orden
            });
        }

        private Task<bool> ExisteAsync(TipoEntidad entidad, int id) => entidad switch
        {
            TipoEntidad.Enemigo => _context.Enemigos.AnyAsync(x => x.Id == id),
            TipoEntidad.Historia => _context.Historias.AnyAsync(x => x.Id == id),
            TipoEntidad.Personaje => _context.Personajes.AnyAsync(x => x.Id == id),
            TipoEntidad.Region => _context.Regiones.AnyAsync(x => x.Id == id),
            TipoEntidad.Vestigio => _context.Vestigios.AnyAsync(x => x.Id == id),
            TipoEntidad.Evento => Task.FromResult(id > 0),   // galería del evento: no tiene tabla
            _ => Task.FromResult(false)
        };
    }
}
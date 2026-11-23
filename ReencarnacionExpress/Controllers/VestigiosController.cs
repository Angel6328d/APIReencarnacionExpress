using Domain.dto;
using Microsoft.AspNetCore.Mvc;
using ReencarnacionExpress.Services.Interfaz;

namespace ReencarnacionExpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VestigiosController : ControllerBase
    {
        private readonly IVestigioService _service;

        public VestigiosController(IVestigioService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<List<VestigioDto>>> Get() =>
            Ok(await _service.ObtenerTodosAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<VestigioDto>> Get(int id)
        {
            var v = await _service.ObtenerPorIdAsync(id);
            return v is null ? NotFound() : Ok(v);
        }

        [HttpPost]
        public async Task<ActionResult<VestigioDto>> Post(GuardarVestigioDto dto)
        {
            var creado = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = creado.Id }, creado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VestigioDto>> Put(int id, GuardarVestigioDto dto)
        {
            var actualizado = await _service.ActualizarAsync(id, dto);
            return actualizado is null ? NotFound() : Ok(actualizado);
        }

        /// <summary>Elimina el registro junto con sus imágenes/videos.</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            await _service.EliminarAsync(id) ? NoContent() : NotFound();
    }
}
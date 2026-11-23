using Domain.dto;
using Microsoft.AspNetCore.Mvc;
using ReencarnacionExpress.Services.Interfaz;

namespace ReencarnacionExpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnemigosController : ControllerBase
    {
        private readonly IEnemigoService _service;

        public EnemigosController(IEnemigoService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<List<EnemigoDto>>> Get() =>
            Ok(await _service.ObtenerTodosAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<EnemigoDto>> Get(int id)
        {
            var e = await _service.ObtenerPorIdAsync(id);
            return e is null ? NotFound() : Ok(e);
        }

        [HttpPost]
        public async Task<ActionResult<EnemigoDto>> Post(GuardarEnemigoDto dto)
        {
            var creado = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = creado.Id }, creado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EnemigoDto>> Put(int id, GuardarEnemigoDto dto)
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
using Domain.dto;
using Microsoft.AspNetCore.Mvc;
using ReencarnacionExpress.Services.Interfaz;

namespace ReencarnacionExpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonajesController : ControllerBase
    {
        private readonly IPersonajeService _service;

        public PersonajesController(IPersonajeService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<List<PersonajeDto>>> Get() =>
            Ok(await _service.ObtenerTodosAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonajeDto>> Get(int id)
        {
            var p = await _service.ObtenerPorIdAsync(id);
            return p is null ? NotFound() : Ok(p);
        }

        [HttpPost]
        public async Task<ActionResult<PersonajeDto>> Post(GuardarPersonajeDto dto)
        {
            var creado = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = creado.Id }, creado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PersonajeDto>> Put(int id, GuardarPersonajeDto dto)
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
using Domain.dto;
using Microsoft.AspNetCore.Mvc;
using ReencarnacionExpress.Services.Interfaz;

namespace ReencarnacionExpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoriasController : ControllerBase
    {
        private readonly IHistoriaService _service;

        public HistoriasController(IHistoriaService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<List<HistoriaDto>>> Get() =>
            Ok(await _service.ObtenerTodosAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<HistoriaDto>> Get(int id)
        {
            var h = await _service.ObtenerPorIdAsync(id);
            return h is null ? NotFound() : Ok(h);
        }

        [HttpPost]
        public async Task<ActionResult<HistoriaDto>> Post(GuardarHistoriaDto dto)
        {
            var creado = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = creado.Id }, creado);
        }
    }
}
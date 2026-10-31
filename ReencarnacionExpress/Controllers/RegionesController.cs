using Domain.dto;
using Microsoft.AspNetCore.Mvc;
using ReencarnacionExpress.Services.Interfaz;

namespace ReencarnacionExpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionesController : ControllerBase
    {
        private readonly IRegionService _service;

        public RegionesController(IRegionService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<List<RegionDto>>> Get() =>
            Ok(await _service.ObtenerTodosAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<RegionDto>> Get(int id)
        {
            var r = await _service.ObtenerPorIdAsync(id);
            return r is null ? NotFound() : Ok(r);
        }

        [HttpPost]
        public async Task<ActionResult<RegionDto>> Post(GuardarRegionDto dto)
        {
            var creado = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = creado.Id }, creado);
        }
    }
}
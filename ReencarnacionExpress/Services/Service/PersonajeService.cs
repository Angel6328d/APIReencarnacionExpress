using Domain.dto;
using Domain.Entities;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using ReencarnacionExpress.Context;
using ReencarnacionExpress.Services.Interfaz;

namespace ReencarnacionExpress.Services.Service
{
    public class PersonajeService : IPersonajeService
    {
        private readonly AppDbContext _context;

        public PersonajeService(AppDbContext context) => _context = context;

        public async Task<List<PersonajeDto>> ObtenerTodosAsync() =>
            await _context.Personajes.AsNoTracking().Select(p => p.ToDto()).ToListAsync();

        public async Task<PersonajeDto?> ObtenerPorIdAsync(int id)
        {
            var p = await _context.Personajes.FindAsync(id);
            return p?.ToDto();
        }

        public async Task<PersonajeDto> CrearAsync(GuardarPersonajeDto dto)
        {
            var p = new Personaje();
            p.Aplicar(dto);
            _context.Personajes.Add(p);
            await _context.SaveChangesAsync();
            return p.ToDto();
        }
    }
}
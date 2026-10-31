using Domain.dto;
using Domain.Entities;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using ReencarnacionExpress.Context;
using ReencarnacionExpress.Services.Interfaz;

namespace ReencarnacionExpress.Services.Service
{
    public class EnemigoService : IEnemigoService
    {
        private readonly AppDbContext _context;

        public EnemigoService(AppDbContext context) => _context = context;

        public async Task<List<EnemigoDto>> ObtenerTodosAsync() =>
            await _context.Enemigos.AsNoTracking().Select(e => e.ToDto()).ToListAsync();

        public async Task<EnemigoDto?> ObtenerPorIdAsync(int id)
        {
            var e = await _context.Enemigos.FindAsync(id);
            return e?.ToDto();
        }

        public async Task<EnemigoDto> CrearAsync(GuardarEnemigoDto dto)
        {
            var e = new Enemigo();
            e.Aplicar(dto);
            _context.Enemigos.Add(e);
            await _context.SaveChangesAsync();
            return e.ToDto();
        }
    }
}
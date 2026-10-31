using Domain.dto;
using Domain.Entities;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using ReencarnacionExpress.Context;
using ReencarnacionExpress.Services.Interfaz;

namespace ReencarnacionExpress.Services.Service
{
    public class VestigioService : IVestigioService
    {
        private readonly AppDbContext _context;

        public VestigioService(AppDbContext context) => _context = context;

        public async Task<List<VestigioDto>> ObtenerTodosAsync() =>
            await _context.Vestigios.AsNoTracking().Select(v => v.ToDto()).ToListAsync();

        public async Task<VestigioDto?> ObtenerPorIdAsync(int id)
        {
            var v = await _context.Vestigios.FindAsync(id);
            return v?.ToDto();
        }

        public async Task<VestigioDto> CrearAsync(GuardarVestigioDto dto)
        {
            var v = new Vestigios();
            v.Aplicar(dto);
            _context.Vestigios.Add(v);
            await _context.SaveChangesAsync();
            return v.ToDto();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using SupermercadoCRUD.Data;
using SupermercadoCRUD2.Models.Entity;

namespace SupermercadoCRUD2.Data.Repositorios
{
    public class VentaRepositorio : IVentaRepositorio
    {
        private readonly SupermercadoContext _context;

        public VentaRepositorio(SupermercadoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Venta>> ObtenerTodasAsync()
        {
            return await _context.Ventas
                .OrderByDescending(v => v.IdVenta)
                .ToListAsync();
        }

        public async Task<Venta?> ObtenerPorIdAsync(int id)
        {
            return await _context.Ventas
                .FirstOrDefaultAsync(v => v.IdVenta == id);
        }

        public async Task<IEnumerable<Venta>> ObtenerPorNombreProductoAsync(string nombreProducto)
        {
            return await _context.Ventas
                .Where(v => v.Producto == nombreProducto)
                .OrderByDescending(v => v.IdVenta)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> ObtenerNombresProductosDistintosAsync()
        {
            return await _context.Ventas
                .Select(v => v.Producto!)
                .Distinct()
                .OrderBy(p => p)
                .ToListAsync();
        }

        public async Task<Venta> InsertarAsync(Venta venta)
        {
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();
            return venta;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return false;
            }

            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

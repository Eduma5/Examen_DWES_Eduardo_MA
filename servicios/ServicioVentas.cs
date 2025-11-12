using SupermercadoCRUD2.Data.Repositorios;
using SupermercadoCRUD2.Models.Dto;
using SupermercadoCRUD2.Models.Entity;

namespace SupermercadoCRUD2.Servicios
{
    public class ServicioVentas
    {
        private readonly IVentaRepositorio _ventaRepositorio;

        public ServicioVentas(IVentaRepositorio ventaRepositorio)
        {
            _ventaRepositorio = ventaRepositorio;
        }

        public async Task<IEnumerable<VentaDto>> ObtenerTodasAsync()
        {
            var ventas = await _ventaRepositorio.ObtenerTodasAsync();
            return ventas.Select(MapearADto);
        }

        public async Task<IEnumerable<VentaDto>> ObtenerPorNombreProductoAsync(string nombreProducto)
        {
            var ventas = await _ventaRepositorio.ObtenerPorNombreProductoAsync(nombreProducto);
            return ventas.Select(MapearADto);
        }

        public async Task<IEnumerable<string>> ObtenerNombresProductosAsync()
        {
            return await _ventaRepositorio.ObtenerNombresProductosDistintosAsync();
        }

        public async Task<VentaDto?> ObtenerPorIdAsync(int id)
        {
            var venta = await _ventaRepositorio.ObtenerPorIdAsync(id);
            return venta != null ? MapearADto(venta) : null;
        }

        public async Task<(bool Exito, string Mensaje, VentaDto? Venta)> CrearVentaAsync(VentaDto ventaDto)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(ventaDto.Producto))
            {
                return (false, "El nombre del producto es obligatorio.", null);
            }

            if (ventaDto.Cantidad <= 0)
            {
                return (false, "La cantidad debe ser mayor a 0.", null);
            }

            if (ventaDto.Precio <= 0)
            {
                return (false, "El precio debe ser mayor a 0.", null);
            }

            // Crear la venta
            var venta = new Venta
            {
                Producto = ventaDto.Producto,
                Cantidad = ventaDto.Cantidad,
                Precio = ventaDto.Precio
            };

            var ventaCreada = await _ventaRepositorio.InsertarAsync(venta);

            return (true, "Venta creada exitosamente.", MapearADto(ventaCreada));
        }

        public async Task<bool> EliminarVentaAsync(int id)
        {
            return await _ventaRepositorio.EliminarAsync(id);
        }

        private VentaDto MapearADto(Venta venta)
        {
            return new VentaDto
            {
                IdVenta = venta.IdVenta,
                Producto = venta.Producto,
                Cantidad = venta.Cantidad,
                Precio = venta.Precio,
                Subtotal = venta.Subtotal
            };
        }
    }
}

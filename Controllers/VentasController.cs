using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupermercadoCRUD2.Models.Dto;
using SupermercadoCRUD2.Servicios;

namespace SupermercadoCRUD2.Controllers
{
    public class VentasController : Controller
    {
        private readonly ServicioVentas _servicioVentas;

        public VentasController(ServicioVentas servicioVentas)
        {
            _servicioVentas = servicioVentas;
        }

        // GET: Ventas
        public async Task<IActionResult> Index(string? filtroProducto)
        {
            IEnumerable<VentaDto> ventas;

            if (!string.IsNullOrWhiteSpace(filtroProducto))
            {
                ventas = await _servicioVentas.ObtenerPorNombreProductoAsync(filtroProducto);
                ViewBag.FiltroActivo = filtroProducto;
            }
            else
            {
                ventas = await _servicioVentas.ObtenerTodasAsync();
            }

            // Obtener lista de productos para el filtro
            var nombresProductos = await _servicioVentas.ObtenerNombresProductosAsync();
            ViewBag.NombresProductos = new SelectList(nombresProductos);

            return View(ventas);
        }

        // GET: Ventas/Create
        public async Task<IActionResult> Create()
        {
            // Obtener productos existentes para el datalist
            var nombresProductos = await _servicioVentas.ObtenerNombresProductosAsync();
            ViewBag.NombresProductos = nombresProductos;
            
            var ventaDto = new VentaDto
            {
                Cantidad = 1,
                Precio = 0
            };

            return View(ventaDto);
        }

        // POST: Ventas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VentaDto ventaDto)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _servicioVentas.CrearVentaAsync(ventaDto);

                if (resultado.Exito)
                {
                    TempData["SuccessMessage"] = resultado.Mensaje;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, resultado.Mensaje);
                }
            }

            // Si llegamos aquí, algo falló, volver a mostrar el formulario
            var nombresProductos = await _servicioVentas.ObtenerNombresProductosAsync();
            ViewBag.NombresProductos = nombresProductos;
            return View(ventaDto);
        }

        // POST: Ventas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _servicioVentas.EliminarVentaAsync(id);

            if (resultado)
            {
                TempData["SuccessMessage"] = "Venta eliminada exitosamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "No se pudo eliminar la venta.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ProyectoApi.Frontend.Models;
using ProyectoApi.Frontend.Services;
using System.Diagnostics;

namespace ProyectoApi.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private IServicioApi _servicioApi;

        public HomeController(IServicioApi servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> Index()
        {
            List<Producto> lista = await _servicioApi.Lista();
            return View(lista);
        }

        public async Task<IActionResult> Producto(int idProducto)
        {
            Producto modelo_producto = new Producto();

            ViewBag.Accion = "Nuevo Producto";

            if (idProducto != 0)
            {
                ViewBag.Accion = "Editar Producto";
                modelo_producto = await _servicioApi.Obtener(idProducto);
            }

            return View(modelo_producto);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(Producto ob_producto)
        {
            bool respuesta;

            if (ob_producto.Id == 0)
            {
                respuesta = await _servicioApi.Guardar(ob_producto);
            }
            else
            {
                respuesta = await _servicioApi.Editar(ob_producto);
            }

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int idProducto)
        {
            var respuesta = await _servicioApi.Eliminar(idProducto);

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
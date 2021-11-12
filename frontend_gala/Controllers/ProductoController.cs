using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BLL;
using Entity;

namespace frontend_gala.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        private ProductoService productoService;

        public ProductoController()
        {
            productoService = new ProductoService("Server=DESKTOP-DHT77N8\\SQLEXPRESS;Database=proyecto_gala_DB;Trusted_Connection = true; MultipleActiveResultSets = true");
        }

        [HttpPost]
        public ActionResult<Producto> PostPago(Producto producto)
        {
            Producto _producto = MapearaProducto(producto);
            var response = productoService.Guardar(_producto);
            return Ok(_producto);
        }

        private Producto MapearaProducto(Producto producto)
        {
            var _producto = new Producto();
            _producto.IdProducto = producto.IdProducto;
            _producto.CaracteristicasProdcuto = producto.CaracteristicasProdcuto;
            _producto.Categoria = producto.Categoria;
            _producto.DetalleProducto = producto.DetalleProducto;
            _producto.ImagenesProducto = producto.ImagenesProducto;
            _producto.ImagenPrincipal = producto.ImagenPrincipal;
            _producto.PrecioProducto = producto.PrecioProducto;
            _producto.Talla = producto.Talla;

            return _producto;
        }
    }
}
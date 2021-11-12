using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BLL;
using Entity;
using frontend_gala.models;

namespace frontend_gala.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CiudadController : ControllerBase
    {
        private CiudadService ciudadService;

        public CiudadController()
        {
            ciudadService = new CiudadService("Server=DESKTOP-DHT77N8\\SQLEXPRESS;Database=proyecto_gala_DB;Trusted_Connection = true; MultipleActiveResultSets = true");
        }

        [HttpPost]
        public ActionResult<CiudadViewModel> PostPago(CiudadInputModel ciudadInputModel)
        {
            Ciudad _ciudad = MapearaCiudad(ciudadInputModel);
            ciudadService.Guardar(_ciudad);
            return Ok();
        }

        private Ciudad MapearaCiudad(CiudadInputModel ciudadInputModel)
        {
            var _ciudad = new Ciudad();
            _ciudad.nombre = ciudadInputModel.nombre;
            _ciudad.costoEnvio = ciudadInputModel.costoEnvio;
            _ciudad.codigoPostal = ciudadInputModel.codigoPostal;

            return _ciudad;
        }
    }
}
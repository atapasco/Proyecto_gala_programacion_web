using System.Collections.Generic;
using Entity;

namespace frontend_gala.models
{
    public class CiudadInputModel
    {
        public string nombre {get; set;}
        public double costoEnvio {get; set;}
        public List<string> codigoPostal {get; set;}
    }
    public class CiudadViewModel : CiudadInputModel
    {
        public CiudadViewModel(Ciudad ciudad)
        {
            this.nombre = ciudad.nombre;
            this.costoEnvio = ciudad.costoEnvio;
            this.codigoPostal = ciudad.codigoPostal;
        }   
    }
}
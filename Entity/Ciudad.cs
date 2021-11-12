using System.Collections.Generic;

namespace Entity
{
    public class Ciudad
    {    
        public string nombre {get; set;}
        public List<string> codigoPostal {get; set;}
        public double costoEnvio {get; set;}
        public Ciudad(string nombre, List<string> codigosPostales, double costoEnvio)
        {
            this.nombre = nombre;
            this.codigoPostal = codigoPostal;
            this.costoEnvio = costoEnvio;
        }        
        
        public Ciudad()
        {

        }
    }
}
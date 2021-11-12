using System;

namespace Entity
{
    public class Domicilio
    {
        public int IdDomicilio {set; get;}
        public Direccion Direccion {set; get;}
        public string NombreCiudad {set; get;}
        public string CodigoPostal {set; get;}
        public double CostoEnvio {set; get;}
        public DateTime FechaEnvio {set; get;}
        public DateTime FechaEntrega {set; get;}
        public double CostoTotalEntrega {set; get;}

        public Domicilio(Direccion direccion, string nombreCiudad, string codigoPostal,
                         double costoEnvio, double CostoTotalEntrega)
        {
            this.IdDomicilio = 0;
            this.Direccion = direccion;
            this.NombreCiudad = nombreCiudad;
            this.CodigoPostal = codigoPostal;
            this.CostoEnvio = costoEnvio;
            this.CostoTotalEntrega = costoEnvio;
            this.FechaEnvio = DateTime.Now;
            this.FechaEntrega.AddDays(7);
        }
    }
}
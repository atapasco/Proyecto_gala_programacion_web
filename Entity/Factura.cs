using System;
using System.Collections.Generic;

namespace Entity
{
    public class Factura
    {
        public int IdFactura {set; get;}
        public DateTime FechaVenta {set; get;} 
        public double ValorTotalSinIva {set; get;}
        public string IdCliente {set; get;}
        public int IdDomicilio {set; get;}
        public double Iva {set; get;}
        public double ValorTotalConIva {set; get;}
        public List<DetalleFactura> DetallesFactura {set; get;}

        public Factura(string idCliente, int idDomicilio, List<DetalleFactura> detallesFactura)
        {
            this.IdFactura = 0;
            this.FechaVenta = DateTime.Now;
            this.ValorTotalSinIva = SacarValorTotalSinIva(detallesFactura);
            this.IdCliente = idCliente;
            this.IdDomicilio = idDomicilio;
            this.Iva = SacarValorTotalSinIva(detallesFactura) * (19 / 100);
            this.ValorTotalConIva = SacarValorTotalConIva(detallesFactura);
            this.DetallesFactura = detallesFactura;
        }
        public Factura()
        {

        }

        public double SacarValorTotalSinIva(List<DetalleFactura> detallesFactura)
        {
            double valorTotal = 0;
            foreach(var item in detallesFactura)
            {
                valorTotal = item.ValorTotal + valorTotal;
            }

            return valorTotal;
        }

        public double SacarValorTotalConIva(List<DetalleFactura> detallesFactura)
        {
            return (SacarValorTotalSinIva(detallesFactura) * (19 / 100)) + SacarValorTotalSinIva(detallesFactura);
        }
    }
}

using System.Collections.Generic;

namespace Entity
{
    public class Finanzas
    {
        public List<Factura> Facturas {set; get;}
        public List<Domicilio> Domicilios {set; get;}
        public double DomiciliosDevueltos {set; get;}
        public double ValorFacturas {set; get;}
        public double TotalDomicilios {set; get;}
        public double GananciasTotales {set; get;}
        public int ProductosVendidos {set; get;}

        public Finanzas(List<Factura> facturas, List<Domicilio> domicilios)
        {
            this.Facturas = facturas;
            this.Domicilios = domicilios;
            this.DomiciliosDevueltos = CalcularDomiciliosDevueltos(domicilios);
            this.ValorFacturas = CalcularTotalFacturas(facturas);
            this.TotalDomicilios = CalcularDomicilios(domicilios);
            this.GananciasTotales = CalcularGananciasTotales(facturas, domicilios);
            this.ProductosVendidos = CalcularProductosVendidos(facturas);
        }

        public double CalcularDomicilios(List<Domicilio> domicilios)
        {
            double costoDomicilios = 0;
            foreach(var item in domicilios)
            {
                costoDomicilios = item.CostoTotalEntrega + costoDomicilios;
            }

            return costoDomicilios;
        }
        public double CalcularDomiciliosDevueltos(List<Domicilio> domicilios)
        {
            double costoDomiciliosDevueltos = 0;
            foreach(var item in domicilios)
            {
                costoDomiciliosDevueltos = costoDomiciliosDevueltos + item.CostoTotalEntrega;
            }
            costoDomiciliosDevueltos = costoDomiciliosDevueltos - CalcularDomicilios(domicilios);

            return costoDomiciliosDevueltos;
        } 
        public double CalcularTotalFacturas(List<Factura> facturas)
        {
            double totalFacturas = 0;
            foreach(var item in facturas)
            {
                totalFacturas = item.ValorTotalConIva + totalFacturas;
            }

            return totalFacturas;
        }
        public double CalcularGananciasTotales(List<Factura> facturas, List<Domicilio> domicilios)
        {
            return CalcularTotalFacturas(facturas) - CalcularDomicilios(domicilios);
        }
        public int CalcularProductosVendidos(List<Factura> facturas)
        {
            int cantidadProductos = 0;
            foreach(var item in facturas)
            {
                foreach(var item2 in item.DetallesFactura)
                {
                    cantidadProductos = item2.CantidadProducto + cantidadProductos;
                }
            }

            return cantidadProductos;
        }
    }
}
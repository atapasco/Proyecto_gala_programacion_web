using System.Collections.Generic;
using System;

namespace Entity
{
    public class Producto
    {
        public string Categoria {set; get;}
        public string IdProducto {set; get;}
        public List<string> Talla {set; get;}
        public String DetalleProducto {set; get;} 
        public double PrecioProducto {set; get;}
        public String CaracteristicasProdcuto {set; get;}
        public byte[] ImagenPrincipal {set; get;}
        public List<byte[]> ImagenesProducto {set; get;}

        public Producto(string categoria, string idProducto, List<string> talla, String detalleProducto,
                        double precioProducto, String caracteristicasProdcuto, byte[] imagenPrincipal,
                        List<byte[]> imagenesProducto)
        {
            this.Categoria = categoria;
            this.IdProducto = idProducto;
            this.Talla = talla;
            this.DetalleProducto = detalleProducto;
            this.PrecioProducto = precioProducto;
            this.CaracteristicasProdcuto = caracteristicasProdcuto;
            this.ImagenPrincipal = imagenPrincipal;
            this.ImagenesProducto = imagenesProducto;
        }
        public Producto()
        {

        }
    }
}
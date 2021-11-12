namespace Entity
{
    public class DetalleFactura
    {
        public Producto Producto {set; get;}
        public int CantidadProducto {set; get;}
        public double ValorTotal {set; get;}
        public bool ProductoDevuelto {set; get;}

        public DetalleFactura(Producto producto, int cantidadProducto)
        {
            this.Producto = producto;
            this.CantidadProducto = cantidadProducto;
            this.ValorTotal = producto.PrecioProducto * cantidadProducto;
            this.ProductoDevuelto = false;
        } 
    }
}
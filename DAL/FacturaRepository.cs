using System.Data.SqlClient;
using Entity;
using System.Collections.Generic;
using System.IO;

namespace DAL
{
    public class FacturaRepository
    {

        private SqlConnection _connection;
        ProductoRepository productoRepository;

        public FacturaRepository(ConectionManager connection)
        {
            _connection = connection.connection;
            productoRepository = new ProductoRepository(connection);
        }

        public void GuardarFactura(Factura factura)
        {
            using (var comand = _connection.CreateCommand())
            {
                comand.CommandText = "INSERT INTO FACTURA (fecha_venta, valor_total_sin_iva, valor_total_con_iva, id_cliente, iva, id_domicilio)" +
                                                "VALUES (@fecha_venta, @valor_total_sin_iva, @valor_total_con_iva, @id_cliente, @iva, @id_domicilio)";
                comand.Parameters.AddWithValue("@fecha_venta", factura.FechaVenta);
                comand.Parameters.AddWithValue("@valor_total_sin_iva", factura.ValorTotalSinIva);
                comand.Parameters.AddWithValue("@valor_total_con_iva", factura.ValorTotalConIva);
                comand.Parameters.AddWithValue("@id_cliente", factura.IdCliente);
                comand.Parameters.AddWithValue("@iva", factura.Iva);
                comand.Parameters.AddWithValue("@id_domicilio", factura.IdDomicilio);

                comand.ExecuteNonQuery();

                GuardarDetalleFactura(factura.DetallesFactura);
            }
        }

        public void GuardarDetalleFactura(List<DetalleFactura> detalleFactura)
        {
            using (var comand = _connection.CreateCommand())
            {
                foreach (var item in detalleFactura)
                {
                    comand.CommandText = "INSERT INTO DETALLE_FACTURA (id_factura, id_producto, cantidad_producto, valor_total, producto_devuelto)" +
                                                      "VALUES (@id_factura, @id_producto, @cantidad_producto, @valor_total, @producto_devuelto)";

                    //el id es menos uno(-1) por que en la base de datos en la tabla detalle Factura hay un disparador que trabaja con este valor
                    comand.Parameters.AddWithValue("@id_factura", -1);
                    comand.Parameters.AddWithValue("@id_producto", item.Producto.IdProducto);
                    comand.Parameters.AddWithValue("@cantidad_producto", item.CantidadProducto);
                    comand.Parameters.AddWithValue("@valor_total", item.ValorTotal);
                    comand.Parameters.AddWithValue("@producto_devuelto", item.ProductoDevuelto);

                    comand.ExecuteNonQuery();
                }
            }
        }

        public List<Factura> ConsultarFacturas()
        {
            List<Factura> facturas = new List<Factura>();

            using (var comando = _connection.CreateCommand())
            {
                comando.CommandText = "SELECT id_factura,fecha_venta,valor_total_sin_iva,valor_total_con_iva,id_cliente,iva,id_domicilio FROM FACTURA";
                var lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        Factura factura = new Factura();
                        factura.IdFactura = lector.GetInt32(0);
                        factura.FechaVenta = lector.GetDateTime(1);
                        factura.ValorTotalSinIva = lector.GetDouble(2);
                        factura.ValorTotalConIva = lector.GetDouble(3);
                        factura.IdCliente = lector.GetString(4);
                        factura.Iva = lector.GetDouble(5);
                        factura.IdDomicilio = lector.GetInt32(6);
                        factura.DetallesFactura = ConsultarDetallesFacturas(lector.GetInt32(0));

                        facturas.Add(factura);
                    }
                }
                lector.Close();
            }
            return facturas;
        }

        public List<Factura> ConsultarFacturasPorIdCliente(string idCliente)
        {
            List<Factura> facturas = new List<Factura>();

            using (var comando = _connection.CreateCommand())
            {
                comando.CommandText = "SELECT id_factura,fecha_venta,valor_total_sin_iva,valor_total_con_iva,id_cliente,iva,id_domicilio" + 
                                        "FROM FACTURA WHERE id_cliente = @id_cliente";
                comando.Parameters.AddWithValue("@id_cliente",idCliente);
                var lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        Factura factura = new Factura();
                        factura.IdFactura = lector.GetInt32(0);
                        factura.FechaVenta = lector.GetDateTime(1);
                        factura.ValorTotalSinIva = lector.GetDouble(2);
                        factura.ValorTotalConIva = lector.GetDouble(3);
                        factura.IdCliente = lector.GetString(4);
                        factura.Iva = lector.GetDouble(5);
                        factura.IdDomicilio = lector.GetInt32(6);
                        factura.DetallesFactura = ConsultarDetallesFacturas(lector.GetInt32(0));

                        facturas.Add(factura);
                    }
                }
                lector.Close();
            }
            return facturas;
        }

        private List<DetalleFactura> ConsultarDetallesFacturas(int idFactura)
        {
            List<DetalleFactura> detalleFacturas = new List<DetalleFactura>();

            using (var comando = _connection.CreateCommand())
            {
                comando.CommandText = "SELECT id_producto,cantidad_producto,valor_total,producto_devuelto FROM DETALLE_FACTURA WHERE id_factura = @id_factura";
                comando.Parameters.AddWithValue("@id_factura",idFactura);
                var lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        DetalleFactura detalleFactura = new DetalleFactura();
                        ConsultarProducto(lector.GetString(0));
                        detalleFactura.CantidadProducto = lector.GetInt32(1);
                        detalleFactura.ValorTotal = lector.GetDouble(2);
                        detalleFactura.ProductoDevuelto = lector.GetBoolean(3);

                        detalleFacturas.Add(detalleFactura);
                    }
                }
                lector.Close();
            }
            return detalleFacturas;
        }

        private Producto ConsultarProducto(string idProducto)
        {
            return productoRepository.ConsultarProductoPorId(idProducto);
        }

    }
}
using System.Data.SqlClient;
using Entity;
using System.Collections.Generic;
using System.IO;

namespace DAL
{

    public class ProductoRepository

    {

        private SqlConnection _connection;

        public ProductoRepository(ConectionManager connection)
        {
            _connection = connection.connection;
        }

        public void Guardar(Producto producto)
        {
            using (var comand = _connection.CreateCommand())
            {
                comand.CommandText = "INSERT INTO PRODUCTO (categoria, id_producto, talla, detalle_producto, precio_producto, caracteristicas_producto, imagen_principal)" +
                                     " VALUES (@categoria, @id_producto, @talla, @detalle_producto, @precio_producto, @caracteristicas_producto, @imagen_principal)";
                comand.Parameters.AddWithValue("@categoria", producto.Categoria);
                comand.Parameters.AddWithValue("@id_producto", producto.IdProducto);
                comand.Parameters.AddWithValue("@talla", "M");
                GuardarTallasDeLosProductos(producto.Talla, producto.IdProducto);
                comand.Parameters.AddWithValue("@detalle_producto", producto.DetalleProducto);
                comand.Parameters.AddWithValue("@precio_producto", producto.PrecioProducto);
                comand.Parameters.AddWithValue("@caracteristicas_producto", producto.CaracteristicasProdcuto);
                comand.Parameters.AddWithValue("@imagen_principal", producto.ImagenPrincipal);
                GuardarImagenesDeLosProductos(producto.ImagenesProducto, producto.IdProducto);

                comand.ExecuteNonQuery();
            }
        }

        public List<Producto> ConsultarProductos()
        {
            List<Producto> productos = new List<Producto>();
            using (var comando = _connection.CreateCommand())
            {


                comando.CommandText = "SELECT categoria, id_producto, talla, detalle_producto, precio_producto, caracteristicas_producto, imagen_principal FROM PRODUCTO";
                var lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        Producto producto = new Producto();
                        producto.Categoria = lector.GetString(0);
                        producto.IdProducto = lector.GetString(1);
                        //2
                        producto.Talla = ConsultarTallasDeLosProductos(lector.GetString(1));
                        producto.DetalleProducto = lector.GetString(3);
                        producto.PrecioProducto = lector.GetDouble(4);
                        producto.CaracteristicasProdcuto = lector.GetString(5);
                        producto.ImagenPrincipal = ConvertirUnStreamABytes(lector.GetStream(6));
                        //7
                        producto.ImagenesProducto = ConsultarImagenesDeLosProductos(lector.GetString(1));

                        productos.Add(producto);
                    }
                }
                lector.Close();


            }
            return productos;
        }

        public List<Producto> ConsultarProductoFavoritosCliente(string idCliente)
        {
            List<Producto> productos = new List<Producto>();
            using (var comando = _connection.CreateCommand())
            {
                comando.CommandText = "SELECT categoria, id_producto, talla, detalle_producto, precio_producto, caracteristicas_producto, imagen_principal" +
                                        "FROM PRODUCTO,PRODUCTOS_FAVORITOS"+ 
                                        "WHERE PRODUCTO.id_producto=PRODUCTOS_FAVORITOS.id_producto AND PRODUCTOS_FAVORITOS.id_cliente = @id_cliente";
                comando.Parameters.AddWithValue("@id_cliente", idCliente);
                var lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        Producto producto = new Producto();
                        producto.Categoria = lector.GetString(0);
                        producto.IdProducto = lector.GetString(1);
                        //2
                        producto.Talla = ConsultarTallasDeLosProductos(lector.GetString(1));
                        producto.DetalleProducto = lector.GetString(3);
                        producto.PrecioProducto = lector.GetDouble(4);
                        producto.CaracteristicasProdcuto = lector.GetString(5);
                        producto.ImagenPrincipal = ConvertirUnStreamABytes(lector.GetStream(6));
                        //7
                        producto.ImagenesProducto = ConsultarImagenesDeLosProductos(lector.GetString(1));

                        productos.Add(producto);
                    }
                }
                lector.Close();
            }
            return productos;
        }
        public void EliminarProducto(string idProducto)
        {
            using (var comando = _connection.CreateCommand())
            {
                comando.CommandText = "DELETE FROM PRODUCTO WHERE id_producto = @id_producto";
                comando.Parameters.AddWithValue("@id_producto", idProducto);
                EliminarImagenesDelProducto(idProducto);
                EliminarTallasDelProducto(idProducto);

                comando.ExecuteNonQuery();
            }

        }

        private void EliminarImagenesDelProducto(string idProducto)
        {
            using (var comando = _connection.CreateCommand())
            {
                comando.CommandText = "DELETE FROM ASIGNACION_IMAGENES WHERE id_producto = @id_producto";
                comando.Parameters.AddWithValue("@id_producto", idProducto);

                comando.ExecuteNonQuery();
            }
        }

        private void EliminarTallasDelProducto(string idProducto)
        {
            using (var comando = _connection.CreateCommand())
            {
                comando.CommandText = "DELETE FROM ASIGNACION_TALLAS WHERE id_producto = @id_producto";
                comando.Parameters.AddWithValue("@id_producto", idProducto);

                comando.ExecuteNonQuery();
            }
        }

        private void GuardarTallasDeLosProductos(List<string> tallas, string idProducto)
        {
            using (var comand = _connection.CreateCommand())
            {
                foreach (var item in tallas)
                {
                    comand.CommandText = "INSERT INTO ASIGNACION_TALLAS (id_producto, tallas) VALUES (@id_producto, @tallas)";
                    comand.Parameters.AddWithValue("@id_producto", idProducto);
                    comand.Parameters.AddWithValue("@tallas", item);

                    comand.ExecuteNonQuery();
                }
            }
        }

        private void GuardarImagenesDeLosProductos(List<byte[]> imagenes, string idProducto)
        {
            using (var comand = _connection.CreateCommand())
            {
                foreach (var item in imagenes)
                {
                    comand.CommandText = "INSERT INTO ASIGNACION_IMAGENES (id_producto, imagen_producto) VALUES (@id_producto, @imagen_producto)";
                    comand.Parameters.AddWithValue("@id_producto", idProducto);
                    comand.Parameters.AddWithValue("@imagen_producto", item);

                    comand.ExecuteNonQuery();
                }
            }
        }

        private List<string> ConsultarTallasDeLosProductos(string id)
        {
            List<string> tallas = new List<string>();
            using (var comando = _connection.CreateCommand())
            {
                comando.CommandText = "SELECT tallas FROM ASIGNACION_TALLAS WHERE id_producto = @id_producto";
                comando.Parameters.AddWithValue("@id_producto", id);
                var lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        tallas.Add(lector.GetString(1));
                    }
                }
                lector.Close();
            }
            return tallas;
        }

        private List<byte[]> ConsultarImagenesDeLosProductos(string id)
        {
            List<byte[]> imagenes = new List<byte[]>();
            using (var comando = _connection.CreateCommand())
            {
                comando.CommandText = "SELECT imagen_producto FROM ASIGNACION_TALLAS WHERE id_producto = @id_producto";
                comando.Parameters.AddWithValue("@id_producto", id);
                var lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        imagenes.Add(ConvertirUnStreamABytes(lector.GetStream(1)));
                    }
                }
                lector.Close();
            }
            return imagenes;
        }
        
        public Producto ConsultarProductoPorId(string idProducto)
        {
            Producto producto = new Producto();
            using (var comando = _connection.CreateCommand())
            {
                
                comando.CommandText = "SELECT categoria, id_producto, talla, detalle_producto, precio_producto, caracteristicas_producto, imagen_principal" +
                                        "FROM PRODUCTO WHERE id_producto = @id_producto";
                comando.Parameters.AddWithValue("@id_producto", idProducto);
                var lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        producto.Categoria = lector.GetString(0);
                        producto.IdProducto = lector.GetString(1);
                        //2
                        producto.Talla = ConsultarTallasDeLosProductos(lector.GetString(1));
                        producto.DetalleProducto = lector.GetString(3);
                        producto.PrecioProducto = lector.GetDouble(4);
                        producto.CaracteristicasProdcuto = lector.GetString(5);
                        producto.ImagenPrincipal = ConvertirUnStreamABytes(lector.GetStream(6));
                        //7
                        producto.ImagenesProducto = ConsultarImagenesDeLosProductos(lector.GetString(1));
                    }
                }
                lector.Close();
                
                return producto;
            }
        }

        private byte[] ConvertirUnStreamABytes(Stream imagen)
        {
            MemoryStream mS = new MemoryStream();
            imagen.CopyTo(mS);

            return mS.ToArray();
        }
    }
}
using System.Data.SqlClient;
using Entity;
using System.Collections.Generic;
using System.IO;

namespace DAL
{
    public class ClienteRepository
    {

        private SqlConnection _connection;
        ProductoRepository productoRepository;

        public ClienteRepository(ConectionManager connection)
        {
            _connection = connection.connection;
            productoRepository = new ProductoRepository(connection);
        }

        public void CrearCuenta(Cliente cliente)
        {
            using (var comand = _connection.CreateCommand())
            {
                comand.CommandText = "INSERT INTO CUENTA (id_cliente ,nombres ,apellidos ,genero,fecha_nacimiento,correo,clave)"+
                                                  "VALUES (@id_cliente,@nombres,@apellidos,@genero,@fecha_nacimiento,@correo,@clave)";

                comand.Parameters.AddWithValue("@id_cliente", cliente.IdCliente);                                  
                comand.Parameters.AddWithValue("@nombres", cliente.Nombres);
                comand.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
                comand.Parameters.AddWithValue("@genero", cliente.Genero);
                comand.Parameters.AddWithValue("@fecha_nacimiento", cliente.FechaNacimiento);
                comand.Parameters.AddWithValue("@correo", cliente.Cuenta.Correo);
                comand.Parameters.AddWithValue("@clave", cliente.Cuenta.Clave);

                comand.ExecuteNonQuery();
            }
        }

        public Cliente CargarCuenta(string correo)
        {
            Cliente cliente = new Cliente();
            using (var comando = _connection.CreateCommand())
            {
                
                comando.CommandText = "SELECT id_cliente, nombres, apellidos, genero, fecha_nacimiento, correo, clave FROM CUENTA WHERE correo = @correo";
                comando.Parameters.AddWithValue("@correo", correo);
                var lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        cliente.IdCliente = lector.GetString(0);
                        cliente.Nombres = lector.GetString(1);
                        cliente.Apellidos = lector.GetString(2);
                        cliente.Genero = lector.GetChar(3);
                        cliente.FechaNacimiento = lector.GetDateTime(4);
                        cliente.Cuenta.Correo = lector.GetString(5);
                        cliente.Cuenta.Clave = lector.GetString(6);
                    }
                }
                lector.Close();
            }
            return cliente;
        }

        public List<Cliente> ConsultarClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            using (var comando = _connection.CreateCommand())
            {
                comando.CommandText = "SELECT id_cliente, nombres, apellidos, genero, fecha_nacimiento, correo FROM CUENTA";
                var lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        Cliente cliente = new Cliente();
                        cliente.IdCliente = lector.GetString(0);
                        cliente.Nombres = lector.GetString(1);
                        cliente.Apellidos = lector.GetString(2);
                        cliente.Genero = lector.GetChar(3);
                        cliente.FechaNacimiento = lector.GetDateTime(4);
                        cliente.Cuenta.Correo = lector.GetString(5);

                        clientes.Add(cliente);
                    }
                }
                lector.Close();
            }
            return clientes;
        }

        public void CambiarClave(string claveNueva, string idCliente)
        {
            using (var comand = _connection.CreateCommand())
            {
                comand.CommandText = "UPDATE CUENTA SET clave = @clave WHERE id_cliente = @id_cliente";
                comand.Parameters.AddWithValue("@id_cliente", idCliente);                                  
                comand.Parameters.AddWithValue("@id_clave", claveNueva);

                comand.ExecuteNonQuery();
            }
        }

        public void AñadirProductoFavorito(string idCliente, string idProducto)
        {
            using (var comand = _connection.CreateCommand())
            {
                comand.CommandText = "INSERT INTO PRODUCTOS_FAVORITOS (id_cliente,id_producto)"+
                                                  "VALUES (@id_cliente,@id_producto)";

                comand.Parameters.AddWithValue("@id_cliente", idCliente);                                  
                comand.Parameters.AddWithValue("@id_producto", idProducto);

                comand.ExecuteNonQuery();
            }
        }
        public void AñadirFacturas(string idCliente)
        {
            using (var comand = _connection.CreateCommand())
            {
                comand.CommandText = "INSERT INTO ASIGNACION_FACTURAS (id_cliente,id_factura)"+
                                                  "VALUES (@id_cliente,@id_factura)";

                comand.Parameters.AddWithValue("@id_cliente", idCliente);                                  
                comand.Parameters.AddWithValue("@id_factura", -1);

                comand.ExecuteNonQuery();
            }
        }
        public void AñadirDirecciones(string idCliente, string idDireccion)
        {
            using (var comand = _connection.CreateCommand())
            {
                comand.CommandText = "INSERT INTO ASIGNACION_FACTURAS (id_cliente,id_direccion)"+
                                                  "VALUES (@id_cliente,@id_direccion)";

                comand.Parameters.AddWithValue("@id_cliente", idCliente);                                  
                comand.Parameters.AddWithValue("@id_direccion", idDireccion);

                comand.ExecuteNonQuery();
            }
        }

        public List<Producto> ConsultarProductosFavoritos(string idCliente)
        {
            return productoRepository.ConsultarProductoFavoritosCliente(idCliente);
        }
    }
}
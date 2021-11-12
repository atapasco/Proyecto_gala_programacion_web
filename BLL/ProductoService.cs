using System;
using System.Data.SqlClient;
using DAL;
using Entity;

namespace BLL
{
    public class ProductoService
    {
        private ConectionManager connection;
        private ProductoRepository productoRepository;

        public ProductoService (string connectionString)
        {
            connection = new ConectionManager(connectionString);
            productoRepository = new ProductoRepository(connection);
        }

        public ProductoGuardarResponse Guardar(Producto producto)
        {
            try
            {
                connection.Open();
                productoRepository.Guardar(producto);
                return new ProductoGuardarResponse("Se guardo correctamente", false);
            }
            catch (Exception e)
            {
                return new ProductoGuardarResponse("Error al guardar el producto " + e.Message, true);
            }
            finally
            {
                connection.Close();
            }
        }

        public class ProductoGuardarResponse
        {
            public string mensaje { set; get; }
            public bool error { set; get; }

            public ProductoGuardarResponse(string mensaje, bool error)
            {
                this.mensaje = mensaje;
                this.error = error;
            }
        }
        
    }
}

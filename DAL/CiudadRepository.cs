using System.Data.SqlClient;
using Entity;
using System.Collections.Generic;
using System.IO;

namespace DAL
{
    public class CiudadRepository
    {
        private SqlConnection _connection;

        public CiudadRepository(ConectionManager connection)
        {
            _connection = connection.connection;
        }

        public void Guardar(Ciudad ciudad)
        {
            using (var comand = _connection.CreateCommand())
            {
                comand.CommandText = "INSERT INTO CIUDAD (nombre, costo)" +
                                     " VALUES (@nombre, @costo)";
                comand.Parameters.AddWithValue("@nombre", ciudad.nombre);
                comand.Parameters.AddWithValue("@costo", ciudad.costoEnvio);   

                comand.ExecuteNonQuery();             
            }
        }
        //da
    }
}
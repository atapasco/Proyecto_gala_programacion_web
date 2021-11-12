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
                GuardarCodigosPostales(ciudad.codigoPostal, ciudad.nombre);
                comand.Parameters.AddWithValue("@costo", ciudad.costoEnvio);

                comand.ExecuteNonQuery();
            }
        }

        public List<Ciudad> ConsultarCiudades()
        {
            List<Ciudad> ciudades = new List<Ciudad>();
            using (var comando = _connection.CreateCommand())
            {
                comando.CommandText = "SELECT nombre, costo FROM CIUDAD";
                var lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        Ciudad ciudad = new Ciudad();
                        ciudad.nombre = lector.GetString(0);
                        ciudad.codigoPostal = ConsultarCodigosPostales(lector.GetString(0));
                        ciudad.costoEnvio = lector.GetDouble(1);

                        ciudades.Add(ciudad);
                    }
                }
                lector.Close();
            }
            return ciudades;
        }

        public void EliminarCiudad(string nombreCiudad)
        {
            using (var comando = _connection.CreateCommand())
            {
                comando.CommandText = "DELETE FROM CIUDAD WHERE nombre_ciudad = @nombre_ciudad";
                comando.Parameters.AddWithValue("@nombre_ciudad", nombreCiudad);
                EliminarCodigosPostales(nombreCiudad);

                comando.ExecuteNonQuery();
            }
        }

        private void EliminarCodigosPostales(string nombreCiudad)
        {
            using (var comando = _connection.CreateCommand())
            {
                comando.CommandText = "DELETE FROM ASIGNACION_CODIGO_POSTAL WHERE nombre_ciudad = @nombre_ciudad";
                comando.Parameters.AddWithValue("@nombre_ciudad", nombreCiudad);

                comando.ExecuteNonQuery();
            }
        }

        private List<string> ConsultarCodigosPostales(string nombreCiudad)
        {
            List<string> coodigosPostales = new List<string>();
            string codigoPostal = null;

            using (var comando = _connection.CreateCommand())
            {
                comando.CommandText = "SELECT nombre_ciudad, codigo_postal FROM ASIGNACION_CODIGO_POSTAL WHERE nombre_ciudad = @nombre_ciudad";
                comando.Parameters.AddWithValue("@nombre_ciudad", nombreCiudad);
                var lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        codigoPostal = lector.GetString(1);

                        coodigosPostales.Add(codigoPostal);
                    }
                }
                lector.Close();
            }
            return coodigosPostales;

        }

        private void GuardarCodigosPostales(List<string> codigosPostales, string nombreCiudad)
        {

            using (var comand = _connection.CreateCommand())
            {
                foreach (var item in codigosPostales)
                {

                    comand.CommandText = "INSERT INTO ASIGNACION_CODIGO_POSTAL (nombre_ciudad, codigo_postal)" +
                                         " VALUES (@nombre_ciudad, @codigo_postal)";
                    comand.Parameters.AddWithValue("@nombre_ciudad", nombreCiudad);
                    comand.Parameters.AddWithValue("@codigo_postal", item);

                    comand.ExecuteNonQuery();
                }
            }
        }
    }
}
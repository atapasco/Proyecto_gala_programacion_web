using System;
using System.Data.SqlClient;
using DAL;
using Entity;

namespace BLL
{
    public class CiudadService
    {
        private ConectionManager connection;
        private CiudadRepository ciudadRepository;

        public CiudadService (string connectionString)
        {
            connection = new ConectionManager(connectionString);
            ciudadRepository = new CiudadRepository(connection);
        }

        public void Guardar(Ciudad ciudad)
        {
            try
            {
                connection.Open();
                ciudadRepository.Guardar(ciudad);
            }
            catch 
            {

            }
            finally
            {
                connection.Close();
            }
        }
    }
}
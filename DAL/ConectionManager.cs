using System;
using System.Data.SqlClient;

namespace DAL
{
    public class ConectionManager
    {
        public SqlConnection connection;

        public ConectionManager(string cadena_coneccion)
        {
            connection = new SqlConnection(cadena_coneccion);
        }

        public void Open()
        {
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }
    }
}

using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Entity
{
    public class Cuenta
    {
        public string Correo {set; get;}
        public string Clave {set; get;}
        public List<Producto> ProductosFavoritos {set; get;}
        public List<Factura> Facturas {set; get;}
        public List<Direccion> Direcciones {set; get;}

        public Cuenta(string correo, string clave, List<Producto> productosFavoritos,
                      List<Factura> facturas, List<Direccion> direcciones)
        {
            this.Correo = correo;
            this.Clave = EncriptarClave(clave);
            this.ProductosFavoritos = productosFavoritos;
            this.Facturas = facturas;
            this.Direcciones = direcciones;
        }

        public string EncriptarClave(string clave)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(clave));
            for (int i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("(0:x2)", stream[i]);
            }

            return sb.ToString();
        }

        public bool ComprobarClave(string clave)
        {
            if(this.Clave == EncriptarClave(clave))
            {
                return true;
            }
            return false;
        }

        public void CambiarClave(string claveNueva)
        {
            this.Clave = EncriptarClave(claveNueva);
        }
    }
}
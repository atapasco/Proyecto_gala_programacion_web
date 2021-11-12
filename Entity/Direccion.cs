namespace Entity
{
    public class Direccion
    {
        public string NombreDireccion {set; get;}
        public string NombreCiudad {set; get;}
        public string CodigoPostal {set; get;}

        public Direccion(string direccion, string nombreCiudad, string codigoPostal)
        {
            this.NombreDireccion = direccion;
            this.NombreCiudad = nombreCiudad;
            this.CodigoPostal = codigoPostal;
        }
    }
}
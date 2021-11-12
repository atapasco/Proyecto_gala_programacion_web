using System;

namespace Entity
{
    public class Cliente
    {
        public string IdCliente {set; get;}
        public string Nombres {set; get;}
        public string Apellidos {set; get;}
        public Cuenta Cuenta {set; get;}
        public char Genero {set; get;}
        public DateTime FechaNacimiento {set; get;}

        public Cliente(string idCliente, string nombres, string apellidos,
                       Cuenta cuenta, char genero, DateTime fechaNacimiento)
        {
            this.IdCliente = idCliente;
            this.Nombres = nombres;
            this.Apellidos = apellidos;
            this.Cuenta = cuenta;
            this.Genero = genero;
            this.FechaNacimiento = fechaNacimiento;
        }
    }
}
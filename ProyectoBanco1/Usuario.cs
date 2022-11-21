using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBanco1
{
    public class Usuario
    {
        public int id { get; set; }
        public int dni { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public int intentosFallidos { get; set; }
        public bool bloqueado { get; set; }
        public bool esAdmin { get; set; }

        public List<UsuarioCaja> userCaja { get; set; } = new List<UsuarioCaja>();
        public ICollection<CajaDeAhorro> cajas { get; set; } = new List<CajaDeAhorro>(); 
        public List<PlazoFijo> pfs { get; } = new List<PlazoFijo>();
        public List<TarjetaDeCredito> tarjetas { get; set; } = new List<TarjetaDeCredito>();
        public List<Pago> pagos { get; set; }  = new List<Pago>();
        

        public Usuario() { }
        public Usuario(int id, int dni, string nombre, string apellido, string mail, string password, int intentosFallidos, bool bloqueado, bool esAdmin)
        {
            this.id = id;
            this.dni = dni;
            this.nombre = nombre;
            this.apellido = apellido;
            this.mail = mail;
            this.password = password;
            this.intentosFallidos = intentosFallidos;
            this.bloqueado = bloqueado;
            this.esAdmin = esAdmin;

        }
        public override string ToString()
        {
            return id + ", " + dni + ", " + nombre + ", " + apellido + ", " + mail + ", " + bloqueado;
        }
    }
}

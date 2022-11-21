using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBanco1
{
    public class Pago
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public Usuario user { get; set; }
        public string nombre { get; set; }
        public double monto { get; set; }
        public bool pagado { get; set; }
        public string metodo { get; set; }

        public Pago() { }
        public Pago( int idUser, string nombre, double monto, bool pagado, string metodo)
        {
           
            this.idUsuario = idUser;
            this.nombre = nombre;
            this.monto = monto;
            this.pagado = pagado;
            this.metodo = metodo;
        }


        public override string ToString()
        {
            return id + ", " + user + ", " + nombre + ", " + monto + ", " + pagado + ", " + metodo;
        }
    }
  

}

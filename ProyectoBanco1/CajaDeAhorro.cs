using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBanco1
{
    public class CajaDeAhorro
    {
        public int id { get; set; }
        public int cbu { get; set; }
        public double saldo { get; set; }

        public List<UsuarioCaja> userCaja { get; set; } = new List<UsuarioCaja>();
        public ICollection<Usuario> titulares { get; set; } = new List<Usuario>();
        public List<Movimiento> movimientos { get; set; } = new List<Movimiento>();

        public CajaDeAhorro(){}

        public CajaDeAhorro(int cbu, double saldo)
        {
            this.cbu = cbu;
            this.saldo = saldo;
        }
        public override string ToString()
        {
            return cbu + " - $" + saldo;
        }
    }
}

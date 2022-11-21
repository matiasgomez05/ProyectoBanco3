using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBanco1
{
    public class UsuarioCaja
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public Usuario user { get; set; }

        public int idCaja { get; set; }
        public CajaDeAhorro caja { get; set; }


        public UsuarioCaja() { }
        public UsuarioCaja(int idU, int idC)
        {
            this.idUsuario = idU;
            this.idCaja = idC;
        }


    }
}

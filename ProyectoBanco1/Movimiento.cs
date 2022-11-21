
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBanco1
{
    public class Movimiento
    {
        public int id { get; set; }
        public int idCaja { get; set; }
        public CajaDeAhorro caja { get; set; }
        public string detalle { get; set; }
        public double monto { get; set; }
        public DateTime fecha { get; set; }

        public Movimiento() { }
        public Movimiento(int id, int idCaja, string detalle, double monto, DateTime fecha)
        {
            this.id = id;
            this.idCaja = idCaja;
            this.detalle = detalle;
            this.monto = monto;
            this.fecha = fecha;
        }
    }
}

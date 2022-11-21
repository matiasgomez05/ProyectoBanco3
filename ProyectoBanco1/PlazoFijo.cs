using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBanco1
{
    public class PlazoFijo
    {
        public int id { get; set; }
        public int idTitular { get; set; }
        public Usuario titular { get; set; }
        public double monto { get; set; }
        public DateTime fechaIni { get; set; }
        public DateTime fechaFin { get; set; }
        public double tasa { get; set; }
        public bool pagado { get; set; }
        public int cbu { get; set; }


        public PlazoFijo() { }
        public PlazoFijo(int id, int idTitular, double monto, DateTime fechaIni, DateTime fechaFin, double tasa, bool pagado, int cbu)
        {
            this.id = id;
            this.idTitular = idTitular;
            this.monto = monto;
            this.fechaIni = fechaIni;
            this.fechaFin = fechaFin;
            this.tasa = tasa;
            this.pagado = pagado;
            this.cbu = cbu;



        }
    }
}
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Runtime.Remoting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Microsoft.EntityFrameworkCore;

namespace ProyectoBanco1
{
    public class Banco

    {
        /*
         * Atributos
         */

        private MyContext contexto;

        private DAL DB;

        public Usuario usuarioActual { get; set; }
        public int nuevoUsuario { get; set; }
        public int nuevaCaja { get; set; }
        public int nuevoPago { get; set; }
        public int nuevoPf { get; set; }
        public int nuevaTarjeta { get; set; }

        

        public List<Usuario> usuarios;
        public List<CajaDeAhorro> cajas;
        public List<PlazoFijo> pfs;
        public List<TarjetaDeCredito> tarjetas;
        public List<Pago> pagos;
        public List<Movimiento> movimientos;
        public List<UsuarioCaja> usuarioCaja;

        /*
         * Constructor e inicializacion de la aplicacion
         */


        public Banco()
        {
            //usuarios = new List<Usuario>();
            //cajas = new List<CajaDeAhorro>();
            //pfs = new List<PlazoFijo>();
            //tarjetas = new List<TarjetaDeCredito>();
            //pagos = new List<Pago>();
            //movimientos = new List<Movimiento>();
            //usuarioCaja = new List<UsuarioCaja>();

            inicializarAtributos();
            
        }

        private void inicializarAtributos()
        {
            try
            {
                contexto = new MyContext();

                contexto.usuarios.Include(u => u.cajas).Include(u => u.pagos).Include(u => u.pfs).Include(u => u.tarjetas).Load();
                contexto.tarjetas.Load();
                contexto.cajas.Include(c => c.titulares).Include(c => c.movimientos).Load();
                contexto.movimientos.Load();
                contexto.pfs.Load();
                contexto.pagos.Load();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void cerrar()
        {
            contexto.Dispose();
        }

        /* 
         * Operaciones de Usuario
         */


        //Busca un usuario existente para iniciar sesion; lo bloquea en caso de 3 intentos fallidos o reinicia sus intentos si lo encuentra.
        //  1: Usuario encontrado
        //  0: Usuario bloqueado
        // -1: Credenciales erroneas
        public int iniciarSesion(int dni, string password)
        {
            int usuarioEncontrado = -1;
            Usuario usuario = contexto.usuarios.Where(u => u.dni.Equals(dni)).FirstOrDefault();
            if (usuario == null) return -1;

            if (usuario.dni.Equals(dni) && usuario.password.Equals(password))
            {
                if (usuario.bloqueado) return 0;

                if (usuario.intentosFallidos >= 3)
                {
                    usuario.bloqueado = true;
                    usuario.intentosFallidos = 3;
                    contexto.usuarios.Update(usuario);
                    contexto.SaveChanges();
                    return 0;
                }

                usuario.intentosFallidos = 0;
                contexto.usuarios.Update(usuario);
                contexto.SaveChanges();

                this.usuarioActual = usuario;
                return 1;
            }
            else if (usuario.dni.Equals(dni) && !usuario.password.Equals(password))
            {
                usuario.intentosFallidos++;
                contexto.usuarios.Update(usuario);
                contexto.SaveChanges();

                return -1;
            }

            return usuarioEncontrado;
        }

        //Cerrar la sesion del usuario actual
        public void cerrarSesion()
        {
            this.usuarioActual = null;
        }



        /* 
         * ABM CAJA DE AHORRO
         */


        //Depositar dinero en la caja de ahorro seleccionada
        public bool depositar(int idCaja, double monto)
        {
            DateTime fecha = DateTime.Now;
            int idNuevoMov = -1;
            foreach (CajaDeAhorro caja in cajas)
            {
                if (caja.id == idCaja)
                {
                    caja.saldo += monto;
                    DB.modificarCaja(idCaja, caja.saldo);

                    idNuevoMov = DB.agregarMovimiento(idCaja, "Deposito de $" + monto, monto, fecha);
                    if (idNuevoMov != -1)
                    {
                        Movimiento movimiento = new Movimiento(idNuevoMov, idCaja, "Deposito de $" + monto, monto, fecha);
                        movimientos.Add(movimiento);
                        caja.movimientos.Add(movimiento);
                    }
                    return true;
                }
            }

            return false;
        }
        //Retirar dinero de la caja de ahorro seleccionada, siempre que tenga saldo suficiente
        public bool retirar(int idCaja, double monto)
        {
            DateTime fecha = DateTime.Now;
            int idNuevoMov = -1;
            foreach (CajaDeAhorro caja in cajas)
            {
                if (caja.id == idCaja)
                {
                    if(monto <= caja.saldo)
                    {
                        caja.saldo -= monto;
                        DB.modificarCaja(idCaja, caja.saldo);

                        idNuevoMov = DB.agregarMovimiento(idCaja, "Extracción de $" + monto, monto, fecha);
                        if (idNuevoMov != -1)
                        {
                            Movimiento movimiento = new Movimiento(idNuevoMov, idCaja, "Extracción de $" + monto, monto, fecha);
                            movimientos.Add(movimiento);
                            caja.movimientos.Add(movimiento);
                        }
                        return true;
                    } 
                    else
                    {
                        return false;
                    }
                } 
            }

            return false;
        }
        //Transferir dinero a la caja de ahorro seleccionada
        public bool transferir(int idCajaOrigen, int cbuCajaDestino, double monto)
        {
            DateTime fecha = DateTime.Now;
            int idNuevoMov = -1;
            foreach (CajaDeAhorro origen in cajas)
            {
                if (origen.id == idCajaOrigen)
                {
                    if (monto <= origen.saldo)
                    {
                        origen.saldo -= monto;
                        DB.modificarCaja(origen.id, origen.saldo);

                        foreach (CajaDeAhorro destino in cajas)
                        {
                            if (destino.cbu == cbuCajaDestino)
                            {
                                destino.saldo += monto;
                                DB.modificarCaja(destino.id, destino.saldo);

                                string detalle = "Transferencia de $" + monto;
                                string detalleOrigen = detalle + " al Destino: CBU " + destino.cbu;
                                string detalleDestino = detalle + "+ del Origen: CBU " + origen.cbu;

                                idNuevoMov = DB.agregarMovimiento(origen.id, detalleOrigen, monto, fecha);
                                if (idNuevoMov != -1)
                                {
                                    Movimiento movimiento = new Movimiento(idNuevoMov, origen.id, detalleOrigen, monto, fecha);
                                    movimientos.Add(movimiento);
                                    origen.movimientos.Add(movimiento);
                                }

                                idNuevoMov = DB.agregarMovimiento(destino.id, detalleDestino, monto, fecha);
                                if (idNuevoMov != -1)
                                {
                                    Movimiento movimiento = new Movimiento(idNuevoMov, destino.id, detalleDestino, monto, fecha);
                                    movimientos.Add(movimiento);
                                    destino.movimientos.Add(movimiento);
                                }
                                return true;
                            }
                        }
                    }
                    else return false;
                }
            }
            return false;
        }

        /*
         * ABM Usuario
         */

        //Agregar un nuevo usuario (Alta)
        public bool altaUsuario(int dni, string nombre, string apellido, string mail, string password, int intentosFallidos, bool bloqueado, bool esAdmin = false)
        {
            //Lo agrego solo si el DNI no está duplicado
            bool existe = false;
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.dni == dni) existe = true;
            }

            if (!existe)
            {
                int idNuevo = DB.agregarUsuario(dni, nombre, apellido, mail, password, intentosFallidos, bloqueado, esAdmin);
                if (idNuevo != -1)
                {
                    usuarios.Add(new Usuario(idNuevo, dni, nombre, apellido, mail, password, intentosFallidos, bloqueado, esAdmin));
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        //Eliminar un usuario existente (Baja)
        public void eliminarUsuario(int dni)
        {
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.dni == dni)
                {
                    usuarios.Remove(usuario);
                }
            }

            /*
            Usuario usuario = usuarios.Find(usuario => usuario.dni.Equals(dni));
            usuarios.Remove(usuario);
            */
        }

        //Modificar un usuario existente (Edición)
        public void modificarUsuario(int dni, string nombre, string apellido, string mail, string password)
        {
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.dni == dni)
                {
                    usuario.nombre = nombre;
                    usuario.apellido = apellido;
                    usuario.mail = mail;
                    usuario.password = password;
                }
            }
        }




        /*
         * ABM Cajas de Ahorro
         */

        //Agregar una nueva caja de ahorro para el usuario en cuestion
        public void altaCajaAhorro(Usuario usuario)
        {
            DateTime DT = DateTime.Now;

            int cbuAleatorio = int.Parse(DT.ToString("ddhhmmss"));
            int idNuevaCaja = DB.agregarCajas(cbuAleatorio, 0);
            if (idNuevaCaja != -1)
            {
                CajaDeAhorro cajaAux = new CajaDeAhorro(idNuevaCaja, cbuAleatorio, 0);
                int idNuevaRelacion = DB.agregarUsuarioCaja(usuario.id, idNuevaCaja);
                if (idNuevaRelacion != -1)
                {
                    cajaAux.titulares.Add(usuario);
                    usuario.cajas.Add(cajaAux);
                    cajas.Add(cajaAux);

                    UsuarioCaja uc1 = new UsuarioCaja(idNuevaRelacion, usuario.id, idNuevaCaja);
                    usuarioCaja.Add(uc1);
                }
            }

        }

        //Eliminar la caja de ahorro del usuario en cuestion solo si su saldo es cero
        public bool bajaCajaAhorro(Usuario usuario, int id)
        {
            foreach (CajaDeAhorro caja in obtenerCajas())
            {
                if (caja.id == id)
                {
                    if (caja.saldo == 0)
                    {
                        if (DB.eliminarCaja(id) == 1) cajas.Remove(caja);
                        if (DB.eliminarUsuarioCaja(id) == 1) usuario.cajas.Remove(caja);
                        
                        return true;
                    } 
                    else return false;
                } 
            }

            return false;
        }

        //Modificar la caja de ahorro del usuario en cuestion 
        public void modificarCajaAhorro(int dni)
        {

        }


        /*
         * ABM Tarjetas de Credito
         */



        public void altaTarjetaCredito(int numero, int cod, double limite, double consumos)
        {
            int idTarjeta;
            idTarjeta = DB.agregarTarjeta(usuarioActual.id, numero, cod, limite, consumos);

            if (idTarjeta != -1)
            {

                TarjetaDeCredito tc = new TarjetaDeCredito(idTarjeta, usuarioActual.id, numero, cod, limite, consumos); 
                tarjetas.Add(tc);
                usuarioActual.tarjetas.Add(tc);

                MessageBox.Show("la tarjeta se agrego con exito.");
                
            }
        }
        //public void modificarTarjetaCredito(int dni, float limite)
        //   {
        //       foreach (TarjetaDeCredito tarjeta in tarjetas)
        //       {
        //           if (tarjeta.titular.dni == dni)
        //           {
        //               tarjeta.limite = limite;
        //           }
        //       }

        //   }

           public void bajaTarjetaCredito(int id)
           {
            if (DB.eliminarTarjeta(id) == 1)
            {

                foreach (var obj in tarjetas.ToList())
                {
                    if (obj.id == id)
                    {
                        if (obj.consumos == 0)
                        {

                            usuarioActual.tarjetas.Remove(obj);
                            tarjetas.Remove(obj);
                            MessageBox.Show("Tarjeta eliminada con exito.");
                        }
                        else
                        {
                            MessageBox.Show("Para poder eliminar la tarjeta, primero debe pagar los consumos");
                        }
                    }
                }

            }

           }

        public void pagarTarjeta(int cbu, int idTarj)
        {
            foreach (var obj2 in tarjetas)
            {             
                 foreach (var obj in cajas)
                 {
                    if (cbu == obj.cbu)
                    {
                        if (idTarj == obj2.id && obj2.consumos > 0 )
                        {
                            if(obj.saldo >= obj2.consumos)
                            {
                                double consumoAnt = obj2.consumos;
                                obj.saldo = obj.saldo - obj2.consumos;
                                obj2.consumos = 0;
                                double saldoFinal = obj.saldo;
                                DB.modificarCaja(obj.id , saldoFinal);
                                DB.modificarTarjeta(idTarj, obj2.consumos);
                                MessageBox.Show("Tarjeta pagada con exito.");

                                int idNuevoMov = DB.agregarMovimiento(obj.id,"Pago de tarjeta: " + obj2.numero, consumoAnt, DateTime.Now);
                                if (idNuevoMov != -1)
                                {
                                    Movimiento m1 = new Movimiento(idNuevoMov, obj.id, "Pago de tarjeta: " + obj2.numero, consumoAnt, DateTime.Now);
                                    movimientos.Add(m1);
                                    obj.movimientos.Add(m1);
                                }
                            }
                            else if(obj.saldo < obj2.consumos)
                            {
                                MessageBox.Show("No dispones de suficiente saldo en la caja de ahorro.");
                            }
                        }else if(idTarj == obj2.id && obj2.consumos <= 0)
                        {
                            MessageBox.Show("La tarjeta no tiene saldo a pagar.");
                        }
                    }
                 }
            }
        }

        //public int generarCodigoTarjeta()
        //{
        //    int min = 100000;
        //    int max = 999999;

        //    Random random = new Random();
        //    return random.Next(min, max + 1);
        //}

        //public int generarCodigoSeguridadTarjeta()
        //{
        //    int min = 100;
        //    int max = 999;

        //    Random random = new Random();
        //    return random.Next(min, max + 1);
        //}

        /*
         * Mostrar Datos
         */



        /*
         * ABM PAGOS-
         */


        //   Agregar un nuevo pago 

        public void altaPago(string nombre, float monto, bool pagado, string metodo)
        {
            nuevoPago++;
            Pago p1 = new Pago(nuevoPago, usuarioActual.id, nombre, monto, pagado, metodo);
            usuarioActual.pagos.Add(p1);
            pagos.Add(p1);


        }

        public void generarPago(int idPago, int cbu, bool checkCaja)
        {
            
            if (checkCaja == true)
            {
                foreach (var obj in obtenerCajas())
                {
                    if (obj.cbu == cbu)
                    {
                        foreach (var obj2 in usuarioActual.pagos)
                        {
                            if (obj2.id == idPago && obj.saldo >= obj2.monto)
                            {

                                obj2.pagado = true;
                                obj.saldo = obj.saldo - obj2.monto;
                                obj2.metodo = "Caja de ahorro";
                                MessageBox.Show("Pago exitoso.");

                                int idNuevoMov =  DB.agregarMovimiento(obj.id, "Pago de : " + obj2.nombre, obj2.monto, DateTime.Now);
                                if (idNuevoMov != -1)
                                {
                                    Movimiento m1 = new Movimiento(idNuevoMov, obj.id, "Pago de : " + obj2.nombre, obj2.monto, DateTime.Now);
                                    movimientos.Add(m1);
                                    obj.movimientos.Add(m1);
                                }
                                else
                                {
                                    MessageBox.Show("error en bd mov");
                                }
                                
                                DB.modificarCaja(obj.id, obj.saldo);

                            }
                            else if (obj2.id == idPago && obj.saldo < obj2.monto)
                            {
                                MessageBox.Show("No tiene suficiente saldo");
                            }
                        }
                    }
                }
            }else if (checkCaja == false )
            {
                foreach (var obj in tarjetas)
                {
                    if (obj.numero == cbu)
                    {
                        foreach (var obj2 in usuarioActual.pagos)
                        {
                            if (obj2.id == idPago )
                            {

                                obj2.pagado = true;
                                obj.consumos = obj.consumos + obj2.monto;
                                obj2.metodo = "Tarjeta de credito";
                                MessageBox.Show("Pago exitoso.");

                                DB.modificarTarjeta(obj.id, obj.consumos);
                            }
                            
                        }
                    }
                }

            }
           
        }

        //Modificar un pago existente 
        public void modificarPago(int id,string nombre, double monto, bool pagado, string metodo)
        {
            foreach (Pago pago in pagos)
            {
                if (pago.id == id)
                {
                    pago.nombre = nombre;
                    pago.monto = monto;
                    pago.pagado = pagado;
                    pago.metodo = metodo;

                }
            }
        }
        //Eliminar un pago existente 
        public void eliminarPago(int id)
        {
            
            foreach (Pago pago in usuarioActual.pagos.ToList())
            {
                if (pago.id == id && pago.pagado == true)
                {
                    usuarioActual.pagos.Remove(pago);
                    pagos.Remove(pago);
                    MessageBox.Show("Se elimino el pago del historial.");
                }
                else if(pago.id == id && pago.pagado == false)
                {

                    MessageBox.Show("No se pudo eliminar el pago.");
                }
            }
        }



        /*
         * ABM PLAZO FIJO
         */


        //Crear plazo fijo

        float montoCaja;
        public void altaPlazoFijo(float monto, int cbu)
        {

            DateTime fechaAnt = new DateTime(2008, 1, 2);

            DateTime fechaIni = DateTime.Now;
            DateTime fechaFin = DateTime.Now.AddDays(30);


            foreach (var obj in usuarioActual.cajas)
            {
                if (cbu == obj.cbu)
                {
                    if (obj.saldo >= monto)
                    {
                        if (monto >= 1000)
                        {
                            int idPlazoFijo;
                            idPlazoFijo = DB.agregarPlazoFijo(usuarioActual.id, monto, fechaIni, fechaFin,70, false, cbu);

                            if (idPlazoFijo != -1)
                            {

                                PlazoFijo pf1 = new PlazoFijo(idPlazoFijo, usuarioActual.id, monto,fechaIni, fechaFin,70, false, cbu);
                                usuarioActual.pfs.Add(pf1);
                                pfs.Add(pf1);
                                obj.saldo = obj.saldo - monto;
                                double saldoIn = obj.saldo;
                                DB.modificarCaja(obj.id, saldoIn);
                                MessageBox.Show("plazo fijo creado con exito");
                            }
                            else
                            {
                                MessageBox.Show("No se pudo generar un ID valido para pf");
                            }
                        }
                        else
                        {
                            MessageBox.Show("El monto minimo para un plazo fijo es de $1000");
                        }
                       
                    }
                    else
                    {
                        MessageBox.Show("No dispones de suficiente saldo en la caja de ahorro");
                    }

                }
                else
                {
                    MessageBox.Show("error");
                }
            }

        }

        // verificar pagos de plazo fijo

        public bool verificarPf(int id, int cbu)
        {
            return false;

            DateTime fechaAnt = new DateTime(2008, 1, 2);
            foreach (var obj in pfs)
            {
                foreach (var obj2 in cajas)
                {
                    if (obj.id == id )
                    {
                        if (obj.fechaFin >= fechaAnt )
                        {
                            obj.pagado = true;

                            
                                if (cbu == obj2.cbu)
                                {

                                    obj2.saldo = obj2.saldo + (obj.monto + (obj.monto * (obj.tasa / 365) * 30)/100);
                                    double saldoFinal = obj2.saldo;
                                    obj.monto = 0;
                                    DB.modificarCaja(obj2.id, saldoFinal);

                                    int idNuevoMov = DB.agregarMovimiento(obj.id, "Pago de plazoFijo: " + obj.id, saldoFinal, DateTime.Now);
                                    if (idNuevoMov != -1)
                                    {
                                        Movimiento m1 = new Movimiento(idNuevoMov, obj.id, "Pago de plazoFijo: " + obj.id, saldoFinal, DateTime.Now);
                                        movimientos.Add(m1);
                                        obj2.movimientos.Add(m1);
                                    }

                                return  true;
                                }
                                else
                                {
                                    MessageBox.Show("error en caja ");
                                return  false;
                            }
                            
                        } else if (obj.fechaFin < fechaAnt) {
                             MessageBox.Show("error en fecha, todavia no se cumplio el plazo");
                            return false;
                        }
                    }
                   
                }
            } 
        }

        // eliminar plazo fijo

        public void eliminarPlazoFijo(int id, int idCaja)
        {
            if(verificarPf(id, idCaja) == true)
            {
                if (DB.eliminarPlazoFijo(id) == 1)
                {
                    foreach (var obj in usuarioActual.pfs.ToList())
                    {
                        if (id == obj.id && obj.pagado == true)
                        {
                            usuarioActual.pfs.Remove(obj);
                            pfs.Remove(obj);
                            MessageBox.Show("plazo fijo eliminado");

                        }
                        else
                        {
                            MessageBox.Show("No se cumplio el plazo, no es posible eliminar el pf.");

                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No se cumplio el plazo,error");
            }

           
        }






        /*
         *   MOSTRAR DATOS.
         */



        //Mostrar todas las tarjetas de credito que posee el Banco (Listar)
        public List<TarjetaDeCredito> obtenerTarjetasDeCredito()
        {
            return tarjetas.ToList();
        }


        //Mostrar todos los plazos fijos que posee el Banco (Listar)
        public List<PlazoFijo> obtenerPlazosFijos()
        {
            return pfs.ToList();
        }


        //Mostrar todos los usuarios que posee el Banco (Listar)
        public List<Usuario> obtenerUsuarios()
        {
            return usuarios.ToList();
        }

        //Mostrar todas las Cajas de Ahorro que posee el Banco (Listar
        public List<CajaDeAhorro> obtenerCajas()
        {
            return usuarioActual.cajas.ToList();
        }

        //Mostrar todas las movimientos que tiene la Caja de Ahorro que pase por parametro (Listar)
        public List<Movimiento> obtenerMovimientos()
        {
            return movimientos.ToList();
        }

        //Mostrar todos los pagos que posee el Banco (Listar)
        public List<Pago> obtenerPagos()
        {
            return pagos.ToList();
        } 
    }
}

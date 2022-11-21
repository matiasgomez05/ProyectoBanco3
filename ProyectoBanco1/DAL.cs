using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Collections;

namespace ProyectoBanco1
{
    public class DAL
    {
        private string connectionString;

        //Constructor
        public DAL()
        {
            //Busco la cadena de conexion desde el archivo de properties del entorno
            connectionString = Properties.Resources.connectionStr;
        }


        // INICIALIZAR USUARIOS.

        public List<Usuario> inicializarUsuarios()
        {
            List<Usuario> dbUsuarios = new List<Usuario>();

            string query = "SELECT * FROM dbo.usuario";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    Usuario nuevoUsuario;
                    while (reader.Read())
                    {
                        nuevoUsuario = new Usuario(
                            reader.GetInt32(0),     //id
                            reader.GetInt32(1),     //dni
                            reader.GetString(2),    //nombre
                            reader.GetString(3),    //apellido
                            reader.GetString(4),    //mail
                            reader.GetString(5),    //password
                            reader.GetInt32(6),     //intentosFallidos
                            reader.GetBoolean(7),   //bloqueado
                            reader.GetBoolean(8));  //esAdmin 


                        dbUsuarios.Add(nuevoUsuario);
                    }
                    reader.Close();
                }
                catch(Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }

            return dbUsuarios;

        }

        // INICIALIZAR PLAZO FIJO.
        //public List<PlazoFijo> inicializarPfs()
        //{
            
        //    List<PlazoFijo> dbPfs = new List<PlazoFijo>();

        //    string query = "SELECT * FROM dbo.plazoFijo";
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand comando = new SqlCommand(query, connection);
        //        try
        //        {
        //            connection.Open();
        //            SqlDataReader reader = comando.ExecuteReader();
        //            PlazoFijo nuevoPf;
        //            while (reader.Read())
        //            {
        //                nuevoPf = new PlazoFijo(
        //                    reader.GetInt32(0),     //id
        //                    reader.GetInt32(1),     //idTitular
        //                    reader.GetDouble(2),    //monto
        //                    reader.GetDateTime(3),    //fechaIni
        //                    reader.GetDateTime(4),    //fechaFin
        //                    reader.GetDouble(5),    //tasa
        //                    reader.GetBoolean(6),  //pagado
        //                    reader.GetInt32(7)    // cbu
        //                    );    

        //                dbPfs.Add(nuevoPf);
        //            }
        //            reader.Close();

        //        }
        //        catch (Exception error)
        //        {
        //            MessageBox.Show(error.Message);
        //            Console.WriteLine(error.Message);
        //        }
        //    }

        //    return dbPfs;

        //}

        // AGREGAR PLAZO FIJO.

        public int agregarPlazoFijo(int titular, double monto, DateTime fechaIni, DateTime fechaFin, double tasa, bool pagado, int cbu)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            int idNuevoPf = -1;
            string queryString = "INSERT INTO dbo.plazoFijo (titular,monto,fechaIni,fechaFin,tasa,pagado,cbu) VALUES (@titular,@monto,@fechaIni,@fechaFin,@tasa, @pagado, @cbu);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@titular", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Float));
                command.Parameters.Add(new SqlParameter("@fechaIni", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@fechaFin", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@tasa", SqlDbType.Float));
                command.Parameters.Add(new SqlParameter("@pagado", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@cbu", SqlDbType.Int));
                command.Parameters["@titular"].Value = titular;
                command.Parameters["@monto"].Value = monto;
                command.Parameters["@fechaIni"].Value = fechaIni;
                command.Parameters["@fechaFin"].Value = fechaFin;
                command.Parameters["@tasa"].Value = tasa;
                command.Parameters["@pagado"].Value = pagado;
                command.Parameters["@cbu"].Value = cbu;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();

                    //*******************************************
                    //Ahora hago esta query para obtener el ID
                    string ConsultaID = "SELECT MAX([ID]) FROM [dbo].[plazoFijo]";
                    command = new SqlCommand(ConsultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoPf = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return idNuevoPf;
            }
        }

        // ELIMINAR PLAZO FIJO.
        public int eliminarPlazoFijo(int id)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            string queryString = "DELETE FROM dbo.plazoFijo WHERE ID = @id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = id;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();
                    return command.ExecuteNonQuery();
                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
                
            }
        }

        //   INICIALIZAR TARJETA DE CREDITO.


        //public List<TarjetaDeCredito> inicializarTarjetas()
        //{

        //    List<TarjetaDeCredito> dbTarjetas = new List<TarjetaDeCredito>();

        //    string query = "SELECT * FROM dbo.tarjeta";
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand comando = new SqlCommand(query, connection);
        //        try
        //        {
        //            connection.Open();
        //            SqlDataReader reader = comando.ExecuteReader();
        //            TarjetaDeCredito nuevaTarjeta;
        //            while (reader.Read())
        //            {
        //                nuevaTarjeta = new TarjetaDeCredito(
        //                    reader.GetInt32(0),     //id
        //                    reader.GetInt32(1),     //idTitular
        //                    reader.GetInt32(2),    //numero
        //                    reader.GetInt32(3),    //codigo
        //                    reader.GetDouble(4),    //limite
        //                    reader.GetDouble(5)   // consumos
        //                    );

        //                dbTarjetas.Add(nuevaTarjeta);



        //            }
        //            reader.Close();

        //        }
        //        catch (Exception error)
        //        {
        //            MessageBox.Show(error.Message);
        //            Console.WriteLine(error.Message);
        //        }
        //    }

        //    return dbTarjetas;

        //}


        // AGREGAR TARJETA DE CREDITO.

        public int agregarTarjeta(int idTitular, int numero, int codigo, double limite, double consumos)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            int idNuevaTarjeta = -1;
            string queryString = "INSERT INTO dbo.tarjeta (titular,numero,codigo,limite,consumos) VALUES (@titular,@numero,@codigo,@limite,@consumos);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@titular", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@numero", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@limite", SqlDbType.Float));
                command.Parameters.Add(new SqlParameter("@consumos", SqlDbType.Float));
                command.Parameters["@titular"].Value = idTitular;
                command.Parameters["@numero"].Value = numero;
                command.Parameters["@codigo"].Value = codigo;
                command.Parameters["@limite"].Value = limite;
                command.Parameters["@consumos"].Value = consumos;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();

                    //*******************************************
                    //Ahora hago esta query para obtener el ID
                    string ConsultaID = "SELECT MAX([ID]) FROM [dbo].[tarjeta]";
                    command = new SqlCommand(ConsultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevaTarjeta = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return idNuevaTarjeta;
            }
        }


        // ELIMINAR TARJETA.
        public int eliminarTarjeta(int id)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            string queryString = "DELETE FROM dbo.tarjeta WHERE ID = @id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = id;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();
                    return command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }

            }
        }
        public int modificarTarjeta(int id, double consumos)
        {
            string queryString = "UPDATE [dbo].[tarjeta] SET consumos=@consumos WHERE id=@id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@consumos", SqlDbType.Float));
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@consumos"].Value = consumos;
                command.Parameters["@id"].Value = id;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }


        public int agregarUsuario(int dni, string nombre, string apellido, string mail, string password, int intentosFallidos, bool bloqueado, bool esAdmin)
            {
                int resultado;
                int idNuevo = -1;

                string query =
                    "INSERT INTO dbo.usuario([dni], [nombre], [apellido], [mail], [password], [intentosFallidos], [bloqueado], [esAdmin]) " +
                         "VALUES (@dni, @nombre, @apellido, @mail, @password, @intentosFallidos, @bloqueado, @esAdmin);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(query, connection);
                comando.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int));
                comando.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NVarChar));
                comando.Parameters.Add(new SqlParameter("@apellido", SqlDbType.NVarChar));
                comando.Parameters.Add(new SqlParameter("@mail", SqlDbType.NVarChar));
                comando.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar));
                comando.Parameters.Add(new SqlParameter("@intentosFallidos", SqlDbType.Int));
                comando.Parameters.Add(new SqlParameter("@bloqueado", SqlDbType.Bit));
                comando.Parameters.Add(new SqlParameter("@esAdmin", SqlDbType.Bit));
                comando.Parameters["@dni"].Value = dni;
                comando.Parameters["@nombre"].Value = nombre;
                comando.Parameters["@apellido"].Value = apellido;
                comando.Parameters["@mail"].Value = mail;
                comando.Parameters["@password"].Value = password;
                comando.Parameters["@intentosFallidos"].Value = intentosFallidos;
                comando.Parameters["@bloqueado"].Value = bloqueado;
                comando.Parameters["@esAdmin"].Value = esAdmin;
                try
                {
                    connection.Open();
                    resultado = comando.ExecuteNonQuery();

                    //Obtengo el ID guardado
                    string consultaID = "SELECT MAX([ID]) FROM [dbo].[usuario]";
                    comando = new SqlCommand(consultaID, connection);
                    SqlDataReader reader = comando.ExecuteReader();

                    reader.Read();
                    idNuevo = reader.GetInt32(0);
                    reader.Close();
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return idNuevo;
            }
        }

        //devuelve la cantidad de elementos modificados en la base (debería ser 1 si anduvo bien)
        public int modificarUsuario(int id, int dni, string nombre, string apellido, string mail, string password, int intentosFallidos, bool bloqueado, bool esAdmin)
        {
            string query = 
                "UPDATE [dbo].[usuario] " +
                "SET nombre = @nombre, apellido = @apellido, mail = @mail, password = @password, intentosFallidos = @intentosFallidos, bloqueado = @bloqueado, esAdmin = @esAdmin " +
                "WHERE id = @id;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(query, connection);
                comando.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                comando.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int));
                comando.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NVarChar));
                comando.Parameters.Add(new SqlParameter("@apellido", SqlDbType.NVarChar));
                comando.Parameters.Add(new SqlParameter("@mail", SqlDbType.NVarChar));
                comando.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar));
                comando.Parameters.Add(new SqlParameter("@intentosFallidos", SqlDbType.Int));
                comando.Parameters.Add(new SqlParameter("@bloqueado", SqlDbType.Bit));
                comando.Parameters.Add(new SqlParameter("@esAdmin", SqlDbType.Bit));
                comando.Parameters["@id"].Value = id;
                comando.Parameters["@dni"].Value = dni;
                comando.Parameters["@nombre"].Value = nombre;
                comando.Parameters["@apellido"].Value = apellido;
                comando.Parameters["@mail"].Value = mail;
                comando.Parameters["@password"].Value = password;
                comando.Parameters["@intentosFallidos"].Value = intentosFallidos;
                comando.Parameters["@bloqueado"].Value = bloqueado;
                comando.Parameters["@esAdmin"].Value = esAdmin;
                try
                {
                    connection.Open();
                    return comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int eliminarUsuario(int id)
        {
            string query = 
                "DELETE FROM [dbo].[usuario] " +
                "WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(query, connection);
                comando.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                comando.Parameters["@id"].Value = id;
                try
                {
                    connection.Open();
                    return comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        /*
         * Cajas de ahorro
         */

        public List<CajaDeAhorro> inicializarCajas()
        {
            List<CajaDeAhorro> dbCajas = new List<CajaDeAhorro>();

            string query =
                "SELECT * FROM dbo.caja";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    CajaDeAhorro nuevaCaja;
                    while (reader.Read())
                    {
                        nuevaCaja = new CajaDeAhorro(
                            reader.GetInt32(0),     //id
                            reader.GetInt32(1),     //cbu
                            reader.GetDouble(2));   //saldo

                        dbCajas.Add(nuevaCaja);
                    }
                    reader.Close();
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }

            return dbCajas;

        }

        public int agregarCajas(int cbu, double saldo)
        {
            int resultado;
            int idNuevo = -1;

            string query =
                "INSERT INTO dbo.caja([cbu], [saldo]) " +
                "VALUES (@cbu, @saldo);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(query, connection);
                comando.Parameters.Add(new SqlParameter("@cbu", SqlDbType.Int));
                comando.Parameters.Add(new SqlParameter("@saldo", SqlDbType.Float));
                comando.Parameters["@cbu"].Value = cbu;
                comando.Parameters["@saldo"].Value = saldo;
                try
                {
                    connection.Open();
                    resultado = comando.ExecuteNonQuery();

                    //Obtengo el ID guardado
                    string consultaID = "SELECT MAX([ID]) FROM [dbo].[caja]";
                    comando = new SqlCommand(consultaID, connection);
                    SqlDataReader reader = comando.ExecuteReader();

                    reader.Read();
                    idNuevo = reader.GetInt32(0);
                    reader.Close();
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return idNuevo;
            }
        }

        public int modificarCaja(int id, double saldo)
        {
            string queryString = "UPDATE [dbo].[caja] SET saldo=@saldo WHERE ID=@id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@saldo", SqlDbType.Float));
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@saldo"].Value = saldo;
                command.Parameters["@id"].Value = id;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int eliminarCaja(int id)
        {
            string query = "DELETE FROM [dbo].[caja] WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(query, connection);
                comando.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                comando.Parameters["@id"].Value = id;
                try
                {
                    connection.Open();
                    return comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public List<UsuarioCaja> inicializarUsuarioCaja()
        {
            List<UsuarioCaja> dbuserCajas = new List<UsuarioCaja>();

            string query =
                "SELECT * FROM dbo.usuarioCaja";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    UsuarioCaja nuevaCaja;
                    while (reader.Read())
                    {
                        nuevaCaja = new UsuarioCaja(
                            reader.GetInt32(0),     //id
                            reader.GetInt32(1),     //idU
                            reader.GetInt32(2));   //idC

                        dbuserCajas.Add(nuevaCaja);
                    }
                    reader.Close();
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }

            return dbuserCajas;

        }

        public int agregarUsuarioCaja(int idUsuario, int idCaja)
        {
            int resultado;
            int idNuevo = -1;

            string query =
                "INSERT INTO dbo.usuarioCaja([idUsuario], [idCaja]) " +
                "VALUES (@idUsuario, @idCaja);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(query, connection);
                comando.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int));
                comando.Parameters.Add(new SqlParameter("@idCaja", SqlDbType.Int));
                comando.Parameters["@idUsuario"].Value = idUsuario;
                comando.Parameters["@idCaja"].Value = idCaja;
                try
                {
                    connection.Open();
                    resultado = comando.ExecuteNonQuery();

                    //Obtengo el ID guardado
                    string consultaID = "SELECT MAX([ID]) FROM [dbo].[usuarioCaja]";
                    comando = new SqlCommand(consultaID, connection);
                    SqlDataReader reader = comando.ExecuteReader();

                    reader.Read();
                    idNuevo = reader.GetInt32(0);
                    reader.Close();
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return idNuevo;
            }
        }

        public int modificarUsuarioCaja(int id, int idUsuario, int idCaja)
        {
            string queryString = "UPDATE [dbo].[usuarioCaja] SET idUsuario=@idUsuario, idCaja=@idCaja WHERE ID=@id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@idCaja", SqlDbType.Int));
                command.Parameters["@id"].Value = id;
                command.Parameters["@idUsuario"].Value = idUsuario;
                command.Parameters["@idCaja"].Value = idCaja;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int eliminarUsuarioCaja(int idCaja)
        {
            string query =
                "DELETE FROM [dbo].[usuarioCaja] " +
                "WHERE idCaja = @idCaja";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(query, connection);
                comando.Parameters.Add(new SqlParameter("@idCaja", SqlDbType.Int));
                comando.Parameters["@idCaja"].Value = idCaja;
                try
                {
                    connection.Open();
                    return comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        //   INICIALIZAR MOVIMIENTOS.


        public List<Movimiento> inicializarMovimientos()
        {

            List<Movimiento> dbMovimiento = new List<Movimiento>();

            string query = "SELECT * FROM dbo.movimiento";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    Movimiento nuevoMov;
                    while (reader.Read())
                    {
                        nuevoMov = new Movimiento(
                            reader.GetInt32(1),     //idCaja
                            reader.GetString(2),    //detalle
                            reader.GetDouble(3),    //monto
                            reader.GetDateTime(4)   //fecha
                            
                            );

                        dbMovimiento.Add(nuevoMov);



                    }
                    reader.Close();

                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    Console.WriteLine(error.Message);
                }
            }

            return dbMovimiento;

        }


        // AGREGAR MOVIMIENTOS.

        public int agregarMovimiento( int idCaja, string detalle, double monto, DateTime fecha)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            int idNuevoMov = -1;
            string queryString = "INSERT INTO dbo.movimiento (idCaja,detalle,monto,fecha) VALUES (@idCaja,@detalle,@monto,@fecha);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@idCaja", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@detalle", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Float));
                command.Parameters.Add(new SqlParameter("@fecha", SqlDbType.DateTime));
                command.Parameters["@idCaja"].Value = idCaja;
                command.Parameters["@detalle"].Value = detalle;
                command.Parameters["@monto"].Value = monto;
                command.Parameters["@fecha"].Value = fecha;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();

                    //*******************************************
                    //Ahora hago esta query para obtener el ID
                    string ConsultaID = "SELECT MAX([ID]) FROM [dbo].[movimiento]";
                    command = new SqlCommand(ConsultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoMov = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return idNuevoMov;
            }
        }


        // ELIMINAR MOVIMIENTOS.
        public int eliminarMovimiento(int id)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            string queryString = "DELETE FROM dbo.movimiento WHERE ID = @id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = id;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();
                    return command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }

            }
        }

    }




}

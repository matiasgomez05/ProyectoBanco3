using ProyectoBanco1;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace ProyectoBanco1
{
    public partial class Form1 : Form
    {
        private Banco banco;

        public delegate void TransfDelegado();
        public TransfDelegado ventanaInicio;

        public Form1(Banco banco)
        {
            InitializeComponent();
            this.banco = banco;

            //Alta usuario de prueba -- Admin --
            //banco.altaUsuario(123, "Admin", "Admin", "admin@admin.com", "123", true);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {   
            if(textBoxDNI.Text != "" && textBoxPass.Text != "")
            {
                int dni = Int32.Parse(textBoxDNI.Text);

                if (textBoxPass.Text == textBoxPass2.Text)
                {
                    bool alta = banco.altaUsuario(dni, textBoxNombre.Text, textBoxApellido.Text, textBoxMail.Text, textBoxPass.Text, 0, false, false);
                    if (alta)
                    {
                        MessageBox.Show("Usuario registrado con exito! Puede ingresar con su Nro de DNI");
                    } 
                    else
                    {
                        MessageBox.Show("El DNI que intenta ingresar ya existe.");
                    }
                }
                else
                {
                    MessageBox.Show("Las contraseñas deben coincidir");
                }
            }
            else
            {
                MessageBox.Show("DNI y Contraseña son obligatorios.");
            }
        }

        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            bool soloNumeros = int.TryParse(textBoxIngresoDni.Text, out _);
            if (!soloNumeros) { MessageBox.Show("Ingrese un DNI valido"); return; }

            int dni = Int32.Parse(textBoxIngresoDni.Text);
            string pass = textBoxIngresoPass.Text;

            if (banco.iniciarSesion(dni, pass)!= -1)
            {
                if (banco.iniciarSesion(dni, pass)== 0)
                {
                    MessageBox.Show("El usuario esta bloqueado. Comuniquese con el administrador.");
                    return;
                }

                this.Close();
                this.ventanaInicio();

                MessageBox.Show("Bienvenido " + banco.usuarioActual.nombre + "!", "Sesion de usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Usuario y/o contraseña incorrectos");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
 }

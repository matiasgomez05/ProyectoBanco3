using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBanco1
{
    public partial class FormInicio : Form
    {
        private Banco banco;

        public delegate void TransfVentanaLogin();
        public TransfVentanaLogin ventanaLogin;
        
        public delegate void TransfVentanaCajasDeAhorro();
        public TransfVentanaCajasDeAhorro ventanaCajasDeAhorro;
        
        public delegate void TransfVentanaPagos();
        public TransfVentanaPagos ventanaPagos;
        
        public delegate void TransfVentanaPlazosFijos();
        public TransfVentanaPlazosFijos ventanaPlazosFijos;
        
        public delegate void TransfVentanaTarjetasDeCredito();
        public TransfVentanaTarjetasDeCredito ventanaTarjetasDeCredito;
        public FormInicio(Banco banco)
        {
            InitializeComponent();
            this.banco = banco;
            label1.Text = banco.usuarioActual.nombre;
            if (banco.usuarioActual.esAdmin) button5.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.ventanaCajasDeAhorro();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.ventanaPagos();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            this.ventanaPlazosFijos();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            this.ventanaTarjetasDeCredito();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //this.Close();
            banco.cerrarSesion();
            this.ventanaLogin();
            MessageBox.Show("Se cerró la sesion correctamente", "Sesion de usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}

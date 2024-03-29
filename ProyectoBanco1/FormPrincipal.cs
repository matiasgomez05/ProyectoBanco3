﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ProyectoBanco1.FormInicio;

namespace ProyectoBanco1
{
    public partial class FormPrincipal : Form
    {
        private Banco banco;

        Form1 ventanaLogin;
        FormInicio ventanaInicio;
        FormCajas ventanaCajasDeAhorro;
        FormPagos ventanaPagos;
        FormPlazosFijos ventanaPlazosFijos;
        FormTarjetasDeCredito ventanaTarjetasDeCredito;
        FormAdmin ventanaAdmin;
        public FormPrincipal()
        {
            InitializeComponent();
            banco = new Banco();

            TransfVentanaLogin();
        }

        private void TransfVentanaLogin()
        {
            ventanaLogin = new Form1(banco);
            ventanaLogin.MdiParent = this;
            ventanaLogin.ventanaInicio += TransfVentanaInicio;
            ventanaLogin.Show();
        }
        
        private void TransfVentanaInicio()
        {
            ventanaInicio = new FormInicio(banco);
            ventanaInicio.MdiParent = this;
            ventanaInicio.ventanaLogin += TransfVentanaLogin;
            ventanaInicio.ventanaCajasDeAhorro += TransfVentanaCajasDeAhorro;
            ventanaInicio.ventanaPagos += TransfVentanaPagos;
            ventanaInicio.ventanaPlazosFijos += TransfVentanaPlazosFijos;
            ventanaInicio.ventanaTarjetasDeCredito += TransfVentanaTarjetasDeCredito;
            ventanaInicio.ventanaAdmin += TransfVentanaAdmin;
            ventanaInicio.Show();
        }

        private void TransfVentanaCajasDeAhorro()
        {
            ventanaCajasDeAhorro = new FormCajas(banco);
            ventanaCajasDeAhorro.MdiParent = this;
            ventanaCajasDeAhorro.ventanaInicio += TransfVentanaInicio;
            ventanaCajasDeAhorro.Show();
        }
        
        private void TransfVentanaPagos()
        {
            ventanaPagos = new FormPagos(banco);
            ventanaPagos.MdiParent = this;
            ventanaPagos.ventanaInicio += TransfVentanaInicio;
            ventanaPagos.Show();
        }private void TransfVentanaPlazosFijos()
        {
            ventanaPlazosFijos = new FormPlazosFijos(banco);
            ventanaPlazosFijos.MdiParent = this;
            ventanaPlazosFijos.ventanaInicio += TransfVentanaInicio;
            ventanaPlazosFijos.Show();
        }private void TransfVentanaTarjetasDeCredito()
        {
            ventanaTarjetasDeCredito = new FormTarjetasDeCredito(banco);
            ventanaTarjetasDeCredito.MdiParent = this;
            ventanaTarjetasDeCredito.ventanaInicio += TransfVentanaInicio;
            ventanaTarjetasDeCredito.Show();
        }
        private void TransfVentanaAdmin()
        {
            ventanaAdmin = new FormAdmin(banco);
            ventanaAdmin.MdiParent = this;
            ventanaAdmin.ventanaInicio += TransfVentanaInicio;
            ventanaAdmin.Show();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void FormPrincipal_Load_1(object sender, EventArgs e)
        {

        }
    }
}

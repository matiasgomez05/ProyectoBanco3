using Azure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace ProyectoBanco1
{
    public partial class FormPagos : Form
    {
        private Banco banco;

        public delegate void TransfDelegado();
        public TransfDelegado ventanaInicio;
        public FormPagos(Banco banco)
        {
            InitializeComponent();
            this.banco = banco;
            refresh();
            checkBox1.Checked = false;
            checkBox2.Checked = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.ventanaInicio();
        }

        //   Metodo para refrescar los datagridview y combobox
        public void refresh()
        {
            comboBox1.Items.Clear();
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            // Si el combobox tiene alguna seleccion, muestra los pagos de el id seleccionado
            // si esta vacio, carga los pagos del usuario admin

            if (comboBox1.Text != "")
            {
                foreach (var obj in banco.obtenerPagos())
                {
                    if (obj.idUsuario == int.Parse(comboBox1.Text))
                    {
                        if (obj.pagado == false)
                        {

                            dataGridView1.Rows.Add(obj.id, obj.nombre, obj.monto);
                        }
                        else if (obj.pagado == true)
                        {

                            dataGridView2.Rows.Add(obj.id, obj.nombre, obj.metodo, obj.monto);
                        }

                    }

                }
            }
            else
            {
                    foreach (var obj in banco.usuarioActual.pagos.ToList())
                    {
                        if (obj.pagado == false)
                        {

                            dataGridView1.Rows.Add(obj.id, obj.nombre, obj.monto);
                        }
                        else if (obj.pagado == true)
                        {
                            dataGridView2.Rows.Add(obj.id, obj.nombre, obj.metodo, obj.monto);
                    }
                    }
            }
            
            // si es Admin, muestra el combobox para seleccionar users y carga el mismo.
            if (banco.usuarioActual.esAdmin)
            {
                label6.Visible = true;
                comboBox1.Visible = true;
                foreach (var obj in banco.obtenerUsuarios())
                {
                    comboBox1.Items.Add(obj.id);
                }

            }

        }

        // BOTON NUEVO PAGO
        private void Nuevo_Click(object sender, EventArgs e)
        {
            banco.altaPago(nombreTxt.Text, int.Parse(montoTxt.Text), false, "");
            nombreTxt.Clear();
            montoTxt.Clear();
            refresh();
        }

        private void Mostrar_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // BOTON ELIMINAR PAGO
        private void Eliminar_Click(object sender, EventArgs e)
        {
            int selectedIndex = dataGridView2.CurrentCell.RowIndex;
            banco.eliminarPago(int.Parse(dataGridView2.Rows[selectedIndex].Cells[0].Value.ToString()));
            refresh();
        }

        // BOTON GENERAR PAGO
        private void Modificar_Click(object sender, EventArgs e)
        {
            int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            banco.generarPago(int.Parse(dataGridView1.Rows[selectedIndex].Cells[0].Value.ToString()), int.Parse(comboBox2.Text), checkBox2.Checked);
            refresh();
        }

        private void pendienteRdn_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void pagoRdn_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void idTxt_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void DatosDgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void metodoTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormPagos_Load(object sender, EventArgs e)
        {

        }

        private void Monto_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // CHECK TARJETA DE CREDITO
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            comboBox1.Items.Clear();

            // SI ES ADMIN MUESTRA TODAS LAS TARJETAS
            if (banco.usuarioActual.esAdmin)
            {
                label7.Visible = true;
                label8.Visible = true;

                foreach (var obj2 in banco.obtenerTarjetasDeCredito())
                {
                    comboBox2.Items.Add(obj2.numero);

                }

            }
            // SI NO ES ADMIN MUESTRA LAS DEL USER ACTUAL
            else
            {
                foreach (var obj in banco.usuarioActual.tarjetas.ToList())
                {
                    comboBox2.Items.Add(obj.numero);
                }    
            }
        }

        //CHECK CAJAS DE AHORRO
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //SI ES ADMIN MUESTRA TODAS LAS CAJAS DE AHORRO
            comboBox1.Items.Clear();
            if (banco.usuarioActual.esAdmin )
            {
                label7.Visible = true;
                label8.Visible = true;

                
                    foreach (var obj in banco.obtenerCajas())
                    {
                     
                        comboBox2.Items.Add(obj.cbu);

                    }
                
            }

            //SI NO ES ADMIN MUESTRA LAS DEL USER ACTUAL
            else
            {
                foreach (var obj in banco.usuarioActual.cajas.ToList())
                {
                    comboBox2.Items.Add(obj.cbu);

                }
            }
            

        }
        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            refresh();

        }
        private void FormPagos_Load_1(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SOLO ADMIN
            //CUANDO SELECCIONA UNA CAJA O TARJETA EN EL COMBOBOX, MUESTRA EL NOMBRE Y APELLIDO DEL TITULAR
            if (comboBox2.Text != "" && checkBox1.Checked)
            {
                label8.Text = "";
                int numeroTarjetaSelect = int.Parse(comboBox2.Text);
                var Query = from Tarjetas in banco.obtenerTarjetasDeCredito()
                            where Tarjetas.numero == numeroTarjetaSelect
                            select Tarjetas.titular;
                foreach (var obj in Query)
                {
                    label8.Text = obj.nombre + obj.apellido;
                }
            }


            if (comboBox2.Text != "" && checkBox2.Checked)
            {
                label8.Text = "";
                int numeroCajaSelect = int.Parse(comboBox2.Text);
                var Query = from Cajas in banco.obtenerCajas()
                            where Cajas.cbu == numeroCajaSelect
                            select Cajas.titulares;
                foreach (var obj in Query)
                {
                    foreach (var obj2 in obj)
                    {
                        
                        label8.Text = obj2.nombre ;
                    }
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}

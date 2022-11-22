using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        public void refresh()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            

            if (banco.usuarioActual.esAdmin)
            {
                comboBox1.Visible = true;
                foreach(var obj in banco.obtenerUsuarios())
                {
                    comboBox1.Items.Add(obj.id);
                }
                
            }
            else
            {
                foreach (var obj in banco.usuarioActual.pagos)
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

        private void Eliminar_Click(object sender, EventArgs e)
        {
            int selectedIndex = dataGridView2.CurrentCell.RowIndex;
            banco.eliminarPago(int.Parse(dataGridView2.Rows[selectedIndex].Cells[0].Value.ToString()));
            refresh();
        }

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
            banco.obtenerPagos();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            refresh();

            foreach (var obj in banco.usuarioActual.tarjetas)
            { 

                    comboBox2.Items.Add(obj.numero);
             
                
            }


        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            refresh();

                foreach (var obj in banco.usuarioActual.cajas)
                {
                        comboBox2.Items.Add(obj.cbu);

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
            refresh();
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
    }
}

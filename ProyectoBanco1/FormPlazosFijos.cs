using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProyectoBanco1
{
    public partial class FormPlazosFijos : Form
    {
        private Banco banco;
        public delegate void TransfDelegado();
        public TransfDelegado ventanaInicio;
        public float tasaActual = 70;


        public FormPlazosFijos(Banco banco)
        {
            InitializeComponent();
            this.banco = banco;
            label5.Text = tasaActual.ToString("0.0000");
            refresh();

        }




        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.ventanaInicio();
        }

        private void button2_Click(object sender, EventArgs e)
        {

             banco.altaPlazoFijo(float.Parse(textBox1.Text), int.Parse(comboBox1.Text));

                refresh();
                
        }


        public void refresh()
        {
            comboBox1.Items.Clear();
            dataGridView1.Rows.Clear();

            foreach (var obj2 in banco.obtenerCajas())
            {
                comboBox1.Items.Add(obj2.cbu);
            }

            foreach (var obj in banco.obtenerPlazosFijos())
            {
                if (obj.idTitular == banco.usuarioActual.id)
                {
                   dataGridView1.Rows.Add(obj.id, obj.cbu, "$" + obj.monto, obj.fechaIni.ToShortDateString(), obj.fechaFin.ToShortDateString());
                           
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            banco.eliminarPlazoFijo(int.Parse(dataGridView1.Rows[selectedIndex].Cells[0].Value.ToString()), int.Parse(dataGridView1.Rows[selectedIndex].Cells[1].Value.ToString()));

            
            refresh();
        }

        private void FormPlazosFijos_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }

}
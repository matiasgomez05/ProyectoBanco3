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
    public partial class FormTarjetasDeCredito : Form
    {
        private Banco banco;

        public delegate void TransfDelegado();
        public TransfDelegado ventanaInicio;
        public FormTarjetasDeCredito(Banco banco)
        {
            InitializeComponent();
            this.banco = banco;
            refresh();

        }
        //   METODO PARA CARGAR EL DATAGRIDVIEW
        public void refresh()
        {

            comboBox1.Items.Clear();
            dataGridView1.Rows.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();


            if (banco.usuarioActual.esAdmin)
            {
                foreach (var obj2 in banco.obtenerCajas())
                {

                    comboBox1.Items.Add(obj2.cbu);

                }



                foreach (var obj in banco.obtenerTarjetasDeCredito())
                {
                    
                        dataGridView1.Rows.Add(obj.id, obj.numero, obj.codigoV, obj.limite, obj.consumos);
                    
                }
            }
            else
            {
                foreach (var obj2 in banco.usuarioActual.cajas)
                {

                    comboBox1.Items.Add(obj2.cbu);

                }



                foreach (var obj in banco.obtenerTarjetasDeCredito())
                {
                    if (banco.usuarioActual.id == obj.idTitular)
                    {
                        dataGridView1.Rows.Add(obj.id, obj.numero, obj.codigoV, obj.limite, obj.consumos);
                    }
                }
            }
            

            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.ventanaInicio();
        }


        // BOTON NUEVA TARJETA DE CREDITO
        private void button2_Click(object sender, EventArgs e)
        {
            banco.altaTarjetaCredito(int.Parse(textBox1.Text), int.Parse(textBox2.Text), Double.Parse(textBox3.Text), 0);
            dataGridView1.Rows.Clear();
            refresh();

        }
            
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                
        }
        
        // BOTON ELIMINAR TARJETA
        private void button3_Click(object sender, EventArgs e)
        {

            int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            banco.bajaTarjetaCredito(int.Parse(dataGridView1.Rows[selectedIndex].Cells[0].Value.ToString()));

            refresh();
        }

        // BOTON PAGAR TARJETA
        private void button4_Click(object sender, EventArgs e)
        {
            
            int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            banco.pagarTarjeta(int.Parse(comboBox1.Text), int.Parse(dataGridView1.Rows[selectedIndex].Cells[0].Value.ToString()));

            refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button4.Visible = true;
            button3.Visible = true;
            comboBox1.Visible = true;
        }

        private void FormTarjetasDeCredito_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

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
                    dataGridView1.Columns[5].Visible = true;
                    dataGridView1.Columns[6].Visible = true;

                foreach (var obj2 in banco.obtenerCajas())
                    {

                    comboBox1.Items.Add(obj2.cbu);

                    }

                    foreach (var obj in banco.obtenerTarjetasDeCredito())
                    {

                     foreach (var obj2 in banco.obtenerUsuarios())
                            {
                                if (obj2.id == obj.idTitular)
                                {

                                    string nombretit = obj2.nombre + " " + obj2.apellido;
                                    dataGridView1.Rows.Add(obj.id, obj.numero, obj.codigoV, obj.limite, obj.consumos, obj.idTitular, nombretit);
                                    
                                }
                            }
                        
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
            if (banco.usuarioActual.esAdmin)
            {
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;

                
                
                foreach(var obj2 in banco.obtenerCajas())
                {
                    if(int.Parse(comboBox1.Text) == obj2.cbu)
                    {
                        foreach (var obj in obj2.titulares)
                        {
                            label4.Text = obj.nombre + " "+ obj.apellido;
                            label5.Text = obj.id.ToString();

                        }
                    }
                    
                 }
            }
        }
    }
}

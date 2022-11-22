using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProyectoBanco1
{
    public partial class FormAdmin : Form
    {
        private Banco banco;

        public delegate void TransfDelegado();
        public TransfDelegado ventanaInicio;
        int selectedIndex { get; set; } 
        int idUser { get; set; }

        public FormAdmin(Banco banco)
        {
            InitializeComponent();
            this.banco = banco;
            Refresh();

            
        }
        public void Refresh()
        {
            dataGridView.Rows.Clear();
            foreach(var obj in banco.obtenerUsuarios())
            {
                dataGridView.Rows.Add(obj.id,obj.nombre,obj.apellido,obj.dni, obj.bloqueado);
                          
            }
            

        }



        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.ventanaInicio();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            

        }

        private void button3_Click(object sender, EventArgs e)
        {

            banco.modificarUsuario(idUser, Int32.Parse(textBox1.Text), textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, checkBox2.Checked);
            Refresh();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int selectedIndex = dataGridView.CurrentCell.RowIndex;
            if (selectedIndex != null)
            {

                int idSelect = Int32.Parse(dataGridView.Rows[selectedIndex].Cells[0].Value.ToString());

                foreach (var obj in banco.obtenerUsuarios())
                {

                    if (obj.id == idSelect)
                    {
                        idUser = idSelect;
                        textBox1.Text = obj.dni.ToString();
                        textBox2.Text = obj.nombre;
                        textBox3.Text = obj.apellido;
                        textBox4.Text = obj.mail;
                        textBox5.Text = obj.password;
                        checkBox2.Checked = obj.bloqueado;

                    }
                }

            }
            
        }
    }
}

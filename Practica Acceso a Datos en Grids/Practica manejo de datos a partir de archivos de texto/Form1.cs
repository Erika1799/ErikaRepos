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

namespace Practica_manejo_de_datos_a_partir_de_archivos_de_texto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_grid.Checked == true)
            {
                dataGridView1.DataSource = "";
                listView1.Columns.Clear();
                listView1.Items.Clear();
                dataGridView1.Visible = true;
                listView1.Visible = false;
            }
            else if (rb_list.Checked == true)
            {
                dataGridView1.DataSource = "";
                listView1.Columns.Clear();
                listView1.Items.Clear();
                dataGridView1.Visible = false;
                listView1.Visible = true;
            }
            else
                return;
        }

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (rb_grid.Checked == true)
                    Acciones.LlenarGrid(txt_Consulta.Text, dataGridView1);
                else if (rb_list.Checked == true)
                    Acciones.LlenarList(txt_Consulta.Text, listView1);
                else
                    return;
            }
            catch (Exception x)
            {

                /*Console.WriteLine("Error: "+ x.Message);*/
                MessageBox.Show("Error: " + x.Message);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lbl_Consulta_Click(object sender, EventArgs e)
        {

        }
    }
}

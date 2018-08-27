using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Programa1
{
    public partial class Form1 : Form
    {
        public string Archivo = "";
        public FileStream arch;
        public StreamReader leer;
        public StreamWriter guardar;

        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                cargaArchivo();
                textBox1.Text = "";
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = false;
            }
                
        }

        public void leeArchivo()
        {
            leer = new StreamReader(Archivo);
            string rline;
            int f = 1;
            dataGridView1.Rows.Clear();
            dataGridView1.AllowUserToAddRows = false;

            do
            {
                rline = leer.ReadLine();
                if (rline != null) //Verifica que haya contenido en el archivo
                {
                    cargaDatos( rline, f);
                    f += 1;
                }
            } while (!(rline == null)); //Termina el while cuando ya no hay nada en el archivo
            leer.Close();    //Cierra el Archivo despues de cargarlo
        }

        public void cargaDatos(string line, int fila)
        {
            string[] arr = line.Split(',');
            dataGridView1.Rows.Add(arr);
        }

        public void cargaArchivo()
        {
            //Archivo = "C:\\Archivos\\" + textBox1.Text + ".txt";
            Archivo = textBox1.Text + ".txt";

            try
            {
                if (File.Exists(Archivo))
                {
                    leeArchivo();
                }
                else
                {
                    MessageBox.Show("No existe el archivo");
                    arch = new FileStream(Archivo, FileMode.Create);
                    arch.Close();
                    MessageBox.Show("Archivo " + Archivo + " creado");
                    leeArchivo();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            textBox3.Enabled = false;
            button2.Enabled = false;
        }

        private void guardaArchivo(string cad)
        {
            guardar = new StreamWriter(Archivo,true);
            guardar.WriteLine(cad);
            guardar.Close();
            leeArchivo();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {

                string cad = textBox2.Text + ',' + Convert.ToString(textBox3.Text);
                guardaArchivo(cad);
                textBox2.Text = "";
                textBox3.Text = "";
                textBox3.Enabled = false;
                button2.Enabled = false;

            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != null)
            {
                textBox3.Enabled = true;
            }
            else
            {
               textBox3.Enabled = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != null)
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            //if ((!Char.IsLetter(e.KeyChar)) || !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                return;
            }
        }
    }
}

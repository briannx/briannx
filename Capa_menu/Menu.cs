using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_clases_ABMC;

namespace Capa_menu
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            Base_de_datos.crear();
            refrescarListas();
            refrescartabla();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OtraTabla ot = new OtraTabla(txtDato.Text);
            if (ot.Guardar() == 0)
            {
                MessageBox.Show("Dato guardado satisfactoriamente!!");
                refrescarListas();
            }
            else
            {
                MessageBox.Show("Dato no guardado!!");
            };
        }
        private void refrescarListas()
        {
            listBox1.DataSource = null;
            listBox1.DataSource = OtraTabla.Buscar("");
            vn_txt.DataSource = null;
            vn_txt.DataSource = OtraTabla.Buscar("");
            comboBox2.DataSource = null;
            comboBox2.DataSource = OtraTabla.Buscar("");
            refrescartabla();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 1)
            {
                OtraTabla ot = (OtraTabla)listBox1.SelectedItem;
                if (ot.Editar(txtDato.Text) == 0)
                {
                    MessageBox.Show("Dato guardado satisfactoriamente!!");
                    refrescarListas();
                }
                else
                {
                    MessageBox.Show("Dato no guardado!!");
                };
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 1)
            {
                OtraTabla ot = (OtraTabla)listBox1.SelectedItem;
                if (ot.Borrar() == 0)
                {
                    MessageBox.Show("Dato borrado satisfactoriamente!!");
                    refrescarListas();
                }
                else { MessageBox.Show("Dato no borrado!!"); };
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            refrescartabla();
        }
        private void refrescartabla()
        {
            string filtro = "";
            int contador = 0;
            if (checkBox1.Checked)
            {
                if (contador == 0)
                {
                    filtro += "where ";
                }
                else
                {
                    filtro += "and ";
                }
                filtro += "Apyn LIKE '%" + na1_txt.Text + "%'";
                contador++;
            }
            if (checkBox2.Checked)
            {
                if (contador == 0)
                {
                    filtro += "where ";
                }
                else
                {
                    filtro += "and ";
                }
                filtro += "(FecNac >= '" + dateTimePicker1.Value + "' and FecNac < '" + dateTimePicker1.Value.AddDays(1) + "')";
                contador++;
            }

            if (checkBox3.Checked)
            {
                if (contador == 0)
                {
                    filtro += "where ";
                }
                else
                {
                    filtro += "and ";
                }
                filtro += "Direccion = '" + textBox9.Text + "' ";
                contador++;
            }

            if (checkBox4.Checked)
            {
                if (contador == 0)
                {
                    filtro += "where ";
                }
                else
                {
                    filtro += "and ";
                }
                filtro += "Localidad = '" + textBox10.Text + "' ";
                contador++;
            }

            if (checkBox5.Checked)
            {
                if (contador == 0)
                {
                    filtro += "where ";
                }
                else
                {
                    filtro += "and ";
                }
                filtro += "CP = '" + textBox11.Text + "' ";
                contador++;
            }

            if (checkBox6.Checked)
            {
                if (contador == 0)
                {
                    filtro += "where ";
                }
                else
                {
                    filtro += "and ";
                }
                filtro += "idOtraTabla = " + ((OtraTabla)comboBox2.SelectedItem).Id + " ";
                contador++;
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Personas.Buscar(filtro);
        }

        private void añadir_txt_Click(object sender, EventArgs e)
        {
            Personas p = new Personas(pr_txt.Text, mr_txt.Text, precio_txt.Text, tl_txt.Text, fc_txt.Value, ((OtraTabla)vn_txt.SelectedItem).Id, ((OtraTabla)vn_txt.SelectedItem).Dato);
            p.Guardar();
            refrescartabla();
        }

        private void editar_txt_Click(object sender, EventArgs e)
        {
            Personas p = (Personas)dataGridView1.SelectedRows[0].DataBoundItem;
            p.Modificar(pr_txt.Text, fc_txt.Value, mr_txt.Text, tl_txt.Text, precio_txt.Text, ((OtraTabla)vn_txt.SelectedItem).Id);
            refrescartabla();
        }

        private void borrar_txt_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                Personas.Borrar(((Personas)dataGridView1.SelectedRows[i].DataBoundItem).Id);
            }
            refrescartabla();
        }
    }
}

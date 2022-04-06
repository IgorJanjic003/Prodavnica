using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace Prodavnica_Projekat
{

    public partial class Form1 : Form
    {
        string cs = @"Data source = DESKTOP-G6UU3BE; Initial catalog = prodza; Integrated security = true";
        string skomanda;
        DataTable tabela, radnici;
        SqlConnection veza;
        List<Komid> korpica = new List<Komid>();
        int cena;
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                if((string)comboBox1.SelectedItem == tabela.Rows[i][5].ToString())
                    comboBox2.Items.Add(tabela.Rows[i][2].ToString());
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                if ((string)comboBox2.SelectedItem == tabela.Rows[i][2].ToString())
                    comboBox1.Items.Add(tabela.Rows[i][5].ToString());
            }
        }
        void rewind()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox1.Text = "Izaberite proizvodjaca";
            comboBox2.Text = "Izaberite proizvod";
            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                if (!comboBox1.Items.Contains(tabela.Rows[i][5].ToString()))
                {
                    comboBox1.Items.Add(tabela.Rows[i][5].ToString());
                }

            }
            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                if (!comboBox2.Items.Contains(tabela.Rows[i][2].ToString()))
                {
                    comboBox2.Items.Add(tabela.Rows[i][2].ToString());
                }

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            rewind();
        }

        private void dodaj_Click(object sender, EventArgs e)
        {
            string aoikjwdoiajwdoi = "";
            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                if(tabela.Rows[i][2].ToString() == (string)comboBox2.SelectedItem)
                {
                    korpica.Add(new Komid(Convert.ToInt32(tabela.Rows[i][0]), Convert.ToInt32(textBox1.Text)));
                    aoikjwdoiajwdoi = comboBox2.SelectedItem + " " + tabela.Rows[i][3].ToString() + "din." +  " " + textBox1.Text + "kom";
                    cena += Convert.ToInt32(tabela.Rows[i][3]) * Convert.ToInt32(textBox1.Text);
                    break;
                }
            }
            label5.Text = cena.ToString();
            listBox1.Items.Add(aoikjwdoiajwdoi);
            rewind();
            textBox1.Text = "";
            
        }

        private void plati_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            MessageBox.Show($"Hvala na ukazanom poverenju.\n Iznos za placanje je {cena}");
            veza = new SqlConnection(cs);
            for (int i = 0; i < korpica.Count; i++)
            {
                SqlCommand komanda = new SqlCommand($"insert into prodaja values({korpica[i].id}, {korpica[i].kom},{rnd.Next(1,3)})",veza);
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
            }
            rewind();
            korpica.Clear();
            listBox1.Items.Clear();
            cena = 0;
            label5.Text = "0";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            korpica.Clear();
            cena = 0;
            label5.Text = "0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            veza = new SqlConnection(cs);
            skomanda = "select * from artikal join proizvodjac on proizvodjac.id = artikal.id_proizvodjaca";
            SqlDataAdapter radnik = new SqlDataAdapter("select * from radnik", veza);
            SqlDataAdapter adapter = new SqlDataAdapter(skomanda, veza);
            tabela = new DataTable();
            radnici = new DataTable();
            adapter.Fill(tabela);
            radnik.Fill(radnici);
            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                if (!comboBox1.Items.Contains(tabela.Rows[i][5].ToString())) {
                    comboBox1.Items.Add(tabela.Rows[i][5].ToString());
                } 

            }
            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                if (!comboBox2.Items.Contains(tabela.Rows[i][2].ToString()))
                {
                    comboBox2.Items.Add(tabela.Rows[i][2].ToString());
                }

            }
        }
    }
    public class Komid
    {
        public int kom;
        public int id;
        public Komid(int koma, int ida)
        {
            this.kom = koma;
            this.id = ida;
        }
    }
}

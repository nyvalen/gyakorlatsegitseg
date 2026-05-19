using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kavezo
{
    public partial class Form1 : Form
    {
        class Adatok
        {
            public string ital;
            public int adag, ar;
        }
        class Megrendel
        {
            public string rendital;
            public int rendadag;
        }
        List <Adatok> lista=new List <Adatok> ();
        List<Megrendel> rendlista=new List <Megrendel> ();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader be = new StreamReader("forras.csv");
            be.ReadLine();
            while (!be.EndOfStream)
            {
                string[] sor = be.ReadLine().Split(';');
                Adatok seged = new Adatok();
                seged.ital = sor[0];
                seged.adag = Convert.ToInt32(sor[1]);
                seged.ar = Convert.ToInt32(sor[2]);
                lista.Add(seged);
            }
            be.Close();

            for (int i = 0; i < lista.Count; i++)
            {
                ListViewItem elem = new ListViewItem(lista[i].ital);
                elem.SubItems.Add(lista[i].adag.ToString());
                elem.SubItems.Add(lista[i].ar.ToString());
                listView1.Items.Add(elem);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader rendel = new StreamReader("megrendeles.txt");
            string rendnev=rendel.ReadLine();
            string rendcim=rendel.ReadLine();
            while (!rendel.EndOfStream)
            {
                string[] seg = rendel.ReadLine().Split('\t');
                Megrendel seged=new Megrendel();
                seged.rendital = seg[0];
                seged.rendadag = Convert.ToInt32(seg[1]);
                rendlista.Add(seged);
            }
            rendel.Close();
            richTextBox1.Text = rendnev+"\n";
            richTextBox1.Text += rendcim + "\n";
            for (int i = 0; i < rendlista.Count; i++)
            {
                richTextBox1.Text+=rendlista[i].rendital+" "+rendlista[i].rendadag.ToString()+"\n";
            }
            for (int i = 0; i < rendlista.Count; i++)
            {
                for (int j = 0; j < lista.Count; j++)
                {
                    if (rendlista[i].rendital == lista[j].ital && rendlista[i].rendadag > lista[j].adag)
                        richTextBox1.Text += "A(z) " + rendlista[i].rendital + " termékből nincs elegendő, " + (rendlista[i].rendadag - lista[j].adag).ToString() + " hiányzik.";


                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int fizetendo = 0;
            for (int i = 0; i < rendlista.Count; i++)
            {
                for (int j = 0; j < lista.Count; j++)
                {
                    if (rendlista[i].rendital == lista[j].ital)
                        fizetendo += lista[j].ar * rendlista[i].rendadag;
                }
            }
            textBox1.Text= fizetendo.ToString();
        }
    }
}

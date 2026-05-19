namespace military_shop
{
    public partial class Form1 : Form
    {
        class Termekek
        {
            public string cikkszam, nev;
            public int ar, keszlet;
        }

        class Rendeles
        {
            public string nev, cim, termek;
            public int cikk, db;
        }

        List<Termekek> list = new List<Termekek>();
        List<Rendeles> rend = new List<Rendeles>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader s = new StreamReader("termekek.csv");
            s.ReadLine();
            while (!s.EndOfStream)
            {
                string[] sor = s.ReadLine().Split(';');
                Termekek seged = new Termekek();
                seged.nev = sor[1];
                seged.cikkszam = sor[2];
                seged.ar = int.Parse(sor[4]);
                seged.keszlet = int.Parse(sor[5]);
                list.Add(seged);

            }
            s.Close();

            for (int i = 0; i < list.Count; i++)
            {
                ListViewItem elem = new ListViewItem(list[i].nev);
                elem.SubItems.Add(list[i].cikkszam.ToString());
                elem.SubItems.Add(list[i].ar.ToString());
                elem.SubItems.Add(list[i].keszlet.ToString());
                listView1.Items.Add(elem);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader f = new StreamReader("megrendeles.txt");
            string nev = f.ReadLine();
            string cim = f.ReadLine();
            while (!f.EndOfStream)
            {
                string[] sor = f.ReadLine().Split(';');
                Rendeles seged = new Rendeles();
                seged.cikk = int.Parse(sor[0]);
                seged.termek = sor[1];
                seged.db = int.Parse(sor[2]);
                rend.Add(seged);
            }
            f.Close();
            richTextBox1.Text = nev + "\n";
            richTextBox1.Text += cim + "\n";
            for (int i = 0; i < rend.Count; i++)
            {
                richTextBox1.Text += rend[i].cikk + ";" + rend[i].termek + ";" + rend[i].db + "\n";
            }
            int fiz = 0;
            for (int i = 0; i < rend.Count; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (rend[i].cikk.ToString() == list[j].cikkszam)
                    {
                        if (rend[i].db > list[j].keszlet)
                        {
                            MessageBox.Show("A készlet nem elegendõ a rendelés végrehajtásához hiányzik " + (rend[i].db - list[j].keszlet).ToString() + "db");
                        }
                        else
                        {
                            if (rend[i].cikk.ToString() == list[j].cikkszam)
                            {
                                fiz += rend[i].db * list[j].ar;
                            }
                        }
                    }

                }
            }

            richTextBox1.Text += "Fizetendõ: " + fiz.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            for (int i = 0; i < rend.Count; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (rend[i].cikk.ToString() == list[j].cikkszam && rend[i].db < list[j].keszlet)
                    {
                        list[j].keszlet = list[j].keszlet - rend[i].db;
                    }
                    ListViewItem elem = new ListViewItem(list[j].nev);
                    elem.SubItems.Add(list[j].cikkszam.ToString());
                    elem.SubItems.Add(list[j].ar.ToString());
                    elem.SubItems.Add(list[j].keszlet.ToString());
                    listView1.Items.Add(elem);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamWriter r = new StreamWriter("hiany.txt");
            for (int i = 0; i < rend.Count; i++)
            {
                for (int j=0;j< list.Count; j++)
                {
                    if (rend[i].cikk.ToString()==list[j].cikkszam)
                    {
                        if (rend[i].db > list[j].keszlet)
                        {
                            int hiany = list[j].keszlet - rend[i].db;
                            r.WriteLine(rend[i].cikk.ToString() + ";" + list[j].nev + ";" + list[j].ar + ";" + hiany);
                        }
                    }
                }

            }
            r.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

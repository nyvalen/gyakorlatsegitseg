using System.Text;
using System.Text.Json.Serialization;

string[] fajl = File.ReadAllLines("veetel.txt");
List<Radiojel> radiojelek = [];

for (int i = 0; i < fajl.Length; i++)
{
    if (i % 2 != 0)
    {
        Radiojel adat = new()
        {
            nap = Convert.ToInt32(fajl[i - 1].Split(' ')[0]),
            vevo = Convert.ToInt32(fajl[i - 1].Split(' ')[1]),
            jel = fajl[i]
        };
        radiojelek.Add(adat);
    }
}

//2. feladat
Console.WriteLine("2. feladat");
Console.WriteLine($"Első rádióamatőr: {radiojelek[0].vevo} \nUtolsó rádióamatőr: {radiojelek[^1].vevo}");

//3. feladat
Console.WriteLine("3. feladat");
string keresett_szo = "farkas";

foreach (var radiojel in radiojelek)
{
    for (int i = 0; i < radiojel.jel.Length - keresett_szo.Length; i++)
    {
        if (keresett_szo == radiojel.jel.Substring(i, keresett_szo.Length))
        {
            Console.WriteLine($"nap: {radiojel.nap} rádióamatőe: {radiojel.vevo}");
            break;
        }
    }
}

//4. feladat
Console.WriteLine("4. feladat");
Dictionary<int, int> nap_vevo = [];
for (int i = 0; i < 11; i++)
{
    int vevok = 0;
    foreach (var radiojel in radiojelek)
    {
        if (radiojel.nap == i + 1)
        {
            vevok++;
        }
    }
    nap_vevo.Add(i + 1, vevok);
}

foreach (var elem in nap_vevo)
{
    Console.WriteLine($"{elem.Key}. nap: {elem.Value} rádióamatőr");
}

//5. feladat
Console.WriteLine("5. feladat");
for (int i = 0; i < 11; i++)
{
    StringBuilder teljes_uzenet = new();
    for (int j = 0; j < 90; j++)
    {
        teljes_uzenet.Append('#');
    }
    foreach (var radiojel in radiojelek)
    {
        if (radiojel.nap == i + 1)
        {
            for (int c = 0; c < 90; c++)
            {
                if (radiojel.jel[c] != '#' && teljes_uzenet[c] == '#')
                {
                    teljes_uzenet.Remove(c, 1);
                    teljes_uzenet.Insert(c, radiojel.jel[c]);
                }
            }
        }
    }
    Console.WriteLine($"{teljes_uzenet}");
}

//7. feladat
Console.WriteLine("7. feladat");
Console.Write("Adja meg a nap sorszámát! ");
int nap_input = Convert.ToInt32(Console.ReadLine());
Console.Write("Adja meg a rádióamatőr sorszámát! ");
int radio_amator_input = Convert.ToInt32(Console.ReadLine());
bool talalt = false;

foreach (var radiojel in radiojelek)
{
    if (radiojel.nap == nap_input && radiojel.vevo == radio_amator_input)
    {
        talalt = true;
        if (radiojel.jel.Split(' ')[0].Contains('/'))
        {
            string kifejlett = radiojel.jel.Split(' ')[0].Split('/')[0];
            string kojok = radiojel.jel.Split(' ')[0].Split('/')[1];
            if (szame(kifejlett) && szame(kojok))
            {
                Console.WriteLine($"A megfigyelt egyedek száma: {Convert.ToInt32(kifejlett) + Convert.ToInt32(kojok)}");
            }
            else
            {
                Console.WriteLine("Nincs információ");
            }
        }
        else
        {
            Console.WriteLine("Nincs információ");
        }
    }
}

if (!talalt)
{
    Console.WriteLine("Nincs ilyen feljegyzés");
}

//6. feladat
static bool szame(string szo)
{
    bool valasz = true;
    for (int i = 0; i < szo.Length; i++)
    {
        if (szo[i] < '0' || szo[i] > '9')
        {
            valasz = false;
        }
    }
    return valasz;
}

struct Radiojel
{
    public int nap;
    public int vevo;
    public string jel;
}
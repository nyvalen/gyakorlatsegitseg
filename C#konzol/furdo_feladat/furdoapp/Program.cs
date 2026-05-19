List<Jegy> jegyek = [];
string[] fajl = File.ReadAllLines("szegedifurdo.csv");
 
bool elsoSor = true;
foreach (var sor in fajl)
{
    if (!elsoSor)
    {
        Jegy adat = new();
        adat.kategoria = sor.Split(';')[0];
        adat.ar = Convert.ToInt32(sor.Split(';')[1]);
        adat.idotartam = sor.Split(';')[2];
        jegyek.Add(adat);
    }
    elsoSor = false;
}

//1. feladat
Console.Clear();
Console.Write($"1. feladat: {jegyek.Count} db jegy adatai vannak a táblázatban");

//2. feladat
int felnottArOsszeg = 0;
int felnottJegyOsszeg = 0;
foreach (var jegy in jegyek)
{
  if (jegy.kategoria.Contains("Felnott"))
  {
    felnottArOsszeg += jegy.ar;
    felnottJegyOsszeg++;
  }
}
Console.Write($"\n2. feladat: A felnőtt belépőjegyek átlagára: {felnottArOsszeg/felnottJegyOsszeg:n2}");

//3. feladat
Console.Write("\n3. feladat: ");
foreach (var jegy in jegyek)
{
  if (jegy.kategoria.Contains("Csaladi") && jegy.kategoria.Split('-')[1] == "2")
  {
    Console.Write($"\n- {jegy.kategoria}, {jegy.ar} Ft, {jegy.idotartam}");
  }
}

//4. feladat
Dictionary<string, int> jegyNapszak = [];
foreach (var jegy in jegyek)
{
  if (!jegyNapszak.ContainsKey(jegy.idotartam))
  {
    jegyNapszak.Add(jegy.idotartam, 0);
  }
}

foreach (var jegy in jegyek)
{
  jegyNapszak[jegy.idotartam]++;
}

Console.Write($"\n4. feladat: Belépőjegy lehetőségek száma napszakok szerint:\n-egész napos: {jegyNapszak["EN"]}\n-3 órás: {jegyNapszak["3"]}\n-2 órás: {jegyNapszak["2"]}");

struct Jegy
{
  public string kategoria;
  public int ar;
  public string idotartam;
}
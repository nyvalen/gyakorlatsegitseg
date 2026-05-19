using System.Text;

string[] file = File.ReadAllLines("kutya.csv", Encoding.GetEncoding("iso-8859-1"));
List<Kutya> kutyak = new List<Kutya>();

bool elsoSor = true;
foreach (var sor in file)
{
  if (!elsoSor)
  {
    Kutya adat = new Kutya();
    adat.sorSzam = Convert.ToInt32(sor.Split(';')[0]);
    adat.nev = sor.Split(';')[1];
    adat.fajta = sor.Split(';')[2];
    adat.ivar = sor.Split(';')[3];
    adat.testSuly = Convert.ToInt32(sor.Split(';')[4]);
    adat.kor = Convert.ToInt32(sor.Split(';')[5]);
    kutyak.Add(adat);
  }
  elsoSor = false;
}

//1. feladat
Console.WriteLine($"1. feladat: {kutyak.Count} kutya található a listában");

//2. feladat
var legidosebbKutya = 2025 - kutyak.Min(k => k.kor);
Console.WriteLine($"2. feladat: A legidősebb kutya {legidosebbKutya} éves");

//3. feladat
Console.WriteLine("3. feladat:");
foreach (var kutya in kutyak)
{
  if (kutya.kor >= 2023)
  {
    Console.WriteLine($"\t- {kutya.nev}: {kutya.kor}");
  }
}

//4. feladat
Console.WriteLine("4. feladat");
Dictionary<string, int> kutyaFajtak = new Dictionary<string, int>();
foreach (var kutya in kutyak)
{
  if (!kutyaFajtak.ContainsKey(kutya.fajta))
  {
    kutyaFajtak.Add(kutya.fajta, 0);
  }
}

foreach (var kutya in kutyak)
{
  kutyaFajtak[kutya.fajta]++;
}

foreach (var kutya in kutyaFajtak)
{
  Console.WriteLine($"\t- {kutya.Key}: {kutya.Value}");
}

struct Kutya
{
  public int sorSzam;
  public string nev;
  public string fajta;
  public string ivar;
  public int testSuly;
  public int kor;
}
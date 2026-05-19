List<Jel> jelek = [];

string[] fajl = File.ReadAllLines("jeladas.txt");

foreach (var sor in fajl)
{
  Jel adat = new();
  adat.rendszam = sor.Split('\t')[0];
  adat.ora = Convert.ToInt32(sor.Split('\t')[1]);
  adat.perc = Convert.ToInt32(sor.Split('\t')[2]);
  adat.sebesseg = Convert.ToInt32(sor.Split('\t')[3]);
  jelek.Add(adat);
}

//1. feladat
Console.Clear();
var utolsoJel = jelek[jelek.Count - 1];
Console.WriteLine($"1. feladat: {utolsoJel.ora} : {utolsoJel.perc} Rendszám: {utolsoJel.rendszam}");

//2. feladat
var elsoJel = jelek[0];
Console.Write($"2. feladat: Az első jármű rendszáma: {elsoJel.rendszam} Időpontok: ");
foreach (var jel in jelek)
{
  if (jel.rendszam == elsoJel.rendszam)
  {
    Console.Write($"{jel.ora}:{jel.perc} ");
  }
}

//3. feladat
Console.WriteLine("\n3. feladat:");
Console.Write("Adja meg a keresett órát: ");
int ora = Convert.ToInt32(Console.ReadLine());
Console.Write("Adja meg a keresett percet: ");
int perc = Convert.ToInt32(Console.ReadLine());
int jelSzamlalo = 0;
foreach (var jel in jelek)
{
  if (jel.ora == ora && jel.perc == perc)
  {
    jelSzamlalo++;
  }
}

Console.WriteLine($"Ebben az időpontban {jelSzamlalo} jeladás történt");

//4. feladat

int legnagyobbSebesseg = jelek.Max(j => j.sebesseg);
Console.Write($"4. feladat:\nA legnagyobb sebesség: {legnagyobbSebesseg} km/h\nAutók rendszámai amik ekkora sebességgel haladtak: ");
foreach (var jel in jelek) {
  if (jel.sebesseg == legnagyobbSebesseg)
  {
    Console.Write($"{jel.rendszam} ");
  }
}

struct Jel
{
  public string rendszam;
  public int ora;
  public int perc;
  public int sebesseg;
}
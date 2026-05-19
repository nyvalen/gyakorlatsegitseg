using System.Text;

List<Mese> mesek = new List<Mese>();

string[] file = File.ReadAllLines("mesefilmek.csv", Encoding.GetEncoding("iso-8859-1"));
bool elsoSor = true;
foreach (string sor in file)
{
  if (!elsoSor)
  {
    var adat = new Mese();
    adat.cim = sor.Split(';')[0];
    adat.hosszMin = Convert.ToInt32(sor.Split(';')[1]);
    adat.bemutatYr = Convert.ToInt32(sor.Split(';')[2]);
    adat.mufaj = sor.Split(';')[3];
    adat.dijak = sor.Split(';')[4];
    mesek.Add(adat);
  }
  elsoSor = false;
}

//3. feladat
Console.WriteLine($"3. feladat:\n{mesek.Count()} mesefilm adatai találhatóak meg a táblázatban");

//4. feladat
static string percToOra(int perc)
{
  int ora = 0;
  while (perc >= 60)
  {
    perc -= 60;
    ora++;
  }
  return $"{ora} : {perc}";
}

//5. feladat
Console.WriteLine("5. feladat:");
string valasz = "";
while (valasz == "")
{
  Console.Write("\nAdja meg egy mese címét: ");
  valasz = Console.ReadLine()!;
}

//6. feladat
Console.WriteLine("6. feladat");
bool talalt = false;
foreach (var mese in mesek)
{
  if (mese.cim == valasz)
  {
    Console.WriteLine(percToOra(mese.hosszMin));
    talalt = true;
  }
}
if (!talalt)
{
  Console.WriteLine("Nincs ilyen mesefilm");
}

//7. feladat
Console.WriteLine("7. feladat");
List<Mese> mesekIntervallum = new List<Mese>();
foreach (var mese in mesek)
{
  if (mese.bemutatYr >= 2010 && mese.bemutatYr <= 2020)
  {
    mesekIntervallum.Add(mese);
  }
}

Mese[] mesekIntervallumSorban = mesekIntervallum.OrderBy(mese => mese.cim).ToArray();
foreach (var mese in mesekIntervallumSorban)
{
  Console.WriteLine(mese.cim);
}

//8. feladat
Console.WriteLine("8. feladat");
Dictionary<string, int> mufajDict = new Dictionary<string, int>();
foreach (var mese in mesek)
{
  if (!mufajDict.Keys.Contains(mese.mufaj))
  {
    mufajDict.Add(mese.mufaj, 0);
  }
}

foreach (var mese in mesek)
{
  mufajDict[mese.mufaj]++;
}

foreach (var mufaj in mufajDict)
{
  Console.WriteLine($"{mufaj.Key}: {mufaj.Value}");
}

//9. feladat
Console.WriteLine("9. feladat");
StreamWriter sw = new StreamWriter("dijazottak.txt");
foreach (var mese in mesek)
{
  if (mese.dijak != "N")
  {
    sw.WriteLine($"{mese.cim};{mese.dijak}");
  }
}
sw.Close();
Console.WriteLine("A fájl elkészült.");

struct Mese
{
  public string cim;
  public int hosszMin;
  public int bemutatYr;
  public string mufaj;
  public string dijak;
}
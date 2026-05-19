using Microsoft.VisualBasic;

List<Ut> utak = [];
string[] fajl = File.ReadAllLines("tavok.txt");

foreach (var sor in fajl)
{
    Ut adat = new()
    {
        nap = Convert.ToInt32(sor.Split(' ')[0]),
        fuvarSzam = Convert.ToInt32(sor.Split(' ')[1]),
        hossz = Convert.ToInt32(sor.Split(' ')[2])
    };
    utak.Add(adat);
}

//2. feladat
Console.WriteLine("2. feladat");
int minNap = utak.Min(x => x.nap);
int minFuvarSzam = utak[0].fuvarSzam;
foreach (var ut in utak)
{
    if (ut.nap == minNap && ut.fuvarSzam < minFuvarSzam)
    {
        minFuvarSzam = ut.fuvarSzam;
    }
}
foreach (var ut in utak)
{
    if (minNap == ut.nap && minFuvarSzam == ut.fuvarSzam)
    {
        Console.WriteLine($"{ut.hossz} km volt az első út a héten");
    }
}

//3. feladat
Console.WriteLine("3. feladat");
int maxNap = utak.Max(x => x.nap);
int maxFuvarSzam = utak[0].fuvarSzam;
foreach (var ut in utak)
{
    if (ut.nap == maxNap && ut.fuvarSzam > maxFuvarSzam)
    {
        maxFuvarSzam = ut.fuvarSzam;
    }
}
foreach (var ut in utak)
{
    if (maxNap == ut.nap && maxFuvarSzam == ut.fuvarSzam)
    {
        Console.WriteLine($"{ut.hossz} km volt az utolsó út a héten");
    }
}

//4. feladat
Console.WriteLine("4. feladat");
Console.WriteLine("Napok ahol nem dolgozott: ");
List<int> munkaNapok = [];
foreach (var ut in utak)
{
    if (!munkaNapok.Contains(ut.nap))
    {
        munkaNapok.Add(ut.nap);
    }
}

for (int i = 0; i < 7; i++)
{
    if (!munkaNapok.Contains(i + 1))
    {
        Console.Write($"{i + 1} ");
    }
}

//5. feladat
Console.WriteLine("\n5. feladat");
Dictionary<int, int> napiFuvarok = [];
foreach (var ut in utak)
{
    napiFuvarok.TryAdd(ut.nap, 0);
}

foreach (var ut in utak)
{
    napiFuvarok[ut.nap]++;
}

Console.WriteLine("A legtöbb fuvar eze(-n/-ken) a napo(-n/-kon) volt: ");
int maxNapiFuvar = napiFuvarok.Max(x => x.Value);
foreach (var nap in napiFuvarok)
{
    if (nap.Value == maxNapiFuvar)
    {
        Console.Write($"{nap.Key}");
    }
}

//6. feladat
Console.WriteLine("\n6. feladat");
Dictionary<int, int> napiTavok = [];
for (int i = 0; i < 7; i++)
{
    napiTavok.Add(i + 1, 0);
}

for (int i = 0; i < 7; i++)
{
    foreach (var ut in utak)
    {
        if (ut.nap == i + 1)
        {
            napiTavok[i + 1] += ut.hossz;
        }
    }
}

foreach (var tavok in napiTavok)
{
    Console.WriteLine($"{tavok.Key}. nap: {tavok.Value} km");
}

//7. feladat
Console.WriteLine("\n6. feladat");

int inputTavolsag = 0;
do
{
    Console.WriteLine("Adjon meg egy távolságot (1 - 30) km-ben: ");
    inputTavolsag = Convert.ToInt32(Console.ReadLine());
} while (inputTavolsag < 1 || inputTavolsag > 30);

Console.WriteLine($"{tavolsagDijazas(inputTavolsag)} Ft");

//8. feladat
Console.WriteLine("8. feladat");
StreamWriter sw = new("dijazas.txt");
foreach (var fuvar in napiFuvarok)
{
    for (int i = 0; i < fuvar.Value; i++)
    {
        foreach (var ut in utak)
        {
            if (ut.nap == fuvar.Key && ut.fuvarSzam == i + 1)
            {
                sw.WriteLine($"{fuvar.Key}. nap {i + 1}. út: {tavolsagDijazas(ut.hossz)} Ft");
            }
        }
    }
}
sw.Close();
Console.WriteLine("A fájl elkészült");

//9. feladat
Console.WriteLine("9. feladat");
int osszeg = 0;
foreach (var ut in utak)
{
    osszeg += tavolsagDijazas(ut.hossz);
}
Console.WriteLine($"A heti munkáért összesen {osszeg} Ft-t kapott");

static int tavolsagDijazas(int tavolsag)
{
    if (tavolsag >= 1 && tavolsag <= 2)
    {
        return 500;
    }
    else if (tavolsag >= 3 && tavolsag <= 5)
    {
        return 700;
    }
    else if (tavolsag >= 6 && tavolsag <= 10)
    {
        return 900;
    }
    else if (tavolsag >= 11 && tavolsag <= 20)
    {
        return 1400;
    }
    else if (tavolsag >= 21 && tavolsag <= 30)
    {
        return 2000;
    }
    else
    {
        return 0;
    }
}

struct Ut
{
    public int nap;
    public int fuvarSzam;
    public int hossz;
}
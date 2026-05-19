string[] fajl = File.ReadAllLines("ut.txt");
List<Utszakasz> utszakaszok = [];

bool elsoSor = true;
int egeszTav = 1;

foreach (var sor in fajl)
{
    if (elsoSor)
    {
        egeszTav = Convert.ToInt32(sor);
        elsoSor = false;
    }
    else
    {
        Utszakasz adat = new()
        {
            tavolsag = Convert.ToInt32(sor.Split(' ')[0]),
            akadaly = sor.Split(' ')[1]
        };
        utszakaszok.Add(adat);
    }
}

//2. feladat
Console.WriteLine("2. feladat");
Console.WriteLine("A települések neve:");
foreach (var szakasz in utszakaszok)
{
    if (szakasz.akadaly.Length >= 4)
    {
        Console.WriteLine($"{szakasz.akadaly}");
    }
}

//3. feladat
Console.WriteLine("3. feladat");
Console.Write("Adja meg a vizsgált szakasz hosszát km-ben! ");
string tavolsagInputKm = Console.ReadLine()!.Replace('.', ',');
float tavolsagInputM = float.Parse(tavolsagInputKm) * 1000;


int minSebessegKorlat = 90;
foreach (var utszakasz in utszakaszok)
{
    if (tavolsagInputM < utszakasz.tavolsag)
    {
        break;
    }

    int sebessegKorlat = 90;
    if (utszakasz.akadaly.Length == 2)
    {
        sebessegKorlat = Convert.ToInt32(utszakasz.akadaly);
    }
    else if (utszakasz.akadaly.Length >= 4)
    {
        sebessegKorlat = 50;
    }

    if (sebessegKorlat < minSebessegKorlat)
    {
        minSebessegKorlat = sebessegKorlat;
    }

}
Console.WriteLine($"Az első {tavolsagInputKm} km-en {minSebessegKorlat} km/h volt a legalacsonyabb megengedett sebesség.");

//4. feladat
Console.WriteLine("4. feladat");
bool varosVege = false;
float varosonBeluliUt = 0;
float startTavolsag = 0;
float endTavolsag = 0;

foreach (var utszakasz in utszakaszok)
{
    if (utszakasz.akadaly.Length >= 4)
    {
        startTavolsag = utszakasz.tavolsag;
    }
    else if (utszakasz.akadaly == "]")
    {
        endTavolsag = utszakasz.tavolsag;
        varosVege = true;
    }

    if (varosVege)
    {
        varosonBeluliUt += endTavolsag - startTavolsag;
        varosVege = false;
    }
}
Console.WriteLine($"Az út {varosonBeluliUt / egeszTav * 100:F2} százaléka vezet településen belül.");

//5. feladat
Console.WriteLine("5. feladat");
Console.Write("\nAdja meg egy település nevét: ");
string inputTelepules = Console.ReadLine()!;
bool telepules = false;
int sebessegTablak = 0;
startTavolsag = 0;
endTavolsag = 0;
foreach (var utszakasz in utszakaszok)
{
    if (utszakasz.akadaly == inputTelepules)
    {
        startTavolsag = utszakasz.tavolsag;
        telepules = true;
    }
    if (telepules)
    {
        if (utszakasz.akadaly == "]")
        {
            endTavolsag = utszakasz.tavolsag;
            telepules = false;
        }
        else if(utszakasz.akadaly.Length == 2)
        {
            sebessegTablak++;
        }
    }
}
Console.WriteLine($"A sebességkorlátozó táblák száma: {sebessegTablak}");
Console.WriteLine($"Az út hossza a településen belül: {endTavolsag - startTavolsag} méter.");

//6. feladat
Console.WriteLine("6. feladat");
string varosTemp = "";
string varosElott = "";
string varosUtan = "";
bool varosFound = false;
foreach(var utszakasz in utszakaszok)
{
    if (varosFound)
    {
        if (utszakasz.akadaly.Length >= 4)
        {
            varosUtan = utszakasz.akadaly;
            break;
        }
    }

    if (utszakasz.akadaly == inputTelepules)
    {
        varosElott = varosTemp;
        varosFound = true;
    }

    if (utszakasz.akadaly.Length >= 4)
    {
        varosTemp = utszakasz.akadaly;
    }
}

int varosElottTav = 0;
int varosUtanTav = 0;
int varosElejeTav = 0;
int varosVegeTav = 0;
bool varosElottben = false;
bool varosban = false;
if (varosElott == "")
{
    Console.WriteLine($"A legközelebbi település: {varosUtan}");
}
else if (varosUtan == "")
{
    Console.WriteLine($"A legközelebbi település: {varosElott}");
}
else
{
    foreach (var utszakasz in utszakaszok)
    {
        if (varosElottben)
        {
            if (utszakasz.akadaly == "]")
            {
                varosElottTav = utszakasz.tavolsag;
                varosElottben = false;
            }
        }

        if (varosban)
        {
            if (utszakasz.akadaly == "]")
            {
                varosVegeTav = utszakasz.tavolsag;
                varosElottben = false;
            }
        }

        if (utszakasz.akadaly == varosElott)
        {
            varosElottben = true;
        }

        if (utszakasz.akadaly == varosUtan)
        {
            varosUtanTav = utszakasz.tavolsag;
        }
        if (utszakasz.akadaly == inputTelepules)
        {
            varosElejeTav = utszakasz.tavolsag;
            varosban = true;
        }
    }

    if (varosElejeTav - varosElottTav > varosUtanTav - varosVegeTav)
    {
        Console.WriteLine($"A legközelebbi település: {varosUtan}");
    }
    else if (varosElejeTav - varosElottTav < varosUtanTav - varosVegeTav)
    {
        Console.WriteLine($"A legközelebbi település: {varosElott}");
    }
    else
    {
        Console.WriteLine("Ugyanolyan távolságra vannak egymástól");
    }
}
struct Utszakasz
{
    public int tavolsag;
    public string akadaly;

}
string[] fajl = File.ReadAllLines("valaszok.txt");
List <Versenyzo> versenyzok = [];

bool elsoSor = true;
string helyesValaszok = "";
foreach (var sor in fajl)
{
    if (elsoSor)
    {
        helyesValaszok = sor;
        elsoSor = false;
    }
    else
    {
        Versenyzo adat = new()
        {
            id = sor.Split(' ')[0],
            valasz = sor.Split(' ')[1]
        };
        versenyzok.Add(adat);
    }
}

//2. feladat
Console.WriteLine($"2. feladat: A vetélkedőn {versenyzok.Count} versenyző indult.");

//3. feladat
Console.Write("\n3. feladat: A versenyző azonosítója: ");
string idInput = Console.ReadLine()!;
string versenyzoValasza = ""; //4. feladathoz
foreach (var versenyzo in versenyzok)
{
    if (versenyzo.id == idInput)
    {
        Console.WriteLine($"{versenyzo.valasz} (a versenyző válasza)");
        versenyzoValasza = versenyzo.valasz; // 4. feladathoz
    }
}

//4. feladat
Console.WriteLine("4. feladat:");
Console.WriteLine($"{helyesValaszok}");
for (int i = 0; i < helyesValaszok.Length; i++)
{
    if (helyesValaszok[i] == versenyzoValasza[i])
    {
        Console.Write('+');
    }
    else
    {
        Console.Write(' ');
    }
}

//5. feladat
Console.Write("\n5. feladat: A feladat sorszáma = ");
int feladatSorszama = Convert.ToInt32(Console.ReadLine()!);
float helyesenValaszolok = 0;

foreach (var versenyzo in versenyzok)
{
    if (versenyzo.valasz[feladatSorszama - 1] == helyesValaszok[feladatSorszama - 1])
    {
        helyesenValaszolok++;
    }
}

Console.WriteLine($"A feladatra {helyesenValaszolok} fő, a versenyzők {(helyesenValaszolok / versenyzok.Count) * 100:F2}%-a adott helyes választ.");

//6. feladat
Console.Write("6. feladat: A versenyzők pontszámának meghatározása");
StreamWriter sw = new("pontok.txt");
Dictionary<string, int> versenyzokPontszamai = []; //7. feladathoz
foreach(var versenyzo in versenyzok)
{
    int versenyzoPontszam = 0;
    for (int i = 0; i < helyesValaszok.Length; i++)
    {
        if (versenyzo.valasz[i] == helyesValaszok[i])
        {
            if (i >= 0 && i <= 4)
            {
                versenyzoPontszam += 3;
            }
            else if (i >= 5 && i <= 9)
            {
                versenyzoPontszam += 4;
            }
            else if (i >= 10 && i <= 12)
            {
                versenyzoPontszam += 5;
            }
            else
            {
                versenyzoPontszam += 6;
            }
        }
    }
    versenyzokPontszamai.Add(versenyzo.id, versenyzoPontszam); //7. feladathoz
    sw.WriteLine($"{versenyzo.id} {versenyzoPontszam}");
}
sw.Close();

//7. feladat
Console.Write("7. feladat: A verseny legjobbjai:\n");
var rendezettPontszamDict = versenyzokPontszamai.OrderByDescending(x => x.Value).ToDictionary();
int helyezes = 1;
int elozoHelyezes = versenyzokPontszamai.Max(x => x.Value);
foreach (var diak in rendezettPontszamDict)
{
    if(elozoHelyezes != diak.Value)
    {
        helyezes++;
    }

    elozoHelyezes = diak.Value;
    Console.WriteLine($"{helyezes}. díj ({diak.Value} pont) : {diak.Key}");

    if (helyezes >= 3)
    {
        break;
    }
}

struct Versenyzo
{
    public string id;
    public string valasz;
}
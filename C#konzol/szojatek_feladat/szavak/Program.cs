//1. feladat
Console.Write("1. feladat Adjon meg egy szót: ");
string szo_input = Console.ReadLine()!;
char[] maganhangzok = ['a', 'e', 'i', 'o', 'u'];
bool talalt = false;
foreach (var c in maganhangzok)
{
    if (szo_input.Contains(c))
    {
        Console.WriteLine("Van benne magánhangzó.");
        talalt = true;
        break;
    }
}
if (!talalt)
{
    Console.WriteLine("Nincs benne magánhangzó.");
}

//2. feladat
var fajl = new StreamReader("szoveg.txt");
string leghosszabb_szo = fajl.ReadLine()!;
while (!fajl.EndOfStream)
{
    string current_szo = fajl.ReadLine()!;
    if (current_szo.Length >= leghosszabb_szo.Length)
    {
        leghosszabb_szo = current_szo;
    }
}
fajl.Close();

Console.WriteLine($"2. feladat: A leghosszabb szó: {leghosszabb_szo} és {leghosszabb_szo.Length} karakterből áll");

//3. feladat
fajl = new StreamReader("szoveg.txt");
float talalt_szavak = 0;
float osszes_szo = 0;

while (!fajl.EndOfStream)
{
    int maganhangzo_count = 0;
    int massalhangzo_count = 0;
    string szo = fajl.ReadLine()!;
    foreach (var c in szo)
    {
        if (maganhangzok.Contains(c))
        {
            maganhangzo_count++;
        }
        else
        {
            massalhangzo_count++;
        }
    }
    if(massalhangzo_count < maganhangzo_count)
    {
        talalt_szavak++;
    }
    osszes_szo++;
}
fajl.Close();
Console.WriteLine($"3. feladat: {talalt_szavak}/{osszes_szo} : {talalt_szavak/osszes_szo*100:F2}%");

//4. feladat
fajl = new StreamReader("szoveg.txt");
List<String> ot_betus_szavak = [];
while (!fajl.EndOfStream)
{
    string szo = fajl.ReadLine()!;
    if(szo.Length == 5)
    {
        ot_betus_szavak.Add(szo);
    }
}
fajl.Close();

Console.Write("4. feladat: Adjon meg egy 3 betűs szórészletet: ");
string szoreszlet = Console.ReadLine()!;

foreach(var szo in ot_betus_szavak)
{
    if(szo.Substring(1, 3) == szoreszlet)
    {
        Console.Write($"{szo} ");
    }
}

//5. feladat
var fajl_ki = new StreamWriter("letra.txt");
var csoportok = ot_betus_szavak
    .GroupBy(szo => szo.Substring(1, 3))
    .Where(g => g.Count() >= 2)
    .ToList();

foreach(var csoport in csoportok)
{
    foreach (var szo in csoport)
        fajl_ki.WriteLine(szo);
    fajl_ki.WriteLine();
}
fajl_ki.Close();
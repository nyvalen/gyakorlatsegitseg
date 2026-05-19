List<Hianyzas> hianyzasok = [];
string[] hianyzasok_fajl = File.ReadAllLines("naplo.txt");
string current_day = "";

foreach (var line in hianyzasok_fajl)
{
  string[] split_line = line.Split(' ');
  Hianyzas adat = new();

  if (split_line[0] == "#")
  {
    current_day = $"{split_line[1]} {split_line[2]}";
  }

  else{
    adat.honap = Convert.ToInt32(current_day.Split(' ')[0]);
    adat.nap = Convert.ToInt32(current_day.Split(' ')[1]);
    adat.vezetek_nev = split_line[0];
    adat.kereszt_nev = split_line[1];
    adat.ora_jelenlet = split_line[2];
    hianyzasok.Add(adat);
  }

}

//2. feladat
Console.Clear();
Console.WriteLine($"2. feladat\nÖsszesen {hianyzasok.Count} hiányzás van rögzítve a naplóban");

//3. feladat
int igazolatlan = 0;
int igazolt = 0;
foreach (var hianyzas in hianyzasok)
{
  foreach (char ora in hianyzas.ora_jelenlet)
  {
    if (ora == 'X') igazolt++;
    else if (ora == 'I') igazolatlan++;
  }
}

Console.WriteLine($"3. feladat:\nIgazolt hiányzások: {igazolt}, igazolatlan hiányzások: {igazolatlan}");

//5. feladat

Console.Write("5. feladat\nAdja meg a keresett hónapot: ");
int honap_input = Convert.ToInt32(Console.ReadLine()!);
Console.Write("Adja meg a keresett napot: ");
int nap_input = Convert.ToInt32(Console.ReadLine()!);

Console.WriteLine($"Azon a napon {hetnapja(honap_input, nap_input)} volt");

//6. feladat

int hianyzas_counter = 0;
Console.Write("Adja meg a keresett nap nevét: ");
string bekert_nap = Console.ReadLine()!;
Console.Write("Adja meg a keresett óra sorszámát: ");
int keresett_ora_szama = Convert.ToInt32(Console.ReadLine()!);


foreach (var hianyzas in hianyzasok)
{
  if (hetnapja(hianyzas.honap, hianyzas.nap) == bekert_nap && (hianyzas.ora_jelenlet[keresett_ora_szama - 1] == 'X' || hianyzas.ora_jelenlet[keresett_ora_szama - 1] == 'I')) hianyzas_counter++;
}

Console.WriteLine($"Ekkor összesen {hianyzas_counter} hiányzás történt");

//7. feladat
Dictionary<string, int> hianyzasok_per_ember = [];

foreach (var hianyzas in hianyzasok)
{
  if (!hianyzasok_per_ember.ContainsKey($"{hianyzas.vezetek_nev} {hianyzas.kereszt_nev}"))
  {
    hianyzasok_per_ember.Add($"{hianyzas.vezetek_nev} {hianyzas.kereszt_nev}", 0);
  }
}

foreach (var hianyzas in hianyzasok)
{
  foreach (var ora in hianyzas.ora_jelenlet)
  {
    if (ora == 'X' || ora == 'I')
    {
      hianyzasok_per_ember[$"{hianyzas.vezetek_nev} {hianyzas.kereszt_nev}"]++;
    }
  }
}

int max_hianyzas = hianyzasok_per_ember.Max(p => p.Value);

Console.Write("7. feladat\nA legtöbbet hiányzó tanulók:");
foreach (var ember in hianyzasok_per_ember)
{
  if (ember.Value == max_hianyzas) Console.Write($" {ember.Key}");
}

//4. feladat

static string hetnapja(int honap, int nap)
{
  string[] napnev = ["vasarnap", "hetfo", "kedd", "szerda", "csutortok", "pentek", "szombat"];
  int[] napszam = [0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 335];

  int napsorszam = (napszam[honap - 1] + nap) % 7;
  return napnev[napsorszam];
}

struct Hianyzas
{
  public int honap;
  public int nap;
  public string vezetek_nev;
  public string kereszt_nev;
  public string ora_jelenlet;
}
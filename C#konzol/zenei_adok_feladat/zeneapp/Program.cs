Console.Clear();
string[] text = File.ReadAllLines("musor.txt");
List<Song> songs = [];
Song temp;
string[] data;
for (int i = 1; i < text.Length; i++)
{
  data = text[i].Split(' ', 4);
  temp.chan = Convert.ToInt32(data[0]);
  temp.min = Convert.ToInt32(data[1]);
  temp.sec = Convert.ToInt32(data[2]);
  temp.songId = data[3];
  songs.Add(temp);
}

Console.WriteLine("2. feladat:\n");
int chan1Count = 0, chan2Count = 0, chan3Count = 0;
foreach (var song in songs)
{
  if (song.chan == 1) chan1Count++;
  else if (song.chan == 2) chan2Count++;
  else if (song.chan == 3) chan3Count++;
}
Console.WriteLine($"Az első csatornán {chan1Count} zeneszám volt hallható\nA második csatornán {chan2Count} zeneszám volt hallható\nA harmadik csatornán {chan3Count} zeneszám volt hallható");

Console.WriteLine("\n3. feladat\n");
string firstECId = "";
string lastECid = "";
int eCFLSecCounter = 0;
bool countSec = false;
foreach (var song in songs)
{
  if (song.chan == 1)
  {
    if (song.songId.Split(':')[0] == "Eric Clapton")
    {
      firstECId = song.songId;
      break;
    }
  }
}
foreach (var song in songs)
{
  if (song.chan == 1)
  {
    if (song.songId.Split(':')[0] == "Eric Clapton")
    {
      lastECid = song.songId;
    }
  }
}
foreach (var song in songs)
{
  if (song.chan == 1 && song.songId == firstECId)
  {
    // Vegye ki a kommentet ha az első számot is bele akarja számolni az időbe
    // eCFLSecCounter += (song.min * 60) + song.sec;
    countSec = true;
  }
  else if (song.chan == 1 && song.songId == lastECid)
  {
    countSec = false;
    break;
  }
  else if (song.chan == 1 && countSec)
  {
    eCFLSecCounter += (song.min * 60) + song.sec;
  }
}

Console.WriteLine($"Az első és utolsó Eric Clapton szám között az első csatornán {timeCalc(eCFLSecCounter)} telt el.");

Console.WriteLine("\n4. feladat\n");

string chan1SongId = "";
string chan2SongId = "";
string chan3SongId = "";

foreach (var song in songs)
{
  if (song.chan == 1)
  {
    chan1SongId = song.songId;
  }
  else if (song.chan == 2)
  {
    chan2SongId = song.songId;
  }
  else if (song.chan == 3)
  {
    chan3SongId = song.songId;
  }
  if (song.songId == "Omega:Legenda")
  {
    Console.WriteLine($"Omega Legenda című száma a {song.chan}. adón szólt");
    if (song.chan != 1) Console.WriteLine($"Ez alatt ez szólt az 1. csatornán: {chan1SongId}");
    if (song.chan != 2) Console.WriteLine($"Ez alatt ez szólt az 2. csatornán: {chan2SongId}");
    if (song.chan != 3) Console.WriteLine($"Ez alatt ez szólt az 3. csatornán: {chan3SongId}");
    break;
  }
}

Console.WriteLine("\n5. feladat\n\nAdd meg a keresendő szöveget!");

string gibber = Console.ReadLine()!;
string gibberTemp = gibber;
StreamWriter sw = new StreamWriter("keres.txt");
sw.WriteLine(gibber);
foreach (var song in songs)
{
  gibberTemp = gibber;
  foreach (char c in song.songId)
  {
    if (c == gibberTemp[0])
    {
      gibberTemp = gibberTemp[1..];
    }
    if (gibberTemp.Length == 0)
    {
      Console.WriteLine($"Egy lehetséges szám: {song.songId}");
      sw.WriteLine(song.songId);
      break;
    }
  }
}
sw.Close();

Console.WriteLine("\n6. feladat\n");

int newChan1SecCounter = 0;
int tempNewChan1SecCounter = 0;
foreach (var song in songs)
{
  if (song.chan == 1)
  {
    if (tempNewChan1SecCounter - 3600 >= 0)
    {
      newChan1SecCounter += 180;
      tempNewChan1SecCounter -= 3420;
    }
    else
    {
      newChan1SecCounter += 60 + (60 * song.min) + song.sec;
      tempNewChan1SecCounter += 60 + (60 * song.min) + song.sec;
    }
  }
}

Console.WriteLine($"Az új műsorszerkezet szerint eltelt idő: {timeCalc(newChan1SecCounter)}");

static string timeCalc(int sec)
{
  int min = 0;
  int h = 0;
  while (sec > 60 || min > 60)
  {
    if (sec > 60)
    {
      sec -= 60;
      min++;
    }
    else
    {
      min -= 60;
      h++;
    }
  }
  return $"{h}:{min}:{sec}";
}

struct Song
{
  public int chan;
  public int min;
  public int sec;
  public string songId;
}
namespace NyiratiBalint_WebAPI.Dtos.Szerzodes
{
    public class SzerzodesCreateDto
    {
        public string SzerzodesSzam { get; set; }
        public bool IgazgatoJovahagyta { get; set; }
        public string SzerzodesTargya { get; set; }
        public int PartnerId { get; set; }
    }
}

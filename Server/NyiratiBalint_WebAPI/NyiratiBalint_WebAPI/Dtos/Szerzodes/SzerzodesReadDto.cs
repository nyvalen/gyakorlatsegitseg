namespace NyiratiBalint_WebAPI.Dtos.Szerzodes
{
    public class SzerzodesReadDto
    {
        public int Id { get; set; }
        public string SzerzodesSzam { get; set; }
        public bool IgazgatoJovahagyta { get; set; }
        public string SzerzodesTargya { get; set; }
        public int PartnerId { get; set; }
        public string PartnerNev { get; set; } = string.Empty;
    }
}

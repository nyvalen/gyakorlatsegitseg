using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NyiratiBalint_WebAPI.Models
{
    public class Szerzodes
    {
        [Key]
        public int Id { get; set; }
        public string SzerzodesSzam { get; set; }
        public bool IgazgatoJovahagyta { get; set; }
        public string SzerzodesTargya { get; set; }
        public int PartnerId { get; set; }
        [ForeignKey("PartnerId")]
        public Partner Partner { get; set; }
    }
}

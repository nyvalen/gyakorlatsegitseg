using System.ComponentModel.DataAnnotations;

namespace NyiratiBalint_WebAPI.Models
{
    public class Partner
    {
        [Key]
        public int Id { get; set; }
        public string PartnerNev { get; set; }
        public string Email { get; set; } 
        public List<Szerzodes> Szerzodes { get; set; }
    }
}

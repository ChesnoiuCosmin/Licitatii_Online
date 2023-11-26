using System.ComponentModel.DataAnnotations;

namespace LicitatiiOnline.Models
{
    public class IstoricLicitatiiModel
    {
        public Guid ID_Istoric {  get; set; }
        public Guid? ID_Licitatie { get; set; }
        public decimal? Pret_Vandut { get; set; }
        public string? Utilizator_Castigator { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Data_Incheiere { get; set; }
    }
}

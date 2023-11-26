using System.ComponentModel.DataAnnotations;

namespace LicitatiiOnline.Models
{
    public class OferteModel
    {
        public Guid ID_Oferta { get; set; }
        public Guid? ID_Licitatie { get; set; }
        public string? Nume_Ofertant { get; set; }
        public decimal? Suma_Ofertata { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Data_Oferire { get; set; }
    }
}

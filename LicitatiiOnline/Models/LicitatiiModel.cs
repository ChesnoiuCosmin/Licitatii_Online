using System.ComponentModel.DataAnnotations;

namespace LicitatiiOnline.Models
{
    public class LicitatiiModel
    {
        public Guid ID_Licitatie { get; set; }
        public Guid? ID_Produs { get; set; }
        public string? Nume_Cumparator { get; set; }
        public decimal? Pret_Actual { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Data_Licitatie { get; set; }
        public string Stare { get; set; }
        public decimal? Interval_Preturi {  get; set; }

    }
}

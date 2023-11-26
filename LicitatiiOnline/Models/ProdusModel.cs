using LicitatiiOnline.Models.DBObjects;
using System.ComponentModel.DataAnnotations;

namespace LicitatiiOnline.Models
{
    public class ProdusModel
    {
        public Guid ID_Produs { get; set; } 
        public string? Nume_Produs { get; set; }
        public string? Descriere { get; set; }
        public string? Categorie { get; set; }
        
        public decimal? Pret_Pornire { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Data_Incheiere_Licitatie { get; set; }

        public string? Utilizator_Ofertant_Curent { get; set; }
        
        public string? Stare {  get; set; }

        public string? CaleImagine { get; set; }

        [Display(Name = "Imagine")]
        public IFormFile? IncarcareImagine { get; set; }

        public string TimpRamas
        {
            get
            {
                if (Data_Incheiere_Licitatie.HasValue && Data_Incheiere_Licitatie > DateTime.Now)
                {
                    TimeSpan remainingTime = Data_Incheiere_Licitatie.Value - DateTime.Now;
                    return $"{remainingTime.Days} zile {remainingTime.Hours} ore {remainingTime.Minutes} minute";
                }
                return "Licitatia s-a incheiat";
            }
        }
        public decimal? Pret_Actual_Licitatie { get; set; }

        public string? CastigatorLicitatie { get; set; }

    }
}

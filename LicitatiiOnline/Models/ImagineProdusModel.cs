using LicitatiiOnline.Models.DBObjects;
using System.ComponentModel.DataAnnotations;

namespace LicitatiiOnline.Models
{
    public class ImagineProdusModel
    {
        public Guid IdImagine { get; set; }
        public Guid IdProdus { get; set; }

        public string? CaleImagine { get; set; }

        public virtual Produs IdProdusNavigation { get; set; } = null!;

    }
}
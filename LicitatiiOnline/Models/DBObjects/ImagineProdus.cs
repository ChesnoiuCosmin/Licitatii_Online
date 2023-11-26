using System;
using System.Collections.Generic;

namespace LicitatiiOnline.Models.DBObjects
{
    public partial class ImagineProdus
    {
        public Guid IdImagine { get; set; }
        public Guid IdProdus { get; set; }
        public string? CaleImagine { get; set; }

        public virtual Produs IdProdusNavigation { get; set; } = null!;
    }
}

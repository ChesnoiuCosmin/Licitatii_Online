using System;
using System.Collections.Generic;

namespace LicitatiiOnline.Models.DBObjects
{
    public partial class Licitatii
    {
        public Licitatii()
        {
            IstoricLicitatiis = new HashSet<IstoricLicitatii>();
            Ofertes = new HashSet<Oferte>();
        }

        public Guid IdLicitatie { get; set; }
        public Guid? IdProdus { get; set; }
        public string? Nume_Cumparator { get; set; }
        public decimal? PretActual { get; set; }
        public DateTime? DataLicitatie { get; set; }
        public string? Stare { get; set; }

        public virtual Produs? IdProdusNavigation { get; set; }
        public virtual ICollection<IstoricLicitatii> IstoricLicitatiis { get; set; }
        public virtual ICollection<Oferte> Ofertes { get; set; }
    }
}

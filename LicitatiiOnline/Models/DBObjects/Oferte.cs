using System;
using System.Collections.Generic;

namespace LicitatiiOnline.Models.DBObjects
{
    public partial class Oferte
    {
        public Guid IdOferta { get; set; }
        public Guid? IdLicitatie { get; set; }
        public string? Nume_Ofertant { get; set; }
        public decimal? SumaOfertata { get; set; }
        public DateTime? DataOferire { get; set; }
        public virtual Licitatii? IdLicitatieNavigation { get; set; }
    }
}

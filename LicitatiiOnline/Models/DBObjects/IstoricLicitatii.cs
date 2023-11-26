using System;
using System.Collections.Generic;

namespace LicitatiiOnline.Models.DBObjects
{
    public partial class IstoricLicitatii
    {
        public Guid IdIstoric { get; set; }
        public Guid? IdLicitatie { get; set; }
        public decimal? PretVandut { get; set; }
        public string? UtilizatorCastigator { get; set; }
        public DateTime? DataIncheiere { get; set; }

        public virtual Licitatii? IdLicitatieNavigation { get; set; }
    }
}

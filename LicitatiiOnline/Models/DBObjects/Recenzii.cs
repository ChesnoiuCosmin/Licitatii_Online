using System;
using System.Collections.Generic;

namespace LicitatiiOnline.Models.DBObjects
{
    public partial class Recenzii
    {
        public Guid IdRecenzie { get; set; }
        public Guid? IdUtilizator { get; set; }
        public string? NumeUtilizator { get; set; }
        public string? TextRecenzie { get; set; }
        public int? Nota { get; set; }
    }
}

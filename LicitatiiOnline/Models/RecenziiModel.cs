namespace LicitatiiOnline.Models
{
    public class RecenziiModel
    {
        public Guid ID_Recenzie { get; set; }
        public Guid? ID_Utilizator { get; set; }
        public string Nume_Utilizator { get; set; }
        public string Text_Recenzie { get; set; }
        public int? Nota { get; set; }
    }
}

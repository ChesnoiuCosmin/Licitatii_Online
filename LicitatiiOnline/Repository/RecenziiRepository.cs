using LicitatiiOnline.Data;
using LicitatiiOnline.Models.DBObjects;
using LicitatiiOnline.Models;


namespace LicitatiiOnline.Repository
{
    public class RecenziiRepository
    {
        private ApplicationDbContext dbContext;

        public RecenziiRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }

        public RecenziiRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<RecenziiModel> GetAllRecenzii()
        {
            List<RecenziiModel> recenziiList = new List<RecenziiModel>();

            foreach (Recenzii dbRecenzii in dbContext.Recenzii)
            {
                recenziiList.Add(MapDbObjectToModel(dbRecenzii));
            }

            return recenziiList;
        }

        public RecenziiModel GetRecenziiId(Guid ID)
        {
            return MapDbObjectToModel(dbContext.Recenzii.FirstOrDefault(x => x.IdRecenzie == ID));
        }

        public List<RecenziiModel> GetRecenziiByIdUtilizator(Guid ID)
        {
            List<RecenziiModel> recenziiList = new List<RecenziiModel>();

            foreach (Recenzii dbRecenzii in dbContext.Recenzii.Where(x => x.IdUtilizator == ID))
            {
                recenziiList.Add(MapDbObjectToModel(dbRecenzii));
            }

            return recenziiList;
        }

        public void InsertRecenzii(RecenziiModel recenziiModel)
        {
            recenziiModel.ID_Recenzie = Guid.NewGuid();

            dbContext.Recenzii.Add(MapModelToDbObject(recenziiModel));
            dbContext.SaveChanges();
        }

        public void UpdateRecenzii(RecenziiModel recenziiModel)
        {
            Recenzii existingRecenzii = dbContext.Recenzii.FirstOrDefault(x => x.IdRecenzie == recenziiModel.ID_Recenzie);

            if (existingRecenzii != null)
            {
                existingRecenzii.IdRecenzie = recenziiModel.ID_Recenzie;
                existingRecenzii.IdUtilizator = recenziiModel.ID_Utilizator;
                existingRecenzii.NumeUtilizator = recenziiModel.Nume_Utilizator;
                existingRecenzii.TextRecenzie = recenziiModel.Text_Recenzie;
                existingRecenzii.Nota = recenziiModel.Nota;
                dbContext.SaveChanges();
            }
        }

        public void DeleteRecenzii(RecenziiModel recenziiModel)
        {
            Recenzii existingRecenzii = dbContext.Recenzii.FirstOrDefault(x => x.IdRecenzie == recenziiModel.ID_Recenzie);

            if (existingRecenzii != null)
            {
                dbContext.Recenzii.Remove(existingRecenzii);
                dbContext.SaveChanges();
            }
        }

        private RecenziiModel MapDbObjectToModel(Recenzii dbRecenzii)
        {
            RecenziiModel recenziiModel = new RecenziiModel();

            if (dbRecenzii != null)
            {
                recenziiModel.ID_Recenzie = dbRecenzii.IdRecenzie;
                recenziiModel.ID_Utilizator = dbRecenzii.IdUtilizator;
                recenziiModel.Nume_Utilizator = dbRecenzii.NumeUtilizator;
                recenziiModel.Text_Recenzie = dbRecenzii.TextRecenzie;
                recenziiModel.Nota = dbRecenzii.Nota;

            }

            return recenziiModel;
        }

        private Recenzii MapModelToDbObject(RecenziiModel recenziiModel)
        {
            Recenzii recenzii = new Recenzii();

            if (recenziiModel != null)
            {
                recenzii.IdRecenzie = recenziiModel.ID_Recenzie;
                recenzii.IdUtilizator = recenziiModel.ID_Utilizator;
                recenzii.NumeUtilizator = recenziiModel.Nume_Utilizator;
                recenzii.TextRecenzie = recenziiModel.Text_Recenzie;
                recenzii.Nota = recenziiModel.Nota;

            }

            return recenzii;
        }
    }
}

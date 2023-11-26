using LicitatiiOnline.Data;
using LicitatiiOnline.Models;
using LicitatiiOnline.Models.DBObjects;


namespace LicitatiiOnline.Repository
{
    public class LicitatiiRepository
    {
        private ApplicationDbContext dbContext;

        public LicitatiiRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }

        public LicitatiiRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<LicitatiiModel> GetAllLicitatii()
        {
            List<LicitatiiModel> licitatiiList = new List<LicitatiiModel>();

            foreach (Licitatii dbLicitatii in dbContext.Licitatii)
            {
                licitatiiList.Add(MapDbObjectToModel(dbLicitatii));
            }

            return licitatiiList;
        }

        public LicitatiiModel GetLicitatiiById(Guid ID)
        {
            return MapDbObjectToModel(dbContext.Licitatii.FirstOrDefault(x => x.IdLicitatie == ID));
        }

        public List<LicitatiiModel> GetLicitatiiByAdresaEmail(DateTime Data)
        {
            List<LicitatiiModel> licitatiiList = new List<LicitatiiModel>();

            foreach (Licitatii dbLicitatii in dbContext.Licitatii.Where(x => x.DataLicitatie == Data))
            {
                licitatiiList.Add(MapDbObjectToModel(dbLicitatii));
            }

            return licitatiiList;
        }

        public void InsertLicitatie(LicitatiiModel licitatiiModel)
        {
            licitatiiModel.ID_Licitatie = Guid.NewGuid();

            dbContext.Licitatii.Add(MapModelToDbObject(licitatiiModel));
            dbContext.SaveChanges();
        }

        public void UpdateUtilizator(LicitatiiModel licitatiiModel)
        {
            Licitatii existingLicitatie = dbContext.Licitatii.FirstOrDefault(x => x.IdLicitatie == licitatiiModel.ID_Licitatie);

            if (existingLicitatie != null)
            {
                existingLicitatie.IdLicitatie = licitatiiModel.ID_Licitatie;
                existingLicitatie.IdProdus = licitatiiModel.ID_Produs;
                existingLicitatie.Nume_Cumparator = licitatiiModel.Nume_Cumparator;
                existingLicitatie.PretActual = licitatiiModel.Pret_Actual;
                existingLicitatie.DataLicitatie = licitatiiModel.Data_Licitatie;
                existingLicitatie.Stare = licitatiiModel.Stare;
                dbContext.SaveChanges();
            }
        }

        public void DeleteUtilizator(LicitatiiModel licitatiiModel)
        {
            Licitatii existingLicitatie = dbContext.Licitatii.FirstOrDefault(x => x.IdLicitatie == licitatiiModel.ID_Licitatie);

            if (existingLicitatie != null)
            {
                dbContext.Licitatii.Remove(existingLicitatie);
                dbContext.SaveChanges();
            }
        }

        private LicitatiiModel MapDbObjectToModel(Licitatii dbLicitatii)
        {
            LicitatiiModel licitatiiModel = new LicitatiiModel();

            if (dbLicitatii != null)
            {
                licitatiiModel.ID_Licitatie = dbLicitatii.IdLicitatie;
                licitatiiModel.ID_Produs = dbLicitatii.IdProdus;
                licitatiiModel.Nume_Cumparator = dbLicitatii.Nume_Cumparator;
                licitatiiModel.Pret_Actual = dbLicitatii.PretActual;
                licitatiiModel.Data_Licitatie = dbLicitatii.DataLicitatie;
                licitatiiModel.Stare = dbLicitatii.Stare;
            }

            return licitatiiModel;
        }

        private Licitatii MapModelToDbObject(LicitatiiModel licitatiiModel)
        {
            Licitatii licitatie = new Licitatii();

            if (licitatiiModel != null)
            {
                licitatie.IdLicitatie = licitatiiModel.ID_Licitatie;
                licitatie.IdProdus = licitatiiModel.ID_Produs;
                licitatie.Nume_Cumparator = licitatiiModel.Nume_Cumparator;
                licitatie.PretActual = licitatiiModel.Pret_Actual;
                licitatie.DataLicitatie = licitatiiModel.Data_Licitatie;
                licitatie.Stare = licitatiiModel.Stare;

            }

            return licitatie;
        }
    }
}

using LicitatiiOnline.Data;
using LicitatiiOnline.Models.DBObjects;
using LicitatiiOnline.Models;


namespace LicitatiiOnline.Repository
{
    public class ImagineProdusRepository
    {
        private ApplicationDbContext dbContext;

        public ImagineProdusRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }

        public ImagineProdusRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ImagineProdusModel> GetAllImagini()
        {
            List<ImagineProdusModel> imaginiList = new List<ImagineProdusModel>();

            foreach (ImagineProdus dbImagineProdus in dbContext.ImagineProdus)
            {
                imaginiList.Add(MapDbObjectToModel(dbImagineProdus));
            }

            return imaginiList;
        }

        public ImagineProdusModel GetImagineById(Guid ID)
        {
            return MapDbObjectToModel(dbContext.ImagineProdus.FirstOrDefault(x => x.IdImagine == ID));
        }

        public List<ImagineProdusModel> GetImaginiByIdProdus(Guid ID)
        {
            List<ImagineProdusModel> imaginiList = new List<ImagineProdusModel>();

            foreach (ImagineProdus dbImagineProdus in dbContext.ImagineProdus.Where(x => x.IdImagine == ID))
            {
                imaginiList.Add(MapDbObjectToModel(dbImagineProdus));
            }

            return imaginiList;
        }

        public void InsertImaginiProdus(ImagineProdusModel imaginiModel)
        {
            imaginiModel.IdImagine = Guid.NewGuid();

            dbContext.ImagineProdus.Add(MapModelToDbObject(imaginiModel));
            dbContext.SaveChanges();
        }

        public void UpdateImagini(ImagineProdusModel imaginiModel)
        {
            ImagineProdus existingImagini = dbContext.ImagineProdus.FirstOrDefault(x => x.IdImagine == imaginiModel.IdImagine);

            if (existingImagini != null)
            {
                existingImagini.IdImagine = imaginiModel.IdImagine;
                existingImagini.IdProdus = imaginiModel.IdProdus;
                existingImagini.CaleImagine = imaginiModel.CaleImagine;
                dbContext.SaveChanges();
            }
        }

        public void DeleteImagini(ImagineProdusModel imaginiModel)
        {
            ImagineProdus existingImagini = dbContext.ImagineProdus.FirstOrDefault(x => x.IdImagine == imaginiModel.IdImagine);

            if (existingImagini != null)
            {
                dbContext.ImagineProdus.Remove(existingImagini);
                dbContext.SaveChanges();
            }
        }

        private ImagineProdusModel MapDbObjectToModel(ImagineProdus dbImaginiProdus)
        {
            ImagineProdusModel imaginiModel = new ImagineProdusModel();

            if (dbImaginiProdus != null)
            {
                imaginiModel.IdImagine = dbImaginiProdus.IdImagine;
                imaginiModel.IdProdus = dbImaginiProdus.IdProdus;
                imaginiModel.CaleImagine = dbImaginiProdus.CaleImagine;

            }

            return imaginiModel;
        }

        private ImagineProdus MapModelToDbObject(ImagineProdusModel imaginiModel)
        {
            ImagineProdus imaginiProdus = new ImagineProdus();

            if (imaginiModel != null)
            {
                imaginiProdus.IdImagine = imaginiModel.IdImagine;
                imaginiProdus.IdProdus = imaginiModel.IdProdus;
                imaginiProdus.CaleImagine = imaginiModel.CaleImagine;

            }

            return imaginiProdus;
        }
    }
}

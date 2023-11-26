using LicitatiiOnline.Data;
using LicitatiiOnline.Models.DBObjects;
using LicitatiiOnline.Models;

namespace LicitatiiOnline.Repository
{
    public class IstoricLicitatiiRepository
    {
        private ApplicationDbContext dbContext;

        public IstoricLicitatiiRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }

        public IstoricLicitatiiRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<IstoricLicitatiiModel> GetAllIstoricLicitatii()
        {
            List<IstoricLicitatiiModel> istoricLicitatiiList = new List<IstoricLicitatiiModel>();

            foreach (IstoricLicitatii dbIstoricLicitatii in dbContext.IstoricLicitatii)
            {
                istoricLicitatiiList.Add(MapDbObjectToModel(dbIstoricLicitatii));
            }

            return istoricLicitatiiList;
        }

        public IstoricLicitatiiModel GetIstoricLicitatiiById(Guid ID)
        {
            return MapDbObjectToModel(dbContext.IstoricLicitatii.FirstOrDefault(x => x.IdIstoric == ID));
        }

        public List<IstoricLicitatiiModel> GetIstoricLicitatiiByIdLicitatii(Guid ID)
        {
            List<IstoricLicitatiiModel> istoricLicitatiiList = new List<IstoricLicitatiiModel>();

            foreach (IstoricLicitatii dbIstoricLicitatii in dbContext.IstoricLicitatii.Where(x => x.IdLicitatie == ID))
            {
                istoricLicitatiiList.Add(MapDbObjectToModel(dbIstoricLicitatii));
            }

            return istoricLicitatiiList;
        }

        public void InsertIstoricLicitatii(IstoricLicitatiiModel istoricLicitatiiModel)
        {
            istoricLicitatiiModel.ID_Istoric = Guid.NewGuid();

            dbContext.IstoricLicitatii.Add(MapModelToDbObject(istoricLicitatiiModel));
            dbContext.SaveChanges();
        }

        public void UpdateIstoricLicitatii(IstoricLicitatiiModel istoricLicitatiiModel)
        {
            IstoricLicitatii existingIstoricLicitatie = dbContext.IstoricLicitatii.FirstOrDefault(x => x.IdIstoric == istoricLicitatiiModel.ID_Istoric);

            if (existingIstoricLicitatie != null)
            { 
                existingIstoricLicitatie.IdIstoric = istoricLicitatiiModel.ID_Istoric;
                existingIstoricLicitatie.IdLicitatie = istoricLicitatiiModel.ID_Licitatie;
                existingIstoricLicitatie.PretVandut = istoricLicitatiiModel.Pret_Vandut;
                existingIstoricLicitatie.UtilizatorCastigator = istoricLicitatiiModel.Utilizator_Castigator;
                existingIstoricLicitatie.DataIncheiere = istoricLicitatiiModel.Data_Incheiere;
                dbContext.SaveChanges();
            }
        }

        public void DeleteIstoricLicitatii(IstoricLicitatiiModel istoricLicitatiiModel)
        {
            IstoricLicitatii existingIstoricLicitatie = dbContext.IstoricLicitatii.FirstOrDefault(x => x.IdIstoric == istoricLicitatiiModel.ID_Istoric);

            if (existingIstoricLicitatie != null)
            {
                dbContext.IstoricLicitatii.Remove(existingIstoricLicitatie);
                dbContext.SaveChanges();
            }
        }

        private IstoricLicitatiiModel MapDbObjectToModel(IstoricLicitatii dbIstoricLicitatii)
        {
            IstoricLicitatiiModel istoricLicitatiiModel = new IstoricLicitatiiModel();

            if (dbIstoricLicitatii != null)
            {
                istoricLicitatiiModel.ID_Istoric = dbIstoricLicitatii.IdIstoric;
                istoricLicitatiiModel.ID_Licitatie = dbIstoricLicitatii.IdLicitatie;
                istoricLicitatiiModel.Pret_Vandut = dbIstoricLicitatii.PretVandut;
                istoricLicitatiiModel.Utilizator_Castigator = dbIstoricLicitatii.UtilizatorCastigator;
                istoricLicitatiiModel.Data_Incheiere = dbIstoricLicitatii.DataIncheiere;
                
            }

            return istoricLicitatiiModel;
        }

        private IstoricLicitatii MapModelToDbObject(IstoricLicitatiiModel istoricLicitatiiModel)
        {
            IstoricLicitatii istoricLicitatii = new IstoricLicitatii();

            if (istoricLicitatiiModel != null)
            {
                istoricLicitatii.IdIstoric = istoricLicitatiiModel.ID_Istoric;
                istoricLicitatii.IdLicitatie = istoricLicitatiiModel.ID_Licitatie;
                istoricLicitatii.PretVandut = istoricLicitatiiModel.Pret_Vandut;
                istoricLicitatii.UtilizatorCastigator = istoricLicitatiiModel.Utilizator_Castigator;
                istoricLicitatii.DataIncheiere = istoricLicitatiiModel.Data_Incheiere;

            }

            return istoricLicitatii;
        }
    }


}

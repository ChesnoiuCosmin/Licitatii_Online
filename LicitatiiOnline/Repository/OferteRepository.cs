using LicitatiiOnline.Data;
using LicitatiiOnline.Models.DBObjects;
using LicitatiiOnline.Models;

namespace LicitatiiOnline.Repository
{
    public class OferteRepository
    {
        private ApplicationDbContext dbContext;

        public OferteRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }

        public OferteRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<OferteModel> GetAllOferte()
        {
            List<OferteModel> oferteList = new List<OferteModel>();

            foreach (Oferte dbOferte in dbContext.Oferte)
            {
                oferteList.Add(MapDbObjectToModel(dbOferte));
            }

            return oferteList;
        }

        public OferteModel GetOferteId(Guid ID)
        {
            return MapDbObjectToModel(dbContext.Oferte.FirstOrDefault(x => x.IdOferta == ID));
        }

        public List<OferteModel> GetOferteByIdLicitatie(Guid ID)
        {
            List<OferteModel> oferteList = new List<OferteModel>();

            foreach (Oferte dbOferte in dbContext.Oferte.Where(x => x.IdLicitatie == ID))
            {
                oferteList.Add(MapDbObjectToModel(dbOferte));
            }

            return oferteList;
        }

        public List<OferteModel> GetOferteByNume(String nume)
        {
            List<OferteModel> oferteList = new List<OferteModel>();

            foreach (Oferte dbOferte in dbContext.Oferte.Where(x => x.Nume_Ofertant == nume))
            {
                oferteList.Add(MapDbObjectToModel(dbOferte));
            }

            return oferteList;
        }

        public void InsertOferta(OferteModel oferteModel)
        {
            oferteModel.ID_Oferta = Guid.NewGuid();

            dbContext.Oferte.Add(MapModelToDbObject(oferteModel));
            dbContext.SaveChanges();
        }

        public void UpdateOferte(OferteModel oferteModel)
        {
            Oferte existingOferta = dbContext.Oferte.FirstOrDefault(x => x.IdOferta == oferteModel.ID_Oferta);

            if (existingOferta != null)
            {
                existingOferta.IdOferta = oferteModel.ID_Oferta;
                existingOferta.IdLicitatie = oferteModel.ID_Licitatie;
                existingOferta.Nume_Ofertant = oferteModel.Nume_Ofertant;
                existingOferta.SumaOfertata = oferteModel.Suma_Ofertata;
                existingOferta.DataOferire = oferteModel.Data_Oferire;
                dbContext.SaveChanges();
            }
        }

        public void DeleteOferte(OferteModel oferteModel)
        {
            Oferte existingOferta = dbContext.Oferte.FirstOrDefault(x => x.IdOferta == oferteModel.ID_Oferta);

            if (existingOferta != null)
            {
                dbContext.Oferte.Remove(existingOferta);
                dbContext.SaveChanges();
            }
        }

        private OferteModel MapDbObjectToModel(Oferte dbOferte)
        {
            OferteModel oferteModel = new OferteModel();

            if (dbOferte != null)
            {
                oferteModel.ID_Oferta = dbOferte.IdOferta;
                oferteModel.ID_Licitatie = dbOferte.IdLicitatie;
                oferteModel.Nume_Ofertant = dbOferte.Nume_Ofertant;
                oferteModel.Suma_Ofertata = dbOferte.SumaOfertata;
                oferteModel.Data_Oferire = dbOferte.DataOferire;
            }

            return oferteModel;
        }

        private Oferte MapModelToDbObject(OferteModel oferteModel)
        {
            Oferte oferte = new Oferte();

            if (oferteModel != null)
            {
                oferte.IdOferta = oferteModel.ID_Oferta;
                oferte.IdLicitatie = oferteModel.ID_Licitatie;
                oferte.Nume_Ofertant = oferteModel.Nume_Ofertant;
                oferte.SumaOfertata = oferteModel.Suma_Ofertata;
                oferte.DataOferire = oferteModel.Data_Oferire;

            }

            return oferte;
        }
    }
}

using LicitatiiOnline.Data;
using LicitatiiOnline.Models.DBObjects;
using LicitatiiOnline.Models;

namespace LicitatiiOnline.Repository
{
    public class ProdusRepository
    {

        private ApplicationDbContext dbContext;

        public ProdusRepository()
        {
            this.dbContext = new ApplicationDbContext();

        }

        public ProdusRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ProdusModel> GetAllProdus()
        {
            List<ProdusModel> produsList = new List<ProdusModel>();

            foreach (Produs dbProdus in dbContext.Produs)
            {
                produsList.Add(MapDbObjectToModel(dbProdus));
            }

            return produsList;
        }

        public ProdusModel GetProdusId(Guid ID)
        {
            return MapDbObjectToModel(dbContext.Produs.FirstOrDefault(x => x.IdProdus == ID));
        }

        public List<ProdusModel> GetProdusByUtilizatorVanzator(String ID)
        {
            List<ProdusModel> produsList = new List<ProdusModel>();

            foreach (Produs dbProdus in dbContext.Produs.Where(x => x.Utilizator_Ofertant_Curent == ID))
            {
                produsList.Add(MapDbObjectToModel(dbProdus));
            }

            return produsList;
        }

        public void InsertProdus(ProdusModel produsModel)
        {
            produsModel.ID_Produs = Guid.NewGuid();

            dbContext.Produs.Add(MapModelToDbObject(produsModel));
            dbContext.SaveChanges();
        }

        public void UpdateProdus(ProdusModel produsModel)
        {
            Produs existingProdus = dbContext.Produs.FirstOrDefault(x => x.IdProdus == produsModel.ID_Produs);

            if (existingProdus != null)
            {
                existingProdus.IdProdus = produsModel.ID_Produs;
                existingProdus.NumeProdus = produsModel.Nume_Produs;
                existingProdus.Descriere = produsModel.Descriere;
                existingProdus.Categorie = produsModel.Categorie;
                existingProdus.PretPornire = produsModel.Pret_Pornire;
                existingProdus.DataIncheiereLicitatie = produsModel.Data_Incheiere_Licitatie;
                existingProdus.Utilizator_Ofertant_Curent = produsModel.Utilizator_Ofertant_Curent;
                existingProdus.Stare = produsModel.Stare;
                existingProdus.CaleImagine = produsModel.CaleImagine;
                dbContext.SaveChanges();
            }
        }

        public void DeleteProdus(Guid id)
        {
            var existingProdus = dbContext.Produs.FirstOrDefault(x => x.IdProdus == id);

            if (existingProdus != null)
            {
                
                var licitatie = dbContext.Licitatii.FirstOrDefault(l => l.IdProdus == existingProdus.IdProdus);

                


                if (licitatie != null)
                {
                    var istoric = new IstoricLicitatii
                    {
                        IdIstoric = Guid.NewGuid(),
                        IdLicitatie = licitatie.IdLicitatie,
                        PretVandut = licitatie.PretActual,
                        UtilizatorCastigator = licitatie.Nume_Cumparator,
                        DataIncheiere = DateTime.Now

                    };

                    dbContext.IstoricLicitatii.Add(istoric);
                    dbContext.SaveChanges() ;

                    var oferte = dbContext.Oferte.Where(o => o.IdLicitatie == licitatie.IdLicitatie);
                    dbContext.Oferte.RemoveRange(oferte);

                    
                    dbContext.Licitatii.Remove(licitatie);
                }

                
                dbContext.Produs.Remove(existingProdus);
                dbContext.SaveChanges();


            }
        }

        private ProdusModel MapDbObjectToModel(Produs dbProdus)
        {
            ProdusModel produsModel = new ProdusModel();

            if (dbProdus != null)
            {
                produsModel.ID_Produs = dbProdus.IdProdus;
                produsModel.Nume_Produs = dbProdus.NumeProdus;
                produsModel.Descriere = dbProdus.Descriere;
                produsModel.Categorie = dbProdus.Categorie;
                produsModel.Pret_Pornire = dbProdus.PretPornire;
                produsModel.Data_Incheiere_Licitatie = dbProdus.DataIncheiereLicitatie?.ToUniversalTime();

                produsModel.Utilizator_Ofertant_Curent = dbProdus.Utilizator_Ofertant_Curent;
                produsModel.Stare = dbProdus.Stare;
                produsModel.CaleImagine = dbProdus.CaleImagine;

                var licitatie = dbContext.Licitatii.FirstOrDefault(l => l.IdProdus == dbProdus.IdProdus);
                if (licitatie != null)
                {
                    produsModel.Pret_Actual_Licitatie = licitatie.PretActual;
                }

            }

            return produsModel;
        }

        private Produs MapModelToDbObject(ProdusModel produsModel)
        {
            Produs produs = new Produs();

            if (produsModel != null)
            {
                produs.IdProdus = produsModel.ID_Produs;
                produs.NumeProdus = produsModel.Nume_Produs;
                produs.Descriere = produsModel.Descriere;
                produs.Categorie = produsModel.Categorie;
                produs.PretPornire = produsModel.Pret_Pornire;
                produs.DataIncheiereLicitatie= produsModel.Data_Incheiere_Licitatie?.ToUniversalTime();
                produs.Utilizator_Ofertant_Curent = produsModel.Utilizator_Ofertant_Curent;
                produs.Stare = produsModel.Stare;
                produs.CaleImagine = produsModel.CaleImagine;

            }

            return produs;
        }

        public void AdaugaOferta(Guid idProdus, string userName, decimal oferta)
        {
            var licitatie = dbContext.Licitatii.FirstOrDefault(l => l.IdProdus == idProdus);

            if (licitatie != null)
            {
                decimal ceaMaiMareOferta = dbContext.Oferte
                    .Where(o => o.IdLicitatie == licitatie.IdLicitatie)
                    .Max(o => o.SumaOfertata) ?? 0;

                if (oferta > ceaMaiMareOferta)
                {
                    var ofertaNoua = new Oferte
                    {
                        IdOferta = Guid.NewGuid(),
                        IdLicitatie = licitatie.IdLicitatie,
                        Nume_Ofertant = userName,
                        SumaOfertata = oferta,
                        DataOferire = DateTime.Now
                    };

                    dbContext.Oferte.Add(ofertaNoua);
                    dbContext.SaveChanges();

                    
                    var produs = dbContext.Produs.FirstOrDefault(p => p.IdProdus == idProdus);
                    if (produs != null)
                    {
                        produs.Utilizator_Ofertant_Curent = userName;
                        dbContext.SaveChanges();
                    }
                }
                else
                {
                }
            }
        }

        public void ActualizeazaLicitatie(Guid idProdus, decimal sumaOferta, string userName)
        {
            var licitatie = dbContext.Licitatii.FirstOrDefault(l => l.IdProdus == idProdus);

            if (licitatie != null)
            {
                licitatie.PretActual = sumaOferta;
                licitatie.Nume_Cumparator = userName;
                dbContext.SaveChanges();
            }
        }

        public void IncepeLicitatie(Guid idProdus, DateTime dataIncheiere)
        {
            var produs = dbContext.Produs.FirstOrDefault(p => p.IdProdus == idProdus);

            if (produs != null)
            {
                produs.DataIncheiereLicitatie = dataIncheiere; 
                dbContext.SaveChanges();

                var licitatieExistent = dbContext.Licitatii.FirstOrDefault(l => l.IdProdus == idProdus);

                if (licitatieExistent == null)
                {
                    var licitatieNoua = new Licitatii
                    {
                        IdLicitatie = Guid.NewGuid(),
                        IdProdus = idProdus,
                        DataLicitatie = DateTime.Now,
                        Stare = "In Progres"
                    };

                    dbContext.Licitatii.Add(licitatieNoua);
                    dbContext.SaveChanges();
                }
            }
        }

        public void ActualizeazaPretActual(Guid idProdus, decimal nouPret)
        {
            var licitatie = dbContext.Licitatii.FirstOrDefault(l => l.IdProdus == idProdus);

            if (licitatie != null)
            {
                licitatie.PretActual = nouPret;
                dbContext.SaveChanges();
            }
        }
        public void ActualizeazaStatusLicitatie(Guid idProdus, string nouStatus)
        {
            var licitatie = dbContext.Licitatii.FirstOrDefault(l => l.IdProdus == idProdus);

            if (licitatie != null)
            {
                licitatie.Stare = nouStatus;
                dbContext.SaveChanges();
            }
        }

    }
}

using LicitatiiOnline.Data;
using LicitatiiOnline.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace LicitatiiOnline.Controllers
{

    public class ProdusController : Controller
    {

        private Repository.ProdusRepository _repository;
        private Repository.OferteRepository _repositoryOferte;
        private Repository.LicitatiiRepository _repositoryLicitatii;
        private IWebHostEnvironment _hostEnvironment;

        public ProdusController(ApplicationDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            _repository = new Repository.ProdusRepository(dbContext);
            _repositoryOferte = new Repository.OferteRepository(dbContext);
            _hostEnvironment = hostEnvironment;
        }

        // GET: ProdusController
        public IActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            var produse = _repository.GetAllProdus().OrderBy(p => p.Nume_Produs).ToList().ToPagedList(pageNumber, pageSize);

            return View(produse);
        }



        // GET: ProdusController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _repository.GetProdusId(id);
            return View("ProduseDetails", model);
        }

        // GET: ProdusController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View("CreateProdus");
        }

        // POST: ProdusController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(ProdusModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.IncarcareImagine != null && model.IncarcareImagine.Length > 0)
                    {
                        string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.IncarcareImagine.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.IncarcareImagine.CopyToAsync(fileStream);
                        }

                        model.CaleImagine = "/Images/" + uniqueFileName;
                    }

                    _repository.InsertProdus(model);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {

                return RedirectToAction("Index");
            }

            return View("CreateProdus", model);
        }

        // GET: ProdusController/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            var model = _repository.GetProdusId(id);
            return View("EditProdus", model);
        }

        // POST: ProdusController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(Guid id, ProdusModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingProdus = _repository.GetProdusId(id);

                    if (existingProdus == null)
                    {
                        return NotFound();
                    }

                    if (model.IncarcareImagine != null && model.IncarcareImagine.Length > 0)
                    {
                        string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.IncarcareImagine.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.IncarcareImagine.CopyToAsync(fileStream);
                        }

                        existingProdus.CaleImagine = "/Images/" + uniqueFileName;
                    }

                    existingProdus.Nume_Produs = model.Nume_Produs;
                    existingProdus.Descriere = model.Descriere;
                    existingProdus.Categorie = model.Categorie;
                    existingProdus.Pret_Pornire = model.Pret_Pornire;
                    existingProdus.Data_Incheiere_Licitatie = model.Data_Incheiere_Licitatie?.ToUniversalTime();
                    existingProdus.Utilizator_Ofertant_Curent = model.Utilizator_Ofertant_Curent;
                    existingProdus.Stare = model.Stare;

                    _repository.UpdateProdus(existingProdus);

                    return RedirectToAction("Index");
                }

                return View("EditProdus", model);
            }
            catch
            {
                return View("EditProdus", model);
            }
        }


        // GET: ProdusController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            var model = _repository.GetProdusId(id);
            return View("DeleteProdus", model);
        }

        // POST: ProdusController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _repository.DeleteProdus(id);
                return RedirectToAction("Index");
            }
            catch
            {
                var model = _repository.GetProdusId(id);
                return View("DeleteProdus", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdaugaOferta(Guid idProdus, Guid idLicitatie, decimal oferta, ProdusModel model)
        {
            var userName = User.Identity.Name;
            var produs = _repository.GetProdusId(idProdus);

            if (produs != null)
            {
                if (produs.Data_Incheiere_Licitatie == null || produs.Data_Incheiere_Licitatie >= DateTime.Now)
                {

                    _repository.ActualizeazaLicitatie(idProdus, oferta, userName);
                    
                }
                else
                {

                    ModelState.AddModelError("Error", "Licitatia nu este in desfasurare.");
                    return RedirectToAction("Index");
                }

                if (produs.Pret_Pornire >= oferta)
                {
                    ModelState.AddModelError("sumaOferta", "Oferta trebuie să fie mai mare decât prețul de pornire.");
                    return RedirectToAction("Index");
                }

                _repository.AdaugaOferta(idProdus, userName, oferta);
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncepeLicitatie(Guid idProdus, DateTime dataIncheiere)
        {
            try
            {
                var produs = _repository.GetProdusId(idProdus);

                if (produs != null)
                {
                    _repository.IncepeLicitatie(idProdus, dataIncheiere); 

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Error", "Produsul nu a fost găsit.");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return RedirectToAction("Index");
            }
        }

    }
}
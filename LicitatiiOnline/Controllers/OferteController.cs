using LicitatiiOnline.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LicitatiiOnline.Controllers
{
    public class OferteController : Controller
    {

        private Repository.OferteRepository _repository;

        public OferteController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.OferteRepository(dbContext);
        }

        // GET: OferteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OferteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OferteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OferteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OferteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OferteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        // GET: OferteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OferteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
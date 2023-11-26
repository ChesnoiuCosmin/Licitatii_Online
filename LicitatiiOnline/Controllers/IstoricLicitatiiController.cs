using LicitatiiOnline.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LicitatiiOnline.Controllers
{
    public class IstoricLicitatiiController : Controller
    {
        private Repository.IstoricLicitatiiRepository _repository;

        public IstoricLicitatiiController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.IstoricLicitatiiRepository(dbContext);
        }


        // GET: IstoricLicitatiiController
        public ActionResult Index()
        {
            return View();
        }

        // GET: IstoricLicitatiiController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IstoricLicitatiiController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IstoricLicitatiiController/Create
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

        // GET: IstoricLicitatiiController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IstoricLicitatiiController/Edit/5
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

        // GET: IstoricLicitatiiController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IstoricLicitatiiController/Delete/5
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

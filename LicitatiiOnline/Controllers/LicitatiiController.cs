using LicitatiiOnline.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LicitatiiOnline.Controllers
{
    public class LicitatiiController : Controller
    {

        private Repository.LicitatiiRepository _repository;

        public LicitatiiController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.LicitatiiRepository(dbContext);
        }

        // GET: LicitatiiController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LicitatiiController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LicitatiiController/Create
        public ActionResult Create()
        {
            return View("CreateLicitatie");
        }

        // POST: LicitatiiController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Models.LicitatiiModel model = new Models.LicitatiiModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.InsertLicitatie(model);

                }
                return View("CreateLicitatie");
            }
            catch
            {
                return View("CreateLicitatie");
            }
        }

        // GET: LicitatiiController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LicitatiiController/Edit/5
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

        // GET: LicitatiiController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LicitatiiController/Delete/5
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


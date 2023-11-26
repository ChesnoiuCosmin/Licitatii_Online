using LicitatiiOnline.Data;
using LicitatiiOnline.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LicitatiiOnline.Controllers
{

    public class RecenziiController : Controller
    {

        private Repository.RecenziiRepository _repository;

        public RecenziiController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.RecenziiRepository(dbContext);
        }

        // GET: RecenziiController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RecenziiController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RecenziiController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecenziiController/Create
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

        // GET: RecenziiController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _repository.GetRecenziiId(id);
            return View("EditRecenzii", model);
        }

        // POST: RecenziiController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new RecenziiModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.UpdateRecenzii(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", id);
                }

            }
            catch
            {
                return RedirectToAction("Index", id);
            }
        }

        // GET: RecenziiController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RecenziiController/Delete/5
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
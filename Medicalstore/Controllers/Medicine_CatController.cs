using Medicalstore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medicalstore.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class Medicine_CatController : Controller
    {
        Medicine_Cat m=new Medicine_Cat();
        Medicine_CatRepository mr= new Medicine_CatRepository();    
        // GET: Medicine_CatController
        public ActionResult Index()
        {
            List<Medicine_Cat>medicine_Cats=mr.GetAll();
            return View(medicine_Cats);
        }

        // GET: Medicine_CatController/Details/5
        public ActionResult Details(int id)
        {
            m=mr.Get(id);
            return View(m);
        }

        // GET: Medicine_CatController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medicine_CatController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Medicine_Cat m)
        {
            try
            {
                int x = mr.Add(m);
                if (x > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
            return View();  
        }

        // GET: Medicine_CatController/Edit/5
        public ActionResult Edit(int id)
        {
            m = mr.Get(id);
            return View(m);
        }

        // POST: Medicine_CatController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Medicine_Cat m)
        {
            try
            {
                int x = mr.Update(m);
                if (x > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
            return View();
        }

        // GET: Medicine_CatController/Delete/5
        public ActionResult Delete(int id)
        {
            m = mr.Get(id);
            return View(m);
        }

        // POST: Medicine_CatController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                int x = mr.Delete(id);
                if (x > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
            return View();
        }
    }
}

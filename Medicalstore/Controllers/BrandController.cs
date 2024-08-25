using Medicalstore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medicalstore.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class BrandController : Controller
    {
        Brand b=new Brand();
        BrandRepository br =new BrandRepository();  
        // GET: BrandController
       
        public ActionResult Index()
        {
            List<Brand>brandlist = br.GetAll();
            return View(brandlist);
        }

        // GET: BrandController/Details/5
        public ActionResult Details(int id)
        {
            b=br.Get(id);
            return View(b);
        }

        // GET: BrandController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand b)
        {
            try
            {
                int x = br.Add(b);
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

        // GET: BrandController/Edit/5
        public ActionResult Edit(int id)
        {
            b = br.Get(id);
            return View(b);
        }

        // POST: BrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Brand b)
        {
            try
            {
                int x = br.Update(b);
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

        // GET: BrandController/Delete/5
        public ActionResult Delete(int id)
        {
            b = br.Get(id);
            return View(b);
        }

        // POST: BrandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                int x = br.Delete(id);
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

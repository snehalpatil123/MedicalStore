using Medicalstore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medicalstore.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class Medicine_MasterController : Controller
    {
        Medicine_Master m=new Medicine_Master();
        Medicine_masterRepository mr=new Medicine_masterRepository();
        Medicine_CatRepository mc=new Medicine_CatRepository();
        BrandRepository br=new BrandRepository();   
        public void SetDropdown() 
        {     
            List<Medicine_Cat> medicine_Cats = mc.GetAll ();
            ViewData["medicine_Cats"]=medicine_Cats;
            List<Brand>brands=br.GetAll ();
            ViewData["brands"]=brands;  
        
        }
      
        // GET: Medicine_MasterController
        public ActionResult Index()
        {
            List<Medicine_Master> Medicine_Masterlist=mr.GetAll();
            return View(Medicine_Masterlist);
        }

        // GET: Medicine_MasterController/Details/5
        public ActionResult Details(int id)
        {
            m=mr.Get(id);
            return View(m);
        }

        // GET: Medicine_MasterController/Create
        public ActionResult Create()
        {
            SetDropdown();  
            return View();
        }

        // POST: Medicine_MasterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Medicine_Master m)
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

        // GET: Medicine_MasterController/Edit/5
        public ActionResult Edit(int id)
        {
            SetDropdown();
            m=mr.Get(id);
            return View(m);
        }

        // POST: Medicine_MasterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Medicine_Master m)
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

        // GET: Medicine_MasterController/Delete/5
        public ActionResult Delete(int id)
        {
            m = mr.Get(id);
            return View(m);
        }

        // POST: Medicine_MasterController/Delete/5
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

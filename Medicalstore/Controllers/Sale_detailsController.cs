using Medicalstore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medicalstore.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class Sale_detailsController : Controller
    {
        Sale_details s = new Sale_details();
        Sale_detailsRepository sr = new Sale_detailsRepository();
        Sale_masterRepository sm=new Sale_masterRepository();
        Medicine_masterRepository mr = new Medicine_masterRepository();

        public void SetDropDown()
        { 
           List<Sale_master>sale_master=sm.GetAll();
            ViewData["Sale_master"]=sale_master;    
            List<Medicine_Master>medicine=mr.GetAll();
            ViewData["Medicine_master"]=medicine;   
        }
     
        // GET: Sale_detailsController
        public ActionResult Index()
        {
           
            List<Sale_details> sale_details = sr.GetAll();
            return View(sale_details);

        }

        // GET: Sale_detailsController/Details/5
        public ActionResult Details(int id)
        {
            s = sr.Get(id);
            return View(s);

        }

        // GET: Sale_detailsController/Create
        public ActionResult Create()
        {
            SetDropDown();  
            return View();
        }

        // POST: Sale_detailsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sale_details s)
        {
            try
            {
                s.sale_id = (int)HttpContext.Session.GetInt32("sale_id");
                int x = sr.Add(s);
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

        // GET: Sale_detailsController/Edit/5
        public ActionResult Edit(int id)
        {
            SetDropDown();
            s=sr.Get(id);
            return View(s);
        }

        // POST: Sale_detailsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Sale_details s)
        {
            try
            {
                int x = sr.Update(s);
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

        // GET: Sale_detailsController/Delete/5
        public ActionResult Delete(int id)
        {
             s = sr.Get(id);    
            return View(s);
        }

        // POST: Sale_detailsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                int x = sr.Delete(id);
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

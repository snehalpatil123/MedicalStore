using Medicalstore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medicalstore.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class Sale_masterController : Controller
    {
        Sale_master s=new Sale_master();    
        Sale_masterRepository sr=new Sale_masterRepository();   
        CustomerRepository cr = new CustomerRepository();

        public void SetDropdown()
        {
            List<Customer> customers = cr.GetAll();
            ViewData["customers"] = customers;

        }
        // GET: Sale_masterController
        public ActionResult Index()
        {
            HttpContext.Session.SetInt32("sale_id", 0);
            List<Sale_master> list = sr.GetAll();
            return View(list);
        }

        // GET: Sale_masterController/Details/5
        public ActionResult Details(int id)
        {
            s=sr.Get(id);
            return View(s);
        }

        // GET: Sale_masterController/Create
        public ActionResult Create()
        {
            SetDropdown();
            return View();
        }

        // POST: Sale_masterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sale_master s)
        {
            string value = Request.Form["response"].ToString();
            if (value != "Create")
            {
                return RedirectToAction("Create","Customer");
            }
            else
            {
                try
                {
                    int x = sr.Add(s);
                    if (x > 0)
                    {
                        HttpContext.Session.SetInt32("sale_id", s.sale_id);
                        return RedirectToAction("Create","Sale_details");
                    }
                }
                catch
                {
                    return View();
                }
            return View();
            }
        }

        // GET: Sale_masterController/Edit/5
        public ActionResult Edit(int id)
        {
            SetDropdown();
            s=sr.Get(id);
            return View(s);
        }

        // POST: Sale_masterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Sale_master s)  
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

        // GET: Sale_masterController/Delete/5
        public ActionResult Delete(int id)
        {
            s = sr.Get(id);
            return View(s);
        }

        // POST: Sale_masterController/Delete/5
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

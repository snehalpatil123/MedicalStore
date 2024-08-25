using Medicalstore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medicalstore.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class ReceiptController : Controller
    {
        Receipt r=new Receipt();
        ReceiptRepository rr=new ReceiptRepository();   
        CustomerRepository cr=new CustomerRepository();

        public void SetDropdown()
        {
         List<Customer> customers=cr.GetAll();
            ViewData["customers"]=customers;
        
        }
        // GET: ReceiptController
        public ActionResult Index()
        {
            List<Receipt> list = rr.GetAll();
            return View(list);
        }

        // GET: ReceiptController/Details/5
        public ActionResult Details(int id)
        {
            r=rr.Get(id);
            return View(r);
        }

        // GET: ReceiptController/Create
        public ActionResult Create()
        {
            SetDropdown();
            return View();
        }

        // POST: ReceiptController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Receipt r)
        {
            try
            {
                int x = rr.Add(r);
                if (x > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }return View();
        }

        // GET: ReceiptController/Edit/5
        public ActionResult Edit(int id)
        {
            SetDropdown();
            r=rr.Get(id);
            return View(r);
        }

        // POST: ReceiptController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Receipt r)
        {
            try
            {
                int x = rr.Update(r);
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

        // GET: ReceiptController/Delete/5
        public ActionResult Delete(int id)
        {
            r=rr.Get(id);
            return View(r);
        }

        // POST: ReceiptController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                int x = rr.Delete(id);
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

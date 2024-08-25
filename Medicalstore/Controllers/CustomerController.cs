using Medicalstore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medicalstore.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class CustomerController : Controller
    {
        Customer c=new Customer();  
        CustomerRepository  cr=new CustomerRepository();    
        // GET: CustomerController
        public ActionResult Index()
        {
            List<Customer> Customerlist = cr.GetAll();
            return View(Customerlist);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            c=cr.Get(id);   
            return View(c);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer c)
        {
            try
            {
                int x = cr.Add(c);
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

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            c=cr.Get(id);   
            return View(c);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Customer c)
        {
            try
            {
                int x = cr.Update(c);
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

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            c=cr.Get(id);
            return View(c);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                int x = cr.Delete(id);
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

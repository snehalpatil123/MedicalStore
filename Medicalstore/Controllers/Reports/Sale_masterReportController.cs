using Medicalstore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Medicalstore.Controllers.Reports
{
    public class Sale_masterReportController : Controller
    {
        SqlCommand cmd;
        SqlConnection cn;
        SqlDataReader dr;
        CustomerRepository cr=new CustomerRepository(); 

        public void OpenConn()
        {
            cn = new SqlConnection();
            cn.ConnectionString = "Data Source=DESKTOP-KR111H2\\SQLEXPRESS;Initial Catalog=Medicinestore;Integrated Security=True;Encrypt=True;TrustServerCertificate=True ";
            cn.Open();
        }
        public IActionResult Sale_masterlistView()
        {

            OpenConn();
            List<dynamic> sale_Masters = new List<dynamic>();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select sale_id,sale_date,cust_nm,grand_total from Sale_master,Customer where Customer.cust_id=Sale_master.cust_id";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var s = new
                {

                    sale_id = Convert.ToInt32(dr[0]),
                    sale_date = Convert.ToDateTime(dr[1]),
                    cust_nm = Convert.ToString(dr[2]),
                    grand_total = Convert.ToDouble(dr[3])


                };
            sale_Masters.Add(s);
        }
            ViewData["sale_Masters"] = sale_Masters;
            return View();
        }
        public IActionResult DateWiseSale_masterView() { return View(); }
        [HttpPost]
        public IActionResult DateWiseSale_masterView(IFormCollection f)
        {
            string fromdate, todate;
            fromdate = f["t1"];
            todate = f["t2"];
            OpenConn();
            List<Sale_master> Salelist = new List<Sale_master>();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Sale_master where sale_date between @fromdate and @todate";
            cmd.Parameters.AddWithValue("@fromdate", fromdate);
            cmd.Parameters.AddWithValue("@todate", todate);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Sale_master s=new Sale_master();

                s.sale_id = Convert.ToInt32(dr[0]);
                s.sale_date = Convert.ToDateTime(dr[1]);
                s.cust_id = Convert.ToInt32(dr[2]);
                s.grand_total = Convert.ToDouble(dr[3]);
                Salelist.Add(s);    
            }
            ViewData["Salelist"] = Salelist;
            return View();
        }
        public List<Customer> SetDropdown()
        {
            List<Customer> Customerlist= cr.GetAll();
            return Customerlist;
        }
        public IActionResult CustomerwiseSale_masterView()
        {
            List<Customer> Customerlist = SetDropdown();
            ViewData["Customerlist"] = Customerlist;
            return View();
        }
        [HttpPost]
        public IActionResult CustomerwiseSale_masterView(IFormCollection f)
        {
            CustomerwiseSale_masterView();
            int customerId = Convert.ToInt32(f["t1"]);
            List<dynamic> Customerlist= new List<dynamic>();

            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select sale_id,sale_date,cust_nm,grand_total from Sale_master,Customer where Customer.cust_id=Sale_master.cust_id and Sale_master.cust_id=@cust_id";
            cmd.Parameters.AddWithValue("@cust_id", customerId);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                var m = new
                {
                    sale_id = Convert.ToInt32(dr[0]),
                    sale_date = Convert.ToDateTime(dr[1]),
                    cust_nm = Convert.ToString(dr[2]),
                    grand_total = Convert.ToInt32(dr[3]),

                };
                Customerlist.Add(m);
            }
            ViewData["sale_Masters"] = Customerlist;
            return View();
        }

    }
}

using Medicalstore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Medicalstore.Controllers.Reports
{
    public class ReceiptReportController : Controller
    {
        SqlCommand cmd;
        SqlConnection cn;
        SqlDataReader dr;
        CustomerRepository cr = new CustomerRepository();
        public void OpenConn()
        {
            cn = new SqlConnection();
            cn.ConnectionString = "Data Source=DESKTOP-KR111H2\\SQLEXPRESS;Initial Catalog=Medicinestore;Integrated Security=True;Encrypt=True;TrustServerCertificate=True ";
            cn.Open();
        }
        public IActionResult ReceiptlistView()
        {
            List<dynamic> list = new List<dynamic>();
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select rec_id,rec_date,cust_nm,rec_amt from Receipt,Customer where Customer.cust_id=Receipt.cust_id";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var m = new
                {
                    rec_id = Convert.ToInt32(dr[0]),
                    rec_date = Convert.ToDateTime(dr[1]),
                    cust_nm = Convert.ToString(dr[2]),
                    rec_amt = Convert.ToInt32(dr[3]),

                };
                list.Add(m);
            }
            ViewData["list"] = list;
            return View();
        }
        public IActionResult DateWiseReceiptView() { return View(); }
        [HttpPost]
        public IActionResult DateWiseReceiptView(IFormCollection f)
        {
            string fromdate, todate;
            fromdate = f["t1"];
            todate = f["t2"];
            OpenConn();
            List<Receipt> Receiptlist = new List<Receipt>();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Receipt where rec_date between @fromdate and @todate";
            cmd.Parameters.AddWithValue("@fromdate", fromdate);
            cmd.Parameters.AddWithValue("@todate", todate);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Receipt r=new Receipt();
                r.rec_id = Convert.ToInt32(dr[0]);
                r.rec_date = Convert.ToDateTime(dr[1]);
                r.cust_id = Convert.ToInt32(dr[2]);
                r.rec_amt = Convert.ToInt32(dr[3]);
                Receiptlist.Add(r);
               
            }
            ViewData["Receiptlist"] = Receiptlist;
            return View();
        }

        public List<Customer> SetDropdown()
        {
            List<Customer> Customerlist = cr.GetAll();
            return Customerlist;
         }
        public IActionResult CustomerwiseReceiptView()
        {
            List<Customer> Customerlist = SetDropdown();
            ViewData["Customerlist"] = Customerlist;
            return View();
        }
        [HttpPost]
        public IActionResult CustomerwiseReceiptView(IFormCollection f)
        {
            CustomerwiseReceiptView();
            int customerId = Convert.ToInt32(f["t1"]);
            List<dynamic> Customerlist = new List<dynamic>();

            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select rec_id,rec_date,cust_nm,rec_amt from Receipt,Customer where Customer.cust_id=Receipt.cust_id and Receipt.cust_id=@cust_id";
            cmd.Parameters.AddWithValue("@cust_id", customerId);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                var m = new
                {
                    rec_id = Convert.ToInt32(dr[0]),
                    rec_date = Convert.ToDateTime(dr[1]),
                    cust_nm = Convert.ToString(dr[2]),
                    rec_amt = Convert.ToInt32(dr[3]),

                };
                Customerlist.Add(m);
            }
            ViewData["list"] = Customerlist;
            return View();
        }

    }
}

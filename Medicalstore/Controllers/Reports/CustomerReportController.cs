using Medicalstore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Medicalstore.Controllers.Reports
{
    public class CustomerReportController : Controller
    {
        SqlCommand cmd;
        SqlConnection cn;
        SqlDataReader dr;

        public void OpenConn()
        {
            cn = new SqlConnection();
            cn.ConnectionString = "Data Source=DESKTOP-KR111H2\\SQLEXPRESS;Initial Catalog=Medicinestore;Integrated Security=True;Encrypt=True;TrustServerCertificate=True ";
            cn.Open();
        }
        public IActionResult CustomerlistView()
        {
            List<Customer> Customerlist = new List<Customer>();
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Customer ";

            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Customer c = new Customer();
                c.cust_id = Convert.ToInt32(dr[0]);
                c.cust_nm = Convert.ToString(dr[1]);
                c.cust_address = Convert.ToString(dr[2]);
                c.cust_phone = Convert.ToString(dr[3]);
                c.cust_email = Convert.ToString(dr[4]);
                Customerlist.Add(c);
            }
            ViewData ["Customerlist"]=Customerlist;
            return View();
        }
          
    }
}

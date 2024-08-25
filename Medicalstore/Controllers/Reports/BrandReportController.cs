using Medicalstore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Medicalstore.Controllers.Reports
{
    public class BrandReportController : Controller
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
        public IActionResult BrandlistView()
        {
            List<Brand> Brandlist = new List<Brand>();
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Brand";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Brand b = new Brand();
                b.brand_id = Convert.ToInt32(dr[0]);
                b.brand_nm = Convert.ToString(dr[1]);
                Brandlist.Add(b);
            }
            ViewData["Brandlist"] = Brandlist;
            return View();
        
           
        }
    }
}

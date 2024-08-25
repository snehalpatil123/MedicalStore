using Medicalstore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Medicalstore.Controllers.Reports
{
    public class Medicine_CatReportController : Controller
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
        public IActionResult Medicine_CatlistView()
        {
            List<Medicine_Cat> medicine_Cats = new List<Medicine_Cat>();
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Medicine_Cat";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Medicine_Cat m = new Medicine_Cat();
                m.cat_id = Convert.ToInt32(dr[0]);
                m.cat_nm = Convert.ToString(dr[1]);
                medicine_Cats.Add(m);
            }
            ViewData["medicine_Cats"] = medicine_Cats;
            return View();
        }
    }
}

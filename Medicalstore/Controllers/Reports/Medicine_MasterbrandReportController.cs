using Medicalstore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Medicalstore.Controllers.Reports
{
    public class Medicine_MasterbrandReportController : Controller
    {
        SqlCommand cmd;
        SqlConnection cn;
        SqlDataReader dr;
        Brand b = new Brand();
        BrandRepository br = new BrandRepository();

        public void OpenConn()
        {

            cn = new SqlConnection();
            cn.ConnectionString = "Data Source=DESKTOP-KR111H2\\SQLEXPRESS;Initial Catalog=Medicinestore;Integrated Security=True;Encrypt=True;TrustServerCertificate=True ";
            cn.Open();

        }
        public IActionResult Medicine_MasterbrandlistView()
        {
            List<dynamic> medicine_Masterslist = new List<dynamic>();
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select med_id,med_nm,cat_nm,brand_nm,contents,rate,stock from  Medicine_Master,Medicine_Cat,Brand where Medicine_Cat.cat_id=Medicine_Master.cat_id and brand.brand_id=Medicine_Master.brand_id";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var m = new
                {
                    med_id = Convert.ToInt32(dr[0]),
                    med_nm = Convert.ToString(dr[1]),
                    cat_nm = Convert.ToString(dr[2]),
                    brand_nm = Convert.ToString(dr[3]),
                    contents = Convert.ToString(dr[4]),
                    rate = Convert.ToInt32(dr[5]),
                    stock = Convert.ToInt32(dr[6]),
                };

                medicine_Masterslist.Add(m);

            }
            ViewData["medicine_Masterslist"] = medicine_Masterslist;
            return View();
        }
        public List<Brand> SetDropdown()

        {
            List<Brand> brandlist = br.GetAll();
            return brandlist;

        }

        public IActionResult BrandwiseMedicine_MasterView()
        {
            List<Brand> brandlist = SetDropdown();
            ViewData["brand"] = brandlist;
            return View();
        }
        [HttpPost]
        public IActionResult BrandwiseMedicine_MasterView(IFormCollection f)
        {
            BrandwiseMedicine_MasterView();
            int brandId = Convert.ToInt32(f["t1"]);
            List<dynamic> brandlist = new List<dynamic>();
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select med_id,med_nm,cat_nm,brand_nm,contents,rate,stock from  Medicine_Master,Medicine_Cat,Brand where Medicine_Cat.cat_id=Medicine_Master.cat_id and brand.brand_id=Medicine_Master.brand_id and Medicine_Master.brand_id=@brand_id";
            cmd.Parameters.AddWithValue("@brand_id", brandId);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var b = new
                {
                    med_id = Convert.ToInt32(dr[0]),
                    med_nm = Convert.ToString(dr[1]),
                    cat_nm = Convert.ToString(dr[2]),
                    brand_nm = Convert.ToString(dr[3]),
                    contents = Convert.ToString(dr[4]),
                    rate = Convert.ToInt32(dr[5]),
                    stock = Convert.ToInt32(dr[6]),
                };

                brandlist.Add(b);
            }
            ViewData["medicine_Masterslist"] = brandlist;
            return View();
        }
    }
}
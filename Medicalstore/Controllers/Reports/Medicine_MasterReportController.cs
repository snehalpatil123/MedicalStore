using Medicalstore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Medicalstore.Controllers.Reports
{
    public class Medicine_MasterReportController : Controller
    {
        SqlCommand cmd;
        SqlConnection cn;
        SqlDataReader dr;
        BrandRepository br = new BrandRepository();
        Medicine_CatRepository mr = new Medicine_CatRepository();
        public void OpenConn()
        {
            cn = new SqlConnection();
            cn.ConnectionString = "Data Source=DESKTOP-KR111H2\\SQLEXPRESS;Initial Catalog=Medicinestore;Integrated Security=True;Encrypt=True;TrustServerCertificate=True ";
            cn.Open();

        }
        public IActionResult Medicine_MasterlistView()
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
        public List<Medicine_Cat > SetDropdown()

        {
            List<Medicine_Cat> MedicineCatlist = mr.GetAll();
            return MedicineCatlist;
           
        }
       
        public IActionResult Medicine_CatwiseMedicine_MasterView()
        {
            List<Medicine_Cat> Medicinelist = SetDropdown();
            ViewData["medicine_cat"] = Medicinelist;
            return View();
        }
        [HttpPost]
        public IActionResult Medicine_CatwiseMedicine_MasterView(IFormCollection f)
        {
            Medicine_CatwiseMedicine_MasterView();
            int medicine_CatId = Convert.ToInt32(f["t1"]);
            List<dynamic> MedicineCatlist = new List<dynamic>();
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select med_id,med_nm,cat_nm,brand_nm,contents,rate,stock from  Medicine_Master,Medicine_Cat,Brand where Medicine_Cat.cat_id=Medicine_Master.cat_id and brand.brand_id=Medicine_Master.brand_id and Medicine_Master.cat_id=@cat_id";
            cmd.Parameters.AddWithValue("@cat_id", medicine_CatId);
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

                MedicineCatlist.Add(m);
            }
            ViewData["medicine_Masterslist"] = MedicineCatlist;
            return View();

        }
       
      
       
    }
}

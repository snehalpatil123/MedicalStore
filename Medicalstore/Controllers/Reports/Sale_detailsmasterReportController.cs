using Medicalstore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Medicalstore.Controllers.Reports
{
    public class Sale_detailsmasterReportController : Controller
    {
       SqlCommand cmd;
        SqlConnection cn;
        SqlDataReader dr;

      
        Sale_masterRepository sr=new Sale_masterRepository();   
        public void OpenConn()
        {

            cn = new SqlConnection();
            cn.ConnectionString = "Data Source=DESKTOP-KR111H2\\SQLEXPRESS;Initial Catalog=Medicinestore;Integrated Security=True;Encrypt=True;TrustServerCertificate=True ";
            cn.Open();
        }
        public IActionResult Sale_detailsmasterlistView()
        {
            List<dynamic> sale_Details = new List<dynamic>();
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select sale_det_id,sale_date,med_nm,sale_details.rate,qty,amt,gst,total from Sale_details,Sale_master,Medicine_master where Sale_master.sale_id=Sale_details.sale_id and Medicine_master.med_id=Sale_details.med_id";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var s = new
                {
                    sale_det_id = Convert.ToInt32(dr[0]),
                    sale_date = Convert.ToDateTime(dr[1]),
                    med_nm = Convert.ToString(dr[2]),
                    rate = Convert.ToInt32(dr[3]),
                    qty = Convert.ToInt32(dr[4]),
                    amt = Convert.ToInt32(dr[5]),
                    gst = Convert.ToDouble(dr[6]),
                    total = Convert.ToDouble(dr[7])


                };
                sale_Details.Add(s);
            }
            ViewData["sale_Details"] = sale_Details;
            return View();
        }
        public List<Sale_master> SetDropdown()
        {
            List<Sale_master> saledata= sr.GetAll();
            return saledata;
        }
        public IActionResult Sale_masterwiseSale_detailsmasterView()
        {
            List<Sale_master> saledata = SetDropdown();
            ViewData["Sale_data"] = saledata;
            return View();
        }
        [HttpPost]
        public IActionResult Sale_masterwiseSale_detailsmasterView(IFormCollection f)
        {
            Sale_masterwiseSale_detailsmasterView();
            int saleId = Convert.ToInt32(f["t1"]);
            List<dynamic> salelist = new List<dynamic>();
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select sale_det_id,sale_date,med_nm,sale_details.rate,qty,amt,gst,total from Sale_details,Sale_master,Medicine_master where Sale_master.sale_id=Sale_details.sale_id and Medicine_master.med_id=Sale_details.med_id and Sale_details.sale_id=@sale_id";
            cmd.Parameters.AddWithValue("@sale_id", saleId);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var s = new
                {
                    sale_det_id = Convert.ToInt32(dr[0]),
                    sale_date = Convert.ToDateTime(dr[1]),
                    med_nm = Convert.ToString(dr[2]),
                    rate = Convert.ToInt32(dr[3]),
                    qty = Convert.ToInt32(dr[4]),
                    amt = Convert.ToInt32(dr[5]),
                    gst = Convert.ToDouble(dr[6]),
                    total = Convert.ToDouble(dr[7])


                };
                salelist.Add(s);
            }
            ViewData["sale_Details"] = salelist;
            return View();
        }
    }
}

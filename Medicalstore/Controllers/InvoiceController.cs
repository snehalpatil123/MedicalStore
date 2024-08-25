using Medicalstore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Medicalstore.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class InvoiceController : Controller
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
        public IActionResult Index(int sale_id)
        {

            List<dynamic> Invoicelist = new List<dynamic>();
            int id=sale_id;
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select Sale_master.sale_id,sale_date,Sale_details.sale_det_id,med_nm,Sale_details.rate,qty,amt,gst,total,cust_nm,cust_address,cust_phone,cust_email from Sale_details  ,Sale_master  ,Medicine_Master  ,Customer where Sale_details.sale_id=Sale_master.sale_id and Sale_details.med_id=Medicine_Master.med_id and Sale_master.cust_id=Customer.cust_id  and Sale_master.sale_id= "+id+" order by sale_det_id";

            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var s = new
                {
                    sale_id = Convert.ToInt32(dr[0]),
                    sale_date=Convert.ToDateTime(dr[1]),
                    sale_det_id=Convert.ToInt32(dr[2]),
                    med_nm=Convert.ToString(dr[3]),
                    rate = Convert.ToInt32(dr[4]),
                    qty=Convert.ToInt32(dr[5]),
                    amt=Convert.ToInt32(dr[6]),
                    gst=Convert.ToDouble(dr[7]),
                    total=Convert.ToDouble(dr[8]),
                    cust_nm=Convert.ToString(dr[9]),    
                    cust_address=Convert.ToString(dr[10]),
                    cust_phone=Convert.ToString(dr[11]),
                    cust_email=Convert.ToString(dr[12]),
                };
                Invoicelist.Add(s);
            }
            ViewData["Invoicedata"] = Invoicelist;
            return View();
        
        }
    }
}

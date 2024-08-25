
using System.Data.SqlClient;

namespace Medicalstore.Models
{
    public class Sale_detailsRepository : ISale_detailsRepository
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;

        
        public void OpenConn() {
            cn = new SqlConnection();
            cn.ConnectionString = "Data Source=DESKTOP-KR111H2\\SQLEXPRESS;Initial Catalog=Medicinestore;Integrated Security=True;TrustServerCertificate=True ";
            cn.Open();

        }
        public int GetNewId(int sale_id)
        {
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = ("select max(sale_det_id) from Sale_details where sale_id="+sale_id);
            object x = cmd.ExecuteScalar();
            if (Convert.ToString(x) == "")
                return 1;
            else
                return (Convert.ToInt32(x) + 1);
        }
        public int GetNewSale_id()
        {
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = ("select max(sale_id) from Sale_master ");
            object x = cmd.ExecuteScalar();
            if (Convert.ToString(x) == "")
                return 1;
            else
                return (Convert.ToInt32(x)  );
        }
        public int GetRate(int med_id)
        {
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select rate from medicine_master where med_id="+med_id;
            object x = cmd.ExecuteScalar();
            int rate= Convert.ToInt32(x);
            return rate;
        }

        public int Add(Sale_details s)
        {
            OpenConn();
            s.sale_det_id=GetNewId(s.sale_id);
            s.gst = 18;
            s.rate = GetRate(s.med_id);
            s.amt = s.rate * s.qty;
            s.total = s.amt+(s.amt * 0.18);
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "insert into Sale_details values(@id,@sale_id,@med_id,@rate,@qty,@amt,@gst,@total)";
            cmd.Parameters.AddWithValue("@id", s.sale_det_id);
            cmd.Parameters.AddWithValue("@sale_id", s.sale_id);
            cmd.Parameters.AddWithValue("@med_id", s.med_id);
            cmd.Parameters.AddWithValue("@rate", s.rate);

            cmd.Parameters.AddWithValue("@qty", s.qty);
            cmd.Parameters.AddWithValue("@amt", s.amt);
            cmd.Parameters.AddWithValue("@gst", s.gst);
            cmd.Parameters.AddWithValue("@total", s.total);
           int x= cmd.ExecuteNonQuery();

            cmd.CommandText = "update sale_master set grand_total= grand_total + @total where sale_id=@sale_id";
            cmd.ExecuteNonQuery();


            cmd.CommandText = "update Medicine_Master set stock=stock - @qty where med_id=@med_id";
              cmd.ExecuteNonQuery();


            
            return x;



        }

        public int Delete(int id)
        {
            OpenConn();
            Sale_details sd = Get(id);
            int sale_id=sd.sale_id;
            double total = sd.total;
            int qty=sd.qty;
            int med_id=sd.med_id;
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "delete from Sale_details where sale_det_id=@sale_det_id and sale_id=@sale_id";
            cmd.Parameters.AddWithValue("@sale_det_id", id);
            cmd.Parameters.AddWithValue("@total", total);
            cmd.Parameters.AddWithValue("@sale_id", sale_id);
            cmd.Parameters.AddWithValue("@qty", qty);
            cmd.Parameters.AddWithValue("@med_id", med_id);
            int x = cmd.ExecuteNonQuery();
            cmd.CommandText = "update sale_master set grand_total=grand_total-@total where sale_id=@sale_id";
            cmd.ExecuteNonQuery();


            cmd.CommandText = "update Medicine_Master set stock=stock + @qty where med_id=@med_id";
            cmd.ExecuteNonQuery();

            return x;

        }

        public Sale_details Get(int id)
        {
            OpenConn();
            Sale_details s = new Sale_details();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Sale_details where sale_det_id=@id order by sale_det_id";
            cmd.Parameters.AddWithValue("@id", id);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                s.sale_det_id = Convert.ToInt32(dr[0]);
               s.sale_id= Convert.ToInt32(dr[1]);
                s.med_id = Convert.ToInt32(dr[2]);
                s.rate = Convert.ToInt32(dr[3]);
                s.qty = Convert.ToInt32(dr[4]);
                s.amt = Convert.ToInt32(dr[5]);
                s.gst = Convert.ToDouble(dr[6]);
                s.total = Convert.ToDouble(dr[7]);

            }
            dr.Close();
            return s;   

        }

       
        public List<Sale_details> GetAll()
        {
          int id=GetNewSale_id();       
            List<Sale_details> sale_Details = new List<Sale_details>();
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Sale_details where sale_id="+id+" order by sale_id";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Sale_details s = new Sale_details();
                s.sale_det_id = Convert.ToInt32(dr[0]);
                s.sale_id = Convert.ToInt32(dr[1]);
                s.med_id = Convert.ToInt32(dr[2]);
                s.rate = Convert.ToInt32(dr[3]);
                s.qty = Convert.ToInt32(dr[4]);
                s.amt = Convert.ToInt32(dr[5]);
                s.gst = Convert.ToDouble(dr[6]);
                s.total = Convert.ToDouble(dr[7]);
                sale_Details.Add(s);

            }
            return sale_Details;

        }
        public int Update(Sale_details s)
        {
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "update Sale_details set sale_id=@sale_id,med_id=@med_id,rate=@rate,qty=@qty,amt=@amt,gst=@gst,total=@total where sale_det_id=@sale_det_id";
            cmd.Parameters.AddWithValue("@sale_det_id", s.sale_det_id);
            cmd.Parameters.AddWithValue("@sale_id", s.sale_id);
            cmd.Parameters.AddWithValue("@med_id", s.med_id);
            cmd.Parameters.AddWithValue("@rate", s.rate);

            cmd.Parameters.AddWithValue("@qty", s.qty);
            cmd.Parameters.AddWithValue("@amt", s.amt);
            cmd.Parameters.AddWithValue("@gst", s.gst);
            cmd.Parameters.AddWithValue("@total", s.total);
            int x = cmd.ExecuteNonQuery();
            return x;

        }
    }
}

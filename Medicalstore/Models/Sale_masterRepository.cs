
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Medicalstore.Models
{
    public class Sale_masterRepository : ISale_masterRepository
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;

        public void OpenConn() 
        {
           
            cn = new SqlConnection();
            cn.ConnectionString = "Data Source=DESKTOP-KR111H2\\SQLEXPRESS;Initial Catalog=Medicinestore;Integrated Security=True;TrustServerCertificate=True ";
            cn.Open();

        }
        
        public int GetNewId()
        {
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = ("select max(sale_id) from Sale_Master");
            object x = cmd.ExecuteScalar();
            if (Convert.ToString(x) == "")
                return 1;
            else
                return (Convert.ToInt32(x) + 1);
        }
        public int Add(Sale_master s)
        {
            OpenConn();
            s.sale_id=GetNewId();   
            s.sale_date=System.DateTime.Now;
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "insert into Sale_master values(@id,@date,@cust_id,@grand_total)";
            cmd.Parameters.AddWithValue("@id",s.sale_id);
            cmd.Parameters.AddWithValue("@date",s.sale_date);   
            cmd.Parameters.AddWithValue("@cust_id",s.cust_id);
            cmd.Parameters.AddWithValue("@grand_total",s.grand_total);
            int x = cmd.ExecuteNonQuery();
            return x;
        }

        public int Delete(int id)
        {
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "delete from Sale_master where sale_id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            int x = cmd.ExecuteNonQuery();
            return x;
        }

        public Sale_master Get(int id)
        {
            OpenConn();
            Sale_master s= new Sale_master();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select *  from Sale_master where sale_id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                s.sale_id = Convert.ToInt32(dr[0]);
                s.sale_date=Convert.ToDateTime(dr[1]);
                s.cust_id = Convert.ToInt32(dr[2]);
                s.grand_total=Convert.ToDouble(dr[3]);


            }
            return s;
        }

        public List<Sale_master> GetAll()
        {
            List<Sale_master> sale_Masters = new List<Sale_master>();
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Sale_master order by sale_id desc";
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                Sale_master s = new Sale_master();
                s.sale_id = Convert.ToInt32(dr[0]);
                s.sale_date = Convert.ToDateTime(dr[1]);
                s.cust_id = Convert.ToInt32(dr[2]);
                s.grand_total = Convert.ToDouble(dr[3]);
                sale_Masters.Add(s);

            }
            return sale_Masters;
        }
        public int Update(Sale_master s)
        {
            OpenConn();
            cmd= new SqlCommand();  
            cmd.Connection= cn;
            cmd.CommandText = "update Sale_master set sale_date=@date,cust_id=@cust_id,grand_total=@grand_total where sale_id=@id";
            cmd.Parameters.AddWithValue("@id",s.sale_id);
            cmd.Parameters.AddWithValue("@date",s.sale_date);
            cmd.Parameters.AddWithValue("@cust_id", s.cust_id);
            cmd.Parameters.AddWithValue("@grand_total", s.grand_total);
            int x = cmd.ExecuteNonQuery();
            return x;
        }
    }
}

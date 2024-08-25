
using System.Data.SqlClient;

namespace Medicalstore.Models
{
    public class ReceiptRepository : IReceiptRepository
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        public void OpenConn() {
            cn = new SqlConnection();
            cn.ConnectionString = "Data Source=DESKTOP-KR111H2\\SQLEXPRESS;Initial Catalog=Medicinestore;Integrated Security=True;TrustServerCertificate=True ";
            cn.Open();
        }
        public int GetNewId()
        {
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = ("select max(rec_id) from Receipt");
            object x = cmd.ExecuteScalar();
            if (Convert.ToString(x) == "")
                return 1;
            else
                return (Convert.ToInt32(x) + 1);
        }
        public int Add(Receipt r)
        {
           OpenConn();
            r.rec_id = GetNewId();  
            r.rec_date =System.DateTime.Now;
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "insert into Receipt values(@id,@date,@cust_id,@amt)";
            cmd.Parameters.AddWithValue("@id", r.rec_id);
            cmd.Parameters.AddWithValue("@date", r.rec_date);
            cmd.Parameters.AddWithValue("@cust_id", r.cust_id);
            cmd.Parameters.AddWithValue("@amt",r.rec_amt);
           int x=cmd.ExecuteNonQuery();
            return x;
              
        }

        public int Delete(int id)
        {
            OpenConn ();
            cmd = new SqlCommand();
            cmd.Connection= cn;
            cmd.CommandText = "delete from Receipt where rec_id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            int x=cmd.ExecuteNonQuery();    
            return x;
        }

        public Receipt Get(int id)
        {
            OpenConn();
            Receipt r = new Receipt();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select *  from Receipt where rec_id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                r.rec_id = Convert.ToInt32(dr[0]);
                r.rec_date = Convert.ToDateTime(dr[1]);
                r.cust_id = Convert.ToInt32(dr[2]);
                r.rec_amt = Convert.ToInt32(dr[3]);

            }
            return r;
        }

        public List<Receipt> GetAll()
        {
            List<Receipt> list = new List<Receipt>();
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Receipt";
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                Receipt r = new Receipt();
                r.rec_id = Convert.ToInt32(dr[0]);
                r.rec_date = Convert.ToDateTime(dr[1]);
                r.cust_id = Convert.ToInt32(dr[2]);
                r.rec_amt = Convert.ToInt32(dr[3]);
                list.Add(r);
            }
            return list;
        }
        public int Update(Receipt r)
        {
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "update Receipt set rec_date=@date,cust_id=@cust_id,rec_amt=@amt where rec_id=@id";
            cmd.Parameters.AddWithValue("@id", r.rec_id);
            cmd.Parameters.AddWithValue("@date", r.rec_date);
            cmd.Parameters.AddWithValue("@cust_id", r.cust_id);
            cmd.Parameters.AddWithValue("@amt", r.rec_amt);
            int x = cmd.ExecuteNonQuery();
            return x;

        }
    }
}

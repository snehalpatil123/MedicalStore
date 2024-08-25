
using System.CodeDom;
using System.Data.SqlClient;

namespace Medicalstore.Models
{
    public class Medicine_masterRepository : IMedicine_MasterRepository
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
            cmd.CommandText = ("select max(med_id) from Medicine_Master");
            object x = cmd.ExecuteScalar();
            if (Convert.ToString(x) == "")
                return 1;
            else
                return (Convert.ToInt32(x) + 1);
        }
        public int Add(Medicine_Master m)
        {
            OpenConn();
            m.med_id=GetNewId();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "insert into Medicine_Master values(@id,@nm,@cat_id,@brand_id,@contents,@rate,@stock)";
             cmd.Parameters.AddWithValue("@id",m.med_id);
            cmd.Parameters.AddWithValue("@nm", m.med_nm);
            cmd.Parameters.AddWithValue("@cat_id",m.cat_id);
            cmd.Parameters.AddWithValue("@brand_id",m.brand_id);
            cmd.Parameters.AddWithValue("@contents",m.contents);
            cmd.Parameters.AddWithValue("@rate",m.rate);
            cmd.Parameters.AddWithValue("@stock", m.stock);
            int x=cmd.ExecuteNonQuery();
            return x;
        }

        public int Delete(int id)
        {
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection= cn;
            cmd.CommandText = "delete from Medicine_Master where med_id=@id";
            cmd.Parameters.AddWithValue("@id",id);
            int x= cmd.ExecuteNonQuery();   
            return x;
        }

        public Medicine_Master Get(int id)
        {
            Medicine_Master m = new Medicine_Master();
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection=cn;
            cmd.CommandText = "select * from Medicine_Master where med_id=@id";
            cmd.Parameters.AddWithValue("@id",id);
           dr= cmd.ExecuteReader();
            while (dr.Read()) {

                m.med_id = Convert.ToInt32(dr[0]);
                m.med_nm = Convert.ToString(dr[1]);
                m.cat_id=Convert.ToInt32(dr[2]);
                m.brand_id=Convert.ToInt32(dr[3]);
                m.contents = Convert.ToString(dr[4]);
                m.rate=Convert.ToInt32(dr[5]);
                m.stock=Convert.ToInt32(dr[6]);


            }
            return m;
           
        }

        public List<Medicine_Master> GetAll()
        {
            List<Medicine_Master> medicine_Masterslist = new List<Medicine_Master>();
            OpenConn();
             cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Medicine_Master";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Medicine_Master m = new Medicine_Master();
                m.med_id = Convert.ToInt32(dr[0]);
                m.med_nm = Convert.ToString(dr[1]);
                m.cat_id = Convert.ToInt32(dr[2]);
                m.brand_id = Convert.ToInt32(dr[3]);
                m.contents = Convert.ToString(dr[4]);
                m.rate = Convert.ToInt32(dr[5]);
                m.stock = Convert.ToInt32(dr[6]);
                medicine_Masterslist.Add(m);
            }
            return medicine_Masterslist;
        }
        public int Update(Medicine_Master m)
        {
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "update Medicine_Master set med_nm=@med_nm,cat_id=@cat_id,brand_id=@brand_id,contents=@contents,rate=@rate,stock=@stock where med_id=@med_id";
            cmd.Parameters.AddWithValue("@med_id",m.med_id);
            cmd.Parameters.AddWithValue("@med_nm",m.med_nm);
            cmd.Parameters.AddWithValue("@cat_id", m.cat_id);
            cmd.Parameters.AddWithValue("@brand_id", m.brand_id);
            cmd.Parameters.AddWithValue("@contents", m.contents);
            cmd.Parameters.AddWithValue("@rate", m.rate);
            cmd.Parameters.AddWithValue("@stock", m.stock);
            int x = cmd.ExecuteNonQuery();
            return x;
        

        }
    }
}

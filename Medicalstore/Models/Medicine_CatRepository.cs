
using System.Data.SqlClient;

namespace Medicalstore.Models
{
    public class Medicine_CatRepository : IMedicine_CatRepository
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
            cmd.CommandText = ("select max(cat_id) from Medicine_Cat");
            object x = cmd.ExecuteScalar();
            if (Convert.ToString(x) == "")
                return 1;
            else
                return (Convert.ToInt32(x) + 1);
        }
        public int Add(Medicine_Cat m)
        {
            OpenConn();
            m.cat_id=GetNewId();    
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "insert into Medicine_Cat values(@id,@nm)";
            cmd.Parameters.AddWithValue("@id",m.cat_id);
            cmd.Parameters.AddWithValue("@nm",m.cat_nm);
            int x=cmd.ExecuteNonQuery();
            return x;
        }

        public int Delete(int id)
        {
           OpenConn(); 
            cmd= new SqlCommand();  
            cmd.Connection= cn;
            cmd.CommandText = "delete from Medicine_Cat where cat_id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            int x= cmd.ExecuteNonQuery();   
            return x;   
        }

        public Medicine_Cat Get(int id)
        {
            Medicine_Cat m = new Medicine_Cat();
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Medicine_Cat where cat_id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                m.cat_id = Convert.ToInt32(dr[0]);
                m.cat_nm = Convert.ToString(dr[1]);
            }
            return m;
        }

        public List<Medicine_Cat> GetAll()
        {
            List<Medicine_Cat> medicine_Cats = new List<Medicine_Cat>();
            OpenConn ();
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
            return medicine_Cats;
        }

        public int Update(Medicine_Cat m)
        {
           OpenConn();
            cmd=new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "update Medicine_Cat set cat_nm=@cat_nm where cat_id=@cat_id";
            cmd.Parameters.AddWithValue("@cat_id",m.cat_id);
            cmd.Parameters.AddWithValue("@cat_nm", m.cat_nm);
            int x=cmd.ExecuteNonQuery();
            return x;
        }
    }
}


using System.Data.SqlClient;

namespace Medicalstore.Models
{
    public class BrandRepository : IBrandRepository
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
            cmd.CommandText = ("select max(brand_id) from Brand");
            object x = cmd.ExecuteScalar();
            if (Convert.ToString(x) == "")
                return 1;
            else
                return (Convert.ToInt32(x) + 1);
        }
        public int Add(Brand b)
        {
           OpenConn();
            b.brand_id = GetNewId();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "insert into Brand values(@id,@nm)";
            cmd.Parameters.AddWithValue("@id", b.brand_id);
            cmd.Parameters.AddWithValue("@nm", b.brand_nm);
            int x=cmd.ExecuteNonQuery();
            return x;
        }

        public int Delete(int id)
        {
            
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "delete  Brand where brand_id=@id ";
            cmd.Parameters.AddWithValue("@id", id);
            int x=cmd.ExecuteNonQuery();    
            return x;
        }

        public Brand Get(int id)
        {
            Brand b = new Brand();
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Brand where brand_id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                b.brand_id = Convert.ToInt32(dr[0]);
                b.brand_nm = Convert.ToString(dr[1]);

            }
            return b;
        }

        public List<Brand> GetAll()
        {
            OpenConn();
            List<Brand> Brandlist = new List<Brand>();

            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Brand ";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Brand b = new Brand();
                b.brand_id = Convert.ToInt32(dr[0]);
                b.brand_nm = Convert.ToString(dr[1]);
                Brandlist.Add(b);
            }
            return Brandlist;
        }

        public int Update(Brand b)
        {

            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "update Brand set brand_nm=@nm where brand_id=@id";
            cmd.Parameters.AddWithValue("@id", b.brand_id);
            cmd.Parameters.AddWithValue("@nm", b.brand_nm);
            int x = cmd.ExecuteNonQuery();
            return x;
        }
    }
}

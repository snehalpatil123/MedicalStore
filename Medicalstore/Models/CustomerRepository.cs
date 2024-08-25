
using System.Data.SqlClient;

namespace Medicalstore.Models
{
    public class CustomerRepository : ICustomerRepository
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
            cmd.CommandText = ("select max(cust_id) from Customer");
            object x = cmd.ExecuteScalar();
            if (Convert.ToString(x) == "")
                return 1;
            else
                return (Convert.ToInt32(x) + 1);
        }
        public int Add(Customer c)
        {
            OpenConn();
            c.cust_id=GetNewId();   
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "insert into Customer values(@id,@nm,@address,@phone,@email)";
            cmd.Parameters.AddWithValue("@id",c.cust_id);
            cmd.Parameters.AddWithValue("@nm", c.cust_nm);
            cmd.Parameters.AddWithValue("@address",c.cust_address);
            cmd.Parameters.AddWithValue("@phone", c.cust_phone);
            cmd.Parameters.AddWithValue("@email", c.cust_email);
            int x = cmd.ExecuteNonQuery();
            return x;
        }

        public int Delete(int id)
        {
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "delete Customer where cust_id=@id ";
            cmd.Parameters.AddWithValue("@id", id);
            int x = cmd.ExecuteNonQuery();
            return x;
        }

        public Customer Get(int id)
        {
           Customer c = new Customer();
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Customer where cust_id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                c.cust_id = Convert.ToInt32(dr[0]);
                c.cust_nm = Convert.ToString(dr[1]);
                c.cust_address = Convert.ToString(dr[2]);
                c.cust_phone = Convert.ToString(dr[3]);
                c.cust_email = Convert.ToString(dr[4]);

            }
            return c;
        }

        public List<Customer> GetAll()
        {
            List<Customer>Customerlist= new List<Customer>();
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Customer ";
           
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Customer c= new Customer(); 
                c.cust_id = Convert.ToInt32(dr[0]);
                c.cust_nm = Convert.ToString(dr[1]);
                c.cust_address = Convert.ToString(dr[2]);
                c.cust_phone = Convert.ToString(dr[3]);
                c.cust_email = Convert.ToString(dr[4]);
                Customerlist.Add(c);
            }
            return Customerlist;
        }

        public int Update(Customer c)
        {
            OpenConn();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "update Customer set cust_nm=@cust_nm,cust_address=@cust_address,cust_phone=@cust_phone,cust_email=@cust_email where cust_id=@cust_id";
            cmd.Parameters.AddWithValue("@cust_id", c.cust_id);
            cmd.Parameters.AddWithValue("@cust_nm", c.cust_nm);
            cmd.Parameters.AddWithValue("@cust_address", c.cust_address);
            cmd.Parameters.AddWithValue("@cust_phone", c.cust_phone);
            cmd.Parameters.AddWithValue("@cust_email", c.cust_email);
            int x = cmd.ExecuteNonQuery();
            return x;

        }
    }
}

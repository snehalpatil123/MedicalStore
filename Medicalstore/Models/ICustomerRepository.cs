namespace Medicalstore.Models
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        Customer Get(int id);
        int Add(Customer c);
        int Update(Customer c);
        int Delete(int id);
    }
}

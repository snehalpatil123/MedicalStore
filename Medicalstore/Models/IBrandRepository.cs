namespace Medicalstore.Models
{
    public interface IBrandRepository
    {
        List<Brand> GetAll();
        Brand Get(int id);
        int Add(Brand b);
        int Update(Brand b);
        int Delete(int id);
    }
}

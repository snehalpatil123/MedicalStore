namespace Medicalstore.Models
{
    public interface ISale_detailsRepository
    {
        List<Sale_details> GetAll();
        Sale_details Get(int id);
        int Add(Sale_details s);
        int Update(Sale_details s);
        int Delete(int id);

    }
}

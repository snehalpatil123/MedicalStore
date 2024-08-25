namespace Medicalstore.Models
{
    public interface ISale_masterRepository
    {
        List<Sale_master> GetAll(); 
        Sale_master Get(int id);
        int Add(Sale_master s);
        int Update(Sale_master s);
        int Delete(int id); 
    }
}

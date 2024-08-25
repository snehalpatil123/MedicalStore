namespace Medicalstore.Models
{
    public interface IMedicine_MasterRepository
    {
        List<Medicine_Master> GetAll(); 
        Medicine_Master Get(int id);
        int Add(Medicine_Master m);
        int Update(Medicine_Master m);  
        int Delete(int id); 
    }
}

namespace Medicalstore.Models
{
    public interface IMedicine_CatRepository
    {
        List<Medicine_Cat> GetAll();
        Medicine_Cat Get(int id);
        int Add(Medicine_Cat m);
        int Update(Medicine_Cat m);
        int Delete(int id); 
    }
}
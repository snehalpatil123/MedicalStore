namespace Medicalstore.Models
{
    public interface IReceiptRepository
    {
        List<Receipt> GetAll();
        Receipt Get(int id);
        int Add (Receipt r);
        int Update (Receipt r);
        int Delete (int id);    
    }
}

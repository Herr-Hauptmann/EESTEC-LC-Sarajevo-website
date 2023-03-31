using EESTEC.Models;

namespace EESTEC.Interfaces
{
    public interface ILocalEventRepository
    {
        Task<IEnumerable<LocalEvent>> GetAll();
        Task<LocalEvent> GetById(int id);

        bool Create(LocalEvent localEvent);
        bool Update(LocalEvent localEvent);
        bool Delete(LocalEvent localEvent);
        bool Save();
    }
}

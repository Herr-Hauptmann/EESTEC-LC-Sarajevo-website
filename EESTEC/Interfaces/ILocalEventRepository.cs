using EESTEC.Models;

namespace EESTEC.Interfaces
{
    public interface ILocalEventRepository
    {
        Task<IEnumerable<LocalEvent>> GetAll(string search="");
        Task<LocalEvent> GetById(int id);

        Task<IEnumerable<LocalEvent>> GetMostRecent();
        LocalEvent Create(LocalEvent localEvent);
        bool Update(LocalEvent localEvent);
        bool Delete(LocalEvent localEvent);
        bool Save();
    }
}

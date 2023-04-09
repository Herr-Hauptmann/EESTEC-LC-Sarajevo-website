using EESTEC.Models;

namespace EESTEC.Interfaces
{
    public interface IEventFileRepository
    {
        Task<IEnumerable<EventFile>> GetAll(int eventId);
        Task<LocalEvent> GetById(int id);
        bool Create(IFormFile file);
        bool Update(IFormFile file);
        bool Delete(int id);
        bool Save();
    }
}

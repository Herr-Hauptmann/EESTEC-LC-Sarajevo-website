using EESTEC.Models;

namespace EESTEC.Interfaces
{
    public interface IEventFileRepository
    {
        Task<IEnumerable<EventFile>> GetAll(int eventId);
        Task<LocalEvent> GetById(int id);
        Task<bool> Create(IFormFile file, LocalEvent localEvent);
        bool Update(IFormFile file);
        bool Delete(EventFile file);
        bool Save();
    }
}

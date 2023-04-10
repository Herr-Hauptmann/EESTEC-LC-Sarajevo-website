using EESTEC.Models;

namespace EESTEC.Interfaces
{
    public interface IEventFileRepository
    { 
        Task<EventFile> GetById(int id);
        Task<bool> Create(IFormFile file, LocalEvent localEvent);
        bool Delete(EventFile file);
        bool Save();
    }
}

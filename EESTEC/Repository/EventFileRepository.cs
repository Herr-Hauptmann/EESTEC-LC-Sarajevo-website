using EESTEC.Interfaces;
using EESTEC.Models;

namespace EESTEC.Repository
{
    public class EventFileRepository : IEventFileRepository
    {
        public bool Create(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventFile>> GetAll(int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<LocalEvent> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(IFormFile localEvent)
        {
            throw new NotImplementedException();
        }
    }
}

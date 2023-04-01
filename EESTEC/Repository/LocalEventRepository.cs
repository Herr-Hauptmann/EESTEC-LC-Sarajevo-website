using EESTEC.Data;
using EESTEC.Interfaces;
using EESTEC.Models;
using Microsoft.EntityFrameworkCore;

namespace EESTEC.Repository
{
    public class LocalEventRepository : ILocalEventRepository
    {
        private readonly AppDbContext _context;

        public LocalEventRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<LocalEvent>> GetAll()
        {
            return await _context.LocalEvents.ToListAsync();
        }
        public bool Create(LocalEvent localEvent)
        {
            _context.Add(localEvent);
            return Save();
        }

        public bool Delete(LocalEvent localEvent)
        {
            _context.Remove(localEvent);
            return Save();
        }
        public async Task<LocalEvent> GetById(int id)
        {
            var localEvent = await _context.LocalEvents.FindAsync(id);
            return localEvent;
        }
        public bool Update(LocalEvent localEvent)
        {
            _context.LocalEvents.Update(localEvent);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}

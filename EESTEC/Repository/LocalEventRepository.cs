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
        public async Task<IEnumerable<LocalEvent>> GetAll(string search)
        {
            if (search == null || search.Length == 0)
                return await _context.LocalEvents.OrderByDescending(e=>e.Date).ToListAsync();
            return await _context.LocalEvents.Where(l => l.Title.Contains(search) || l.Description.Contains(search)).OrderByDescending(e => e.Date).ToListAsync();
        }

        public async Task<IEnumerable<LocalEvent>> GetMostRecent()
        {
            return await _context.LocalEvents.OrderByDescending(e => e.Date).Take(6).ToListAsync();
        }
        public LocalEvent Create(LocalEvent localEvent)
        {
            _context.Add(localEvent);
            Save();
            return localEvent;
        }

        public bool Delete(LocalEvent localEvent)
        {
            _context.Remove(localEvent);
            return Save();
        }
        public async Task<LocalEvent> GetById(int id)
        {
            var localEvent = await _context.LocalEvents.Include(localEvent => localEvent.Files).SingleOrDefaultAsync(p => p.Id == id);
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

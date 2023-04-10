using EESTEC.Data;
using EESTEC.Interfaces;
using EESTEC.Models;

namespace EESTEC.Repository
{
    public class EventFileRepository : IEventFileRepository
    {

        private readonly AppDbContext _context;

        public EventFileRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(IFormFile file, LocalEvent localEvent)
        {
            //upload files to wwwroot
            var fileName = Path.GetFileName(file.FileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\EventFiles", fileName);

            //Add file to database
            var eventFile = new EventFile
            {
                Name = fileName,
                Path = filePath,
                LocalEvent = localEvent
            };
            _context.EventFiles.Add(eventFile);
            var result = Save();

            //save file locally
            using (var fileSteam = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileSteam);
            }
            
            return result;
        }

        public bool Delete(EventFile file)
        {
            System.IO.File.Delete(file.Path);
            _context.Remove(file);
            return Save();
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
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(IFormFile localEvent)
        {
            throw new NotImplementedException();
        }
    }
}

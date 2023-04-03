using EESTEC.Data;
using EESTEC.Interfaces;
using EESTEC.Models;
using Microsoft.EntityFrameworkCore;

namespace EESTEC.Repository
{
    public class PartnerRepository : IPartnerRepository
    {
        private readonly AppDbContext _context;
        public PartnerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Partner>> GetAllAsync()
        {
            return await _context.Partners.Include(p => p.PartnerCategory).OrderBy(p => p.PartnerCategory.DisplayOrder).ToListAsync();
        }

        public async Task<Partner> GetByIdAsync(int id)
        {
            var partner = await _context.Partners.Include(p => p.PartnerCategory).SingleOrDefaultAsync(p => p.Id == id);
            return partner;
        }

        public bool Create(Partner partner)
        {
            _context.Add(partner);
            return Save();
        }

        public bool Delete(Partner partner)
        {
            _context.Remove(partner);
            return Save();
        }

        public bool Update(Partner partner)
        {
            _context.Partners.Update(partner);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}

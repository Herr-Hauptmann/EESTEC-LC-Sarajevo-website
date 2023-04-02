using EESTEC.Data;
using EESTEC.Interfaces;
using EESTEC.Models;
using Microsoft.EntityFrameworkCore;

namespace EESTEC.Repository
{
    public class PartnerCategoryRepository : IPartnerCategoryRepository
    {
        private readonly AppDbContext _context;

        public PartnerCategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool Create(PartnerCategory partnerCategory)
        {
            _context.Add(partnerCategory);
            return Save();
        }

        public bool Delete(PartnerCategory partnerCategory)
        {
            _context.Remove(partnerCategory);
            return Save();
        }

        public async Task<IEnumerable<PartnerCategory>> GetAllAsync()
        {
            return await _context.PartnerCategories.Include(c => c.Partners).OrderBy(c => c.DisplayOrder).ToListAsync();
        }

        public async Task<PartnerCategory> GetByIdAsync(int id)
        {
            var partnerCategory = await _context.PartnerCategories.FindAsync(id);
            return partnerCategory;
        }


        public bool Update(PartnerCategory partnerCategory)
        {
            _context.PartnerCategories.Update(partnerCategory);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}

using EESTEC.Models;

namespace EESTEC.Interfaces
{
    public interface IPartnerCategoryRepository
    {
        Task<IEnumerable<PartnerCategory>> GetAllAsync();
        Task<PartnerCategory> GetByIdAsync(int id);
        bool Create(PartnerCategory localEvent);
        bool Update(PartnerCategory localEvent);
        bool Delete(PartnerCategory localEvent);
        bool Save();
    }
}

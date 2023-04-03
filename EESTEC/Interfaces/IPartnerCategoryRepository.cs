using EESTEC.Models;

namespace EESTEC.Interfaces
{
    public interface IPartnerCategoryRepository
    {
        Task<IEnumerable<PartnerCategory>> GetAllAsync();
        Task<PartnerCategory> GetByIdAsync(int id);
        bool Create(PartnerCategory partnerCategory);
        bool Update(PartnerCategory partnerCategory);
        bool Delete(PartnerCategory partnerCategory);
        bool Save();
    }
}

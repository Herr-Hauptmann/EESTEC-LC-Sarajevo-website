using EESTEC.Models;

namespace EESTEC.Interfaces
{
    public interface IPartnerRepository
    {
        Task<IEnumerable<Partner>> GetAllAsync();
        Task<Partner> GetByIdAsync(int id);
        bool Create(Partner partner);
        bool Update(Partner partner);
        bool Delete(Partner partner);
        bool Save();
    }
}

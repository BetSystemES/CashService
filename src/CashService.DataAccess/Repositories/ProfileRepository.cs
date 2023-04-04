using CashService.BusinessLogic.Contracts.Repositories;
using CashService.BusinessLogic.Entities;

namespace CashService.DataAccess.Repositories
{
    public class ProfileRepository : SqlRepository<ProfileEntity>, IProfileRepository
    {
        public ProfileRepository(CashDbContext context) : base(context)
        {
        }
    }
}
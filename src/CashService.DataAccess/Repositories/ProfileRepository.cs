using CashService.BusinessLogic.Contracts.Repositories;
using CashService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashService.DataAccess.Repositories
{
    public class ProfileRepository : SqlRepository<ProfileEntity>, IProfileRepository
    {

        public ProfileRepository(DbSet<ProfileEntity> entities) : base(entities)
        {
        }
    }
}
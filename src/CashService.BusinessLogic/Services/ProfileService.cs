using CashService.BusinessLogic.Contracts;
using CashService.BusinessLogic.Contracts.Repositories;
using CashService.BusinessLogic.Contracts.Services;
using CashService.BusinessLogic.Entities;

namespace CashService.BusinessLogic.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IDataContext _context;

        public ProfileService(IProfileRepository profileRepository, IDataContext context)
        {
            _profileRepository = profileRepository;
            _context = context;
        }

        public async Task CreateProfile(Guid userId, CancellationToken token)
        {
            await _profileRepository.Add(new ProfileEntity() { Id = userId }, token);
            await _context.SaveChanges(token);

        }
    }
}

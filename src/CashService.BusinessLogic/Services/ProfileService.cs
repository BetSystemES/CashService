using CashService.BusinessLogic.Contracts.Repositories;
using CashService.BusinessLogic.Contracts.Services;
using CashService.BusinessLogic.Entities;

namespace CashService.BusinessLogic.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task CreateProfile(Guid userId, CancellationToken token)
        {
            await _profileRepository.Add(new ProfileEntity() { Id = userId }, token);
        }
    }
}

using CashService.BusinessLogic.Contracts;
using CashService.BusinessLogic.Contracts.Providers;
using CashService.BusinessLogic.Contracts.Repositories;
using CashService.BusinessLogic.Contracts.Services;
using CashService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CashService.BusinessLogic.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileProvider _profileProvider;
        private readonly IProfileRepository _profileRepository;
        private readonly IDataContext _context;

        public ProfileService(
            IProfileProvider profileProvider,
            IProfileRepository profileRepository,
            IDataContext context)
        {
            _profileProvider = profileProvider;
            _profileRepository = profileRepository;
            _context = context;
        }

        public EntityEntry Attach(ProfileEntity entity)
        {
           return _profileRepository.Attach(entity);
        }

        public async Task Create(Guid userId, CancellationToken token)
        {
            await _profileRepository.Add(new ProfileEntity() { Id = userId }, token);
            await _context.SaveChanges(token);
        }

        public void Detach(ProfileEntity entity)
        {
            _profileRepository.Detach(entity);
        }

        public EntityEntry Entry(ProfileEntity entity)
        {
            return _profileRepository.Entry(entity);
        }

        public async Task<ProfileEntity> Get(Guid userId, CancellationToken token)
        {
            var profile = await _profileProvider.Get(userId, token);

            return profile;
        }

        public async Task Update(ProfileEntity entity, CancellationToken token)
        {
            await _profileRepository.Update(entity, token);
            await _context.SaveChanges(token);
        }
    }
}
using System;
using System.Threading.Tasks;
using Actio.Common.Domain.Models;
using Actio.Common.Domain.Repositories;
using Actio.Common.Exceptions;

namespace Actio.Services.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ActivityService(IActivityRepository repository, ICategoryRepository categoryRepository)
        {
            _activityRepository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task AddAsync(Guid id, Guid userId, string category, string name, string description, DateTime createdAt)
        {
            var activityCategory = _categoryRepository.GetAsync(name);
            if (activityCategory == null)
            {
                throw new ActioException("Category not found");
            }
            
            await _activityRepository.AddAsync(new Activity(id, userId, name, category, description, createdAt));
        }
    }
}
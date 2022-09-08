using Edge.Bills.Data.Context;
using Edge.Bills.Domain.Entities;
using Edge.Bills.Domain.Interfaces;
using Edge.Bills.Shared.ViewModels.User;
using Microsoft.EntityFrameworkCore;

namespace Edge.Bills.Data.Repositories
{
    public class UserRepository : BaseEntityRepositoryClass<User, Guid>, IUserRepository
    {
        public UserRepository(BillsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> Add(UserFullViewModel user)
        {
            user.Id = Guid.NewGuid();
            var entity = GenerateEntity(user);
            return await AddEntity(entity);
        }

        public async Task<UserFullViewModel?> GetByEmail(string email)
        {
            var entity = await GetSet().FirstOrDefaultAsync(x => x.Email == email);
            return GenerateUserFullViewModel(entity);
        }

        public async Task<UserFullViewModel?> GetById(Guid id)
        {
            var entity = await base.GetEntityById(id);
            return GenerateUserFullViewModel(entity);
        }

        public async Task<IEnumerable<UserListViewModel>> GetList()
        {
            var entities = await base.GetQueryable();
            return entities.Any() ? entities.Select(x => new UserListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                UserType = x.UserType
            }).OrderBy(x => x.Name) : new List<UserListViewModel>();
        }

        public async Task<int> Update(UserFullViewModel user)
        {
            var entity = GenerateEntity(user);
            return await UpdateEntity(entity);
        }

        private User GenerateEntity(UserFullViewModel user) => new User
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            UserType = user.UserType
        };

        private UserFullViewModel? GenerateUserFullViewModel(User? user) => user == null ? null : new UserFullViewModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            UserType = user.UserType
        };
    }
}

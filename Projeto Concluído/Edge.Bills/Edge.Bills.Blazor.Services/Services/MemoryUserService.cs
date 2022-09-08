using Edge.Bills.Blazor.Services.Helpers;
using Edge.Bills.Blazor.Services.Interfaces;
using Edge.Bills.Shared.Enums;
using Edge.Bills.Shared.ViewModels.User;

namespace Edge.Bills.Blazor.Services.Services
{
    public class MemoryUserService : IUserService
    {
        private List<UserFullViewModel> _userList;

        public MemoryUserService()
        {
            _userList = new List<UserFullViewModel>
            {
                new UserFullViewModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Master",
                    Email = "master@master.com",
                    Password = "123456",
                    UserType = EUserType.Administrator
                },
                new UserFullViewModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Visualizer",
                    Email = "visu@visu.com",
                    Password = "123456",
                    UserType = EUserType.Visualizer
                }
            };
        }

        public Task<bool> Add(UserFullViewModel user)
        {
            _userList.Add(user);
            return Task.FromResult(true);
        }

        public Task<bool> Delete(Guid id)
        {
            var userOnList = _userList.FirstOrDefault(x => x.Id == id);
            if (userOnList != null)
            {
                _userList.Remove(userOnList);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<UserFullViewModel?> GetByEmail(string email)
        {
            var userOnList = _userList.FirstOrDefault(x => x.Email == email);
            var user = userOnList == null ? null : Helper.CreateDeepCopy(userOnList);
            return Task.FromResult(user);
        }

        public Task<UserFullViewModel?> GetById(Guid id)
        {
            var userOnList = _userList.FirstOrDefault(x => x.Id == id);
            var user = userOnList == null ? null : Helper.CreateDeepCopy(userOnList);
            return Task.FromResult(user);
        }

        public Task<IEnumerable<UserListViewModel>> GetList()
        {
            var list = _userList.Select(x => new UserListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                UserType = x.UserType
            });
            return Task.FromResult(list);
        }

        public Task<bool> Update(UserFullViewModel user)
        {
            var userOnList = _userList.FirstOrDefault(x => x.Id == user.Id);
            if (userOnList != null)
            {
                userOnList.Name = user.Name;
                userOnList.Email = user.Email;
                if (!string.IsNullOrWhiteSpace(user.Password))
                {
                    userOnList.Password = user.Password;
                }
                userOnList.UserType = user.UserType;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}

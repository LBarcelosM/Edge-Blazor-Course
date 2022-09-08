using Edge.Bills.Shared.ViewModels.User;

namespace Edge.Bills.Blazor.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> Add(UserFullViewModel user);
        Task<bool> Delete(Guid id);
        Task<UserFullViewModel?> GetByEmail(string email);
        Task<UserFullViewModel?> GetById(Guid id);
        Task<IEnumerable<UserListViewModel>> GetList();
        Task<bool> Update(UserFullViewModel user);
    }
}

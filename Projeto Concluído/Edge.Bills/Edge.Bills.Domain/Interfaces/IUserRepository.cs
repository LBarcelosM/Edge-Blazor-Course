using Edge.Bills.Shared.ViewModels.User;

namespace Edge.Bills.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<int> Add(UserFullViewModel user);
        Task<int> Delete(Guid id);
        Task<UserFullViewModel?> GetByEmail(string email);
        Task<UserFullViewModel?> GetById(Guid id);
        Task<IEnumerable<UserListViewModel>> GetList();
        Task<int> Update(UserFullViewModel user);
    }
}

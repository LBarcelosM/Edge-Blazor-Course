using Edge.Bills.Shared.ViewModels.Bill;

namespace Edge.Bills.Domain.Interfaces
{
    public interface IBillRepository
    {
        Task<int> Add(BillFullViewModel user);
        Task<int> Delete(long id);
        Task<BillFullViewModel?> GetById(long id);
        Task<IEnumerable<BillListViewModel>> GetByUserId(Guid userId);
        Task<IEnumerable<BillListViewModel>> GetList();
        Task<int> Update(BillFullViewModel user);
    }
}

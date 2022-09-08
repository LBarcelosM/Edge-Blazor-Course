using Edge.Bills.Shared.ViewModels.Bill;

namespace Edge.Bills.Blazor.Services.Interfaces
{
    public interface IBillService
    {
        Task<bool> Add(BillFullViewModel user);
        Task<bool> Delete(long id);
        Task<BillFullViewModel?> GetById(long id);
        Task<IEnumerable<BillListViewModel>> GetByUserId(Guid userId);
        Task<IEnumerable<BillListViewModel>> GetList();
        Task<bool> Update(BillFullViewModel user);
    }
}

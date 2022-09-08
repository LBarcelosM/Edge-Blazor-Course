using Edge.Bills.Data.Context;
using Edge.Bills.Domain.Entities;
using Edge.Bills.Domain.Interfaces;
using Edge.Bills.Shared.ViewModels.Bill;

namespace Edge.Bills.Data.Repositories
{
    public class BillRepository : BaseEntityRepositoryClass<Bill, long>, IBillRepository
    {
        public BillRepository(BillsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> Add(BillFullViewModel bill)
        {
            bill.Id = default;
            var entity = GenerateEntity(bill);
            return await AddEntity(entity);
        }

        public async Task<BillFullViewModel?> GetById(long id)
        {
            var entity = await base.GetEntityById(id);
            return entity == null ? null : new BillFullViewModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Name = entity.Name,
                Value = entity.Value,
                Status = entity.Status

            };
        }

        public async Task<IEnumerable<BillListViewModel>> GetByUserId(Guid userId)
        {
            var query = await GetQueryable("User");
            return query.Any() ? query.Where(x => x.UserId == userId).Select(x => GenerateBillListViewModel(x)).OrderBy(x => x.Name) : new List<BillListViewModel>();
        }

        public async Task<IEnumerable<BillListViewModel>> GetList()
        {
            var query = await GetQueryable("User");
            return query.Any() ? query.Select(x => GenerateBillListViewModel(x)).OrderBy(x => x.UserName).ThenBy(x => x.Name) : new List<BillListViewModel>();
        }

        public async Task<int> Update(BillFullViewModel bill)
        {
            var entity = GenerateEntity(bill);
            return await UpdateEntity(entity);
        }

        private Bill GenerateEntity(BillFullViewModel bill) => new Bill
        {
            Id = bill.Id,
            UserId = bill.UserId,
            Name = bill.Name,
            Value = bill.Value,
            Status = bill.Status
        };

        private BillListViewModel GenerateBillListViewModel(Bill bill) => new BillListViewModel
        {
            Id = bill.Id,
            Name = bill.Name,
            Value = bill.Value,
            Status = bill.Status,
            UserName = bill.User.Name
        };
    }
}

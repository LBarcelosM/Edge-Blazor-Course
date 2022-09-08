using Edge.Bills.Blazor.Services.Interfaces;
using Edge.Bills.Shared.ViewModels.Bill;
using Edge.Bills.Shared.ViewModels.Request;

namespace Edge.Bills.Blazor.Services.Services
{
    public class ApiBillService : BaseApiService, IBillService
    {
        public ApiBillService(HttpClient httpClient) : base("bills", httpClient)
        {
        }

        public async Task<bool> Add(BillFullViewModel user)
        {
            var result = await Post<BillFullViewModel, RequestResult<int>>(ServiceUrl(), user);
            return result != null && result.Success;
        }

        public async Task<bool> Delete(long id)
        {
            var url = GetRouteParamsUrl(ServiceUrl("#ID"), new Tuple<string, string>("#ID", id.ToString()));
            var result = await Delete<RequestResult<int>>(url);
            return result != null && result.Success;
        }

        public async Task<BillFullViewModel?> GetById(long id)
        {
            var url = GetRouteParamsUrl(ServiceUrl("#ID"), new Tuple<string, string>("#ID", id.ToString()));
            var result = await Get<RequestResult<BillFullViewModel?>>(url);
            if (result != null && result.Success)
            {
                return result.Data;
            }
            return null;
        }

        public async Task<IEnumerable<BillListViewModel>> GetByUserId(Guid userId)
        {
            var url = GetRouteParamsUrl(ServiceUrl("user/#ID"), new Tuple<string, string>("#ID", userId.ToString()));      
            var result = await Get<RequestResult<IEnumerable<BillListViewModel>?>>(url);
            if (result != null && result.Success)
            {
                return result.Data;
            }
            return new List<BillListViewModel>();
        }

        public async Task<IEnumerable<BillListViewModel>> GetList()
        {
            var url = ServiceUrl();
            var result = await Get<RequestResult<IEnumerable<BillListViewModel>>>(ServiceUrl());
            if (result != null && result.Success)
            {
                return result.Data;
            }
            return new List<BillListViewModel>();
        }

        public async Task<bool> Update(BillFullViewModel user)
        {
            var result = await Put<BillFullViewModel, RequestResult<int>>(ServiceUrl(), user);
            return result != null && result.Success;
        }
    }
}
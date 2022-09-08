using Edge.Bills.Blazor.Services.Interfaces;
using Edge.Bills.Shared.ViewModels.Request;
using Edge.Bills.Shared.ViewModels.User;

namespace Edge.Bills.Blazor.Services.Services
{
    public class ApiUserService : BaseApiService, IUserService
    {
        public ApiUserService(HttpClient httpClient) : base("users", httpClient)
        {
        }

        public async Task<bool> Add(UserFullViewModel user)
        {
            var result = await Post<UserFullViewModel, RequestResult<int>>(ServiceUrl(), user);
            return result != null && result.Success;
        }

        public async Task<bool> Delete(Guid id)
        {
            var url = GetRouteParamsUrl(ServiceUrl("#ID"), new Tuple<string, string>("#ID", id.ToString()));
            var result = await Delete<RequestResult<int>>(url);
            return result != null && result.Success;
        }

        public Task<UserFullViewModel?> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<UserFullViewModel?> GetById(Guid id)
        {
            var url = GetRouteParamsUrl(ServiceUrl("#ID"), new Tuple<string, string>("#ID", id.ToString()));
            var result = await Get<RequestResult<UserFullViewModel?>>(url);
            if (result != null && result.Success)
            {
                return result.Data;
            }
            return null;
        }

        public async Task<IEnumerable<UserListViewModel>> GetList()
        {
            var result = await Get<RequestResult<IEnumerable<UserListViewModel>>>(ServiceUrl());
            if(result != null && result.Success)
            {
                return result.Data;
            }
            return new List<UserListViewModel>();
        }

        public async Task<bool> Update(UserFullViewModel user)
        {
            var result = await Put<UserFullViewModel, RequestResult<int>>(ServiceUrl(), user);
            return result != null && result.Success;
        }
    }
}

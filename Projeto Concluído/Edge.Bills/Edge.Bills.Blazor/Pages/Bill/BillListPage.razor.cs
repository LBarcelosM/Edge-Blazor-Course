using Edge.Bills.Blazor.Services.Interfaces;
using Edge.Bills.Shared.ViewModels.Bill;
using Edge.Bills.Shared.ViewModels.User;
using Microsoft.AspNetCore.Components;

namespace Edge.Bills.Blazor.Pages.Bill
{
    partial class BillListPage
    {
        [Parameter] public Guid? user_id { get; set; }
        [Inject] public IBillService BillService { get; set; }
        [Inject] public IUserService UserService { get; set; }

        private bool _billListLoaded = false;
        private bool _userListLoaded = false;
        private IEnumerable<BillListViewModel> _billList = new List<BillListViewModel>();
        private IEnumerable<UserListViewModel> _userList = new List<UserListViewModel>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadBillList();
                await LoadUserList();
            }
        }

        private async Task LoadBillList()
        {
            _billList = user_id.HasValue ? await BillService.GetByUserId(user_id.Value) : await BillService.GetList();
            _billListLoaded = true;
            StateHasChanged();
        }

        private async Task LoadUserList()
        {
            _userList = await UserService.GetList();
            _userListLoaded = true;
            StateHasChanged();
        }

        private async Task OnChangeSelectedUser(ChangeEventArgs e)
        {
            var emptyId = Guid.Empty.ToString();
            Guid? id = e.Value.ToString() == emptyId ? null : Guid.Parse(e.Value.ToString());
            if (user_id != id)
            {
                user_id = id;
                _billListLoaded = false;
                await LoadBillList();
            }
        }

        private void New()
        {
            var url = user_id.HasValue ? $"/bill/user/{user_id}" : "/bill";
            NavigationManager.NavigateTo(url);
        }

        private void EditBill(long id)
        {
            NavigationManager.NavigateTo($"bill/{id}");
        }
    }
}

using Edge.Bills.Blazor.Services.Interfaces;
using Edge.Bills.Shared.Enums;
using Edge.Bills.Shared.ViewModels.Bill;
using Edge.Bills.Shared.ViewModels.User;
using Microsoft.AspNetCore.Components;

namespace Edge.Bills.Blazor.Pages.Bill
{
    partial class BillDetailsPage
    {
        [Parameter] public long? id { get; set; }
        [Parameter] public Guid? user_id { get; set; }
        [Inject] public IBillService BillService { get; set; }
        [Inject] public IUserService UserService { get; set; }

        private bool _userListLoaded = false;
        private BillFullViewModel _viewModel = new BillFullViewModel();
        private IEnumerable<UserListViewModel> _userList = new List<UserListViewModel>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if(user_id != null)
                {
                    _viewModel.UserId = user_id.Value;
                }
                if (id.HasValue)
                {
                    LoadingService.Show();
                    var bill = await BillService.GetById(id.Value);
                    if (bill != null)
                    {
                        _viewModel = bill;
                        LoadingService.Hide();
                        StateHasChanged();
                    }
                    else
                    {
                        id = null;
                    }
                }
                await LoadUserList();
            }
        }

        private async Task LoadUserList()
        {
            _userList = await UserService.GetList();
            _userListLoaded = true;
            StateHasChanged();
        }

        private void OnChangeStatus(ChangeEventArgs e)
        {
            _viewModel.Status = (EBillStatus)Enum.Parse(typeof(EBillStatus), e.Value.ToString());
        }

        private async Task OnChangeSelectedUser(ChangeEventArgs e)
        {
            _viewModel.UserId = Guid.Parse(e.Value.ToString());
        }

        private void OnCancel()
        {
            ReturnToList();
        }

        private async Task OnSave()
        {
            var success = false;
            if (id.HasValue)
            {
                success = await BillService.Update(_viewModel);
            }
            else
            {
                success = await BillService.Add(_viewModel);
            }
            if (success)
            {
                ToastService.AddSuccess("Dados salvos com sucesso!");
                ReturnToList();
            }
            else
            {
                ToastService.AddError("Erro ao salvar dados!");
            }
        }

        private void ReturnToList()
        {
            var url = user_id.HasValue ? $"/bill_list/user/{user_id}" : "/bill_list";
            NavigationManager.NavigateTo(url);
        }
    }
}

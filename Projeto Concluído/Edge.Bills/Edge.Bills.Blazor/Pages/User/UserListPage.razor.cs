using Edge.Bills.Blazor.CustomComponents.Components;
using Edge.Bills.Blazor.Services.Interfaces;
using Edge.Bills.Shared.ViewModels.User;
using Microsoft.AspNetCore.Components;

namespace Edge.Bills.Blazor.Pages.User
{
    partial class UserListPage
    {
        [Inject] public IUserService UserService { get; set; }

        private bool _loading = true;
        private IEnumerable<UserListViewModel> _userList = new List<UserListViewModel>();
        private ConfirmModalComponent _confirmDeleteUser;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadUserList();
            }
        }

        private async Task LoadUserList()
        {
            _userList = await UserService.GetList();
            _loading = false;
            StateHasChanged();
        }

        private void MyBills(Guid id)
        {
            NavigationManager.NavigateTo($"bill_list/user/{id}");
        }

        private void EditUser(Guid id)
        {
            NavigationManager.NavigateTo($"user/{id}");
        }

        private async Task DeleteUser(Guid id)
        {
            var success = await UserService.Delete(id);
            if (success)
            {
                await LoadUserList();
                ToastService.AddSuccess("Usuário excluído com sucesso!");
            }
            else
            {
                ToastService.AddError("Erro ao excluir usuário!");
            }
        }

        private void ConfirmDeleteUser(Guid id)
        {
            _confirmDeleteUser.Show(async () => await DeleteUser(id));
        }
    }
}

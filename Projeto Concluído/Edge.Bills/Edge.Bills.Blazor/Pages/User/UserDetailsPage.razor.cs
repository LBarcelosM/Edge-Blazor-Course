using Edge.Bills.Blazor.Services.Interfaces;
using Edge.Bills.Shared.Enums;
using Edge.Bills.Shared.ViewModels.User;
using Microsoft.AspNetCore.Components;

namespace Edge.Bills.Blazor.Pages.User
{
    partial class UserDetailsPage
    {
        [Parameter] public Guid? id { get; set; }
        [Inject] public IUserService UserService { get; set; }

        private UserFullViewModel _viewModel = new UserFullViewModel();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (id.HasValue)
                {
                    LoadingService.Show();
                    var user = await UserService.GetById(id.Value);
                    if (user != null)
                    {
                        _viewModel = user;
                        LoadingService.Hide();
                        StateHasChanged();
                    }
                    else
                    {
                        id = null;
                    }
                }
            }
        }

        private void OnChangeUserType(ChangeEventArgs e)
        {
            _viewModel.UserType = (EUserType)Enum.Parse(typeof(EUserType), e.Value.ToString());
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
                success = await UserService.Update(_viewModel);
            }
            else
            {
                success = await UserService.Add(_viewModel);
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

        private void ReturnToList() => NavigationManager.NavigateTo("user_list");
    }
}

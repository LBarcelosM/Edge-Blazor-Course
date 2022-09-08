using Edge.Bills.Blazor.CustomComponents.Interfaces;
using Edge.Bills.Blazor.CustomComponents.Models;
using Microsoft.AspNetCore.Components;

namespace Edge.Bills.Blazor.CustomComponents.Components
{
    partial class ToastContainerComponent
    {
        [Inject] public IToastService ToastService { get; set; }
        private List<ToastViewModel> _toastList = new List<ToastViewModel>();

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                ToastService.RegisterOnAddToast(AddToast);
            }
            return base.OnAfterRenderAsync(firstRender);
        }

        private void AddToast(ToastViewModel viewModel)
        {
            _toastList.Add(viewModel);
            StateHasChanged();
        }

        private void RemoveToast(ToastViewModel viewModel)
        {
            if (_toastList.Contains(viewModel))
            {
                _toastList.Remove(viewModel);
                StateHasChanged();
            }
        }
    }
}

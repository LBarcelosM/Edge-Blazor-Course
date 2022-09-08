using Edge.Bills.Blazor.CustomComponents.Interfaces;
using Edge.Bills.Blazor.CustomComponents.Models;
using Microsoft.AspNetCore.Components;

namespace Edge.Bills.Blazor.CustomComponents.Components
{
    partial class ToastComponent
    {
        [Parameter] public ToastViewModel ViewModel { get; set; }
        [Parameter] public Action<ToastViewModel> OnRemove { get; set; }
        [Inject] public IToastService ToastService { get; set; }
        private int DELAY = 3000;

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                Task.Delay(DELAY).ContinueWith(_ =>
                {
                    OnRemove(ViewModel);
                });
            }
        }

        private string BackgroundClass()
        {
            switch (ViewModel.ToastType)
            {
                case Enums.EToastType.Error:
                    {
                        return "bg-danger";
                    }
                case Enums.EToastType.Success:
                    {
                        return "bg-success";
                    }
                case Enums.EToastType.Warning:
                    {
                        return "bg-warning";
                    }
                default: return "";
            }
        }
    }
}

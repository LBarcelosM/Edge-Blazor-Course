using Edge.Bills.Blazor.CustomComponents.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Edge.Bills.Blazor.CustomComponents.Components
{
    partial class LoaderComponent
    {
        [Inject] public ILoadingService LoadingService { get; set; }

        private bool _show = false;

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                LoadingService.RegisterChange(StateChange);
            }
            base.OnAfterRender(firstRender);
        }

        private void StateChange(bool value)
        {
            _show = value;
            StateHasChanged();
        }
    }
}
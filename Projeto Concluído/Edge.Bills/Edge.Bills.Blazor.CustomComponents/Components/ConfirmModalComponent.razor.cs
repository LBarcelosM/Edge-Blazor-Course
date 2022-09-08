using Microsoft.AspNetCore.Components;

namespace Edge.Bills.Blazor.CustomComponents.Components
{
    partial class ConfirmModalComponent
    {
        [Parameter] public bool IsShowing { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public string Message { get; set; }
        private Action _confirmAction;

        private string _modalClass => IsShowing ? "d-block" : "d-none";

        public void Show(Action confirmAction)
        {
            _confirmAction = confirmAction;
            IsShowing = true;
            StateHasChanged();
        }

        public void Hide()
        {
            IsShowing = false;
            StateHasChanged();
        }

        private void OnConfirm()
        {
            _confirmAction.Invoke();
            Hide();
        }
    }
}

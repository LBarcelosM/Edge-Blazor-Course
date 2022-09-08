using Edge.Bills.Blazor.CustomComponents.Enums;
using Edge.Bills.Blazor.CustomComponents.Interfaces;
using Edge.Bills.Blazor.CustomComponents.Models;

namespace Edge.Bills.Blazor.CustomComponents.Services
{
    public class ToastService : IToastService
    {
        private event Action<ToastViewModel> _onAddToast;

        public void AddError(string message)
        {
            var toast = GenerateToast("Erro", message, EToastType.Error);
            _onAddToast.Invoke(toast);
        }

        public void AddSuccess(string message)
        {
            var toast = GenerateToast("Sucesso", message, EToastType.Success);
            _onAddToast.Invoke(toast);
        }

        public void AddWarning(string message)
        {
            var toast = GenerateToast("Alerta", message, EToastType.Warning);
            _onAddToast.Invoke(toast);
        }

        public void RegisterOnAddToast(Action<ToastViewModel> onAddToastAction)
        {
            _onAddToast += onAddToastAction;
        }

        private ToastViewModel GenerateToast(string title, string message, EToastType type) => new ToastViewModel
        {
            Title = title,
            Message = message,
            ToastType = type
        };
    }
}

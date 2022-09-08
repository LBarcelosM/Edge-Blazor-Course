using Edge.Bills.Blazor.CustomComponents.Models;

namespace Edge.Bills.Blazor.CustomComponents.Interfaces
{
    public interface IToastService
    {
        void AddError(string message);
        void AddSuccess(string message);
        void AddWarning(string message);
        void RegisterOnAddToast(Action<ToastViewModel> onAddToastAction);
    }
}

namespace Edge.Bills.Blazor.CustomComponents.Interfaces
{
    public interface ILoadingService
    {
        bool IsShowed { get; }
        void Show();
        void Hide();
        void RegisterChange(Action<bool> action);
    }
}

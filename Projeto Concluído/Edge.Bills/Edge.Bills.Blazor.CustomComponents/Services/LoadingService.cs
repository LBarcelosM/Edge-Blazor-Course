using Edge.Bills.Blazor.CustomComponents.Interfaces;

namespace Edge.Bills.Blazor.CustomComponents.Services
{
    public class LoadingService : ILoadingService
    {
        public bool IsShowed { get; private set; }
        private event Action<bool> _onChange;

        public void Hide()
        {
            Change(false);
        }

        public void RegisterChange(Action<bool> action)
        {
            _onChange += action;
        }

        public void Show()
        {
            Change(true);
        }

        private void Change(bool value)
        {
            IsShowed = value;
            _onChange?.Invoke(IsShowed);
        }
    }
}

using Edge.Bills.Blazor.CustomComponents.Enums;

namespace Edge.Bills.Blazor.CustomComponents.Models
{
    public class ToastViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public EToastType ToastType { get; set; }
    }
}

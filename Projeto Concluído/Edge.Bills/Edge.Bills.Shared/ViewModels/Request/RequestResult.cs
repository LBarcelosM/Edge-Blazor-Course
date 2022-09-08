namespace Edge.Bills.Shared.ViewModels.Request
{
    public class RequestResult<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
    }
}

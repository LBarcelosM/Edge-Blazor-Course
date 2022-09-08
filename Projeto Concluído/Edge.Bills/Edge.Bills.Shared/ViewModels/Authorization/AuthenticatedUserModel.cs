namespace Edge.Bills.Shared.ViewModels.Authorization
{
    public class AuthenticatedUserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Token { get; set; }
    }
}

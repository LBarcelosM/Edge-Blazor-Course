using Edge.Bills.Shared.Enums;

namespace Edge.Bills.Shared.ViewModels.User
{
    public class UserFullViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EUserType UserType { get; set; }
    }
}

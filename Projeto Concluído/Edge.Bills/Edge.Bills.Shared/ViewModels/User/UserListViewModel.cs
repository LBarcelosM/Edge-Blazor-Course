using Edge.Bills.Shared.Enums;

namespace Edge.Bills.Shared.ViewModels.User
{
    public class UserListViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public EUserType UserType { get; set; }
    }
}

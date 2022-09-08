using Edge.Bills.Shared.Enums;

namespace Edge.Bills.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EUserType UserType { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
    }
}

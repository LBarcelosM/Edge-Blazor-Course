using Edge.Bills.Shared.Enums;

namespace Edge.Bills.Domain.Entities
{
    public class Bill
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public EBillStatus Status { get; set; }
        public virtual User User { get; set; }
    }
}

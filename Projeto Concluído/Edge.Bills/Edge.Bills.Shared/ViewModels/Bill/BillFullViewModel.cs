using Edge.Bills.Shared.Enums;

namespace Edge.Bills.Shared.ViewModels.Bill
{
    public class BillFullViewModel
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public EBillStatus Status { get; set; }
    }
}

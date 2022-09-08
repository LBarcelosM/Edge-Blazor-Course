using Edge.Bills.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace Edge.Bills.Blazor.Components.Bill
{
    partial class BillStatusComponent
    {
        [Parameter] public EBillStatus Status { get; set; }
    }
}

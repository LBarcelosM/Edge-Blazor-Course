using Edge.Bills.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace Edge.Bills.Blazor.Components.User
{
    partial class UserTypeComponent
    {
        [Parameter] public EUserType UserType { get; set; }
    }
}

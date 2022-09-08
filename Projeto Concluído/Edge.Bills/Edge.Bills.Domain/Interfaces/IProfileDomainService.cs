using Edge.Bills.Security.Enums;
using Edge.Bills.Shared.Enums;

namespace Edge.Bills.Domain.Interfaces
{
    public interface IProfileDomainService
    {
        IEnumerable<EPermissionType> GetPermissions(EUserType userType);
    }
}

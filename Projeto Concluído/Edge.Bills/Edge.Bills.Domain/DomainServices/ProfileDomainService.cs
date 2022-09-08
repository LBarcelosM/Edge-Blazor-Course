using Edge.Bills.Domain.Interfaces;
using Edge.Bills.Security.Enums;
using Edge.Bills.Shared.Enums;

namespace Edge.Bills.Domain.DomainServices
{
    public class ProfileDomainService : IProfileDomainService
    {
        public IEnumerable<EPermissionType> GetPermissions(EUserType userType)
        {
            switch (userType)
            {
                case EUserType.Administrator:
                    {
                        return new List<EPermissionType> {
                            EPermissionType.MANAGE_USER,
                            EPermissionType.MANAGE_BILL,
                            EPermissionType.VIEW_USERS,
                            EPermissionType.VIEW_BILLS
                        };
                    }
                case EUserType.Visualizer:
                    {
                        return new List<EPermissionType> {
                            EPermissionType.VIEW_USERS,
                            EPermissionType.VIEW_BILLS
                        };
                    }
                default:
                    {
                        return new List<EPermissionType>();
                    }
            }
        }
    }
}

using Edge.Bills.Security.Enums;

namespace Edge.Bills.Shared.Helpers
{
    public static class BillsPoliciesHelper
    {
        public static class POLICIES
        {
            public const string MANAGE_BILL = "MANAGE_BILL";
            public const string MANAGE_USER = "MANAGE_USER";
            public const string VIEW_BILLS = "VIEW_BILLS";
            public const string VIEW_USERS = "VIEW_USERS";
        }

        public static List<Tuple<string, string[]>> GetPolicies()
        {
            var list = new List<Tuple<string, string[]>>();
            AddPolicy(POLICIES.MANAGE_BILL, list);
            AddPolicy(POLICIES.MANAGE_USER, list);
            AddPolicy(POLICIES.VIEW_BILLS, list);
            AddPolicy(POLICIES.VIEW_USERS, list);
            return list;
        }

        private static void AddPolicy(string name, List<Tuple<string, string[]>> list)
        {
            var policy = CreatePolicy(name);
            if (policy != null)
            {
                list.Add(policy);
            }
        }

        private static Tuple<string, string[]>? CreatePolicy(string name)
        {
            var tuple = default(Tuple<string, string[]>?);
            switch (name)
            {
                case POLICIES.MANAGE_BILL:
                    {
                        tuple = new Tuple<string, string[]>(POLICIES.MANAGE_BILL, new string[] { EPermissionType.MANAGE_BILL.ToString() });
                        break;
                    }
                case POLICIES.MANAGE_USER:
                    {
                        tuple = new Tuple<string, string[]>(POLICIES.MANAGE_USER, new string[] { EPermissionType.MANAGE_USER.ToString() });
                        break;
                    }
                case POLICIES.VIEW_BILLS:
                    {
                        tuple = new Tuple<string, string[]>(POLICIES.VIEW_BILLS, new string[] { EPermissionType.VIEW_BILLS.ToString() });
                        break;
                    }
                case POLICIES.VIEW_USERS:
                    {
                        tuple = new Tuple<string, string[]>(POLICIES.VIEW_USERS, new string[] { EPermissionType.VIEW_USERS.ToString() });
                        break;
                    }
            }
            return tuple;
        }
    }
}
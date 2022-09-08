using System.Text;

namespace Edge.Bills.WebApi.Security
{
    public static class SecuritySettings
    {
        public static string Secret = "5986804059a641b8b6f89fab6e852233";
        public static byte[] SecretBytes => Encoding.ASCII.GetBytes(Secret);
    }
}

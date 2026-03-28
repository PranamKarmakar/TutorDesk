using System.Security.Cryptography;
using System.Text;

namespace TutorDesk.Common.Helpers
{
    public static class HashHelper
    {
        public static string Hash(string password)
        {
            return Convert.ToBase64String(
                SHA256.HashData(Encoding.UTF8.GetBytes(password)));
        }
    }
}

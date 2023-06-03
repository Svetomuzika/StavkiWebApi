using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Stavki.Infrastructure.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "StavkiServer"; // издатель токена
        public const string AUDIENCE = "StavkiClient"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 15; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}

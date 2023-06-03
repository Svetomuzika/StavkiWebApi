using Stavki.Data.Data;
using Stavki.Infrastructure.EF.Domains;

namespace Stavki.Infrastructure.Services.Interfaces
{
    public interface IAuthService
    {
        UserInfo SignIn(ShortUserInfo userData);
        UserInfo SignUp(UserInfo userData);
        object ResetToken(ShortUserInfo user);
        List<object> GetAllResponsibles();
    }
}

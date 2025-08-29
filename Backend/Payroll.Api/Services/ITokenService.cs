using Microsoft.AspNetCore.Identity;

namespace Payroll.Api.Services;
public interface ITokenService
{
    string CreateToken(IdentityUser user, IEnumerable<string> roles);
}

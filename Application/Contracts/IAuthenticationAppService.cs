using Application.Dtos.Authentication;
using Pokemon.Core.Results;

namespace Application.Contracts;

public interface IAuthenticationAppService
{
    public Task<Result<string>> Register(RegisterDto dto);
    public Task<Result<string>> Login(LoginDto dto);
}

using Application.Contracts;
using Application.Dtos.Authentication;
using Application.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pokemon.Core.Results;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Services.Authentication;

public class AuthenticationAppService : IAuthenticationAppService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtConfiguration _jwtConfiguration;

    public AuthenticationAppService(UserManager<IdentityUser> userManager, IOptions<JwtConfiguration> jwtConfiguration)
    {
        _userManager = userManager;
        _jwtConfiguration = jwtConfiguration.Value;
    }

    public async Task<Result<string>> Login(LoginDto dto)
    {
        var userExist = await _userManager.FindByEmailAsync(dto.Email);

        if (userExist == null) 
        {
            return Result.Error("The email or password is incorrect");
        }

        bool isPasswordCorrect = await _userManager.CheckPasswordAsync(userExist, dto.Password);

        if (!isPasswordCorrect)
        {
            return Result.Error("The email or password is incorrect");
        }

        string jwtToken = GenerateJwtToken(userExist);

        return Result.Success(jwtToken);
    }

    public async Task<Result<string>> Register(RegisterDto dto)
    {
        var userExist = await _userManager.FindByEmailAsync(dto.Email);
        if (userExist != null) 
        {
            return Result.Error("Email Already Exist");
        }

        var newUser = new IdentityUser()
        {
            Email = dto.Email,
            UserName = dto.Email,

        };

        var isCreated = await _userManager.CreateAsync(newUser, dto.Password);

        if (!isCreated.Succeeded)
        {
            return Result.Error("User Creation Failed");
        }

        var token = GenerateJwtToken(newUser);

        return Result.Success(token);

    }

    private string GenerateJwtToken(IdentityUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = System.Text.Encoding.ASCII.GetBytes(_jwtConfiguration.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, value:user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()) 
            }),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

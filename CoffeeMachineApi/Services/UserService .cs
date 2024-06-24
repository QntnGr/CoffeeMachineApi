using CoffeeMachineApi.Entities;
using CoffeeMachineApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoffeeMachineApi.Services
{
    public class UserService : IUserService
    {
        private readonly TokenConfiguration _appSettings;
        private readonly CoffeeContext _coffeeDbContext;

        public UserService(IOptions<TokenConfiguration> appSettings, CoffeeContext coffeeDbContext)
        {
            _appSettings = appSettings.Value;
            _coffeeDbContext = coffeeDbContext;
        }

        public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model)
        {
            var user = await _coffeeDbContext.Users.SingleOrDefaultAsync(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = await generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _coffeeDbContext.Users.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _coffeeDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> AddAndUpdateUser(User userObj)
        {
            bool isSuccess = false;
            if (userObj.Id > 0)
            {
                var obj = await _coffeeDbContext.Users.FirstOrDefaultAsync(c => c.Id == userObj.Id);
                if (obj != null)
                {
                    obj.FirstName = userObj.FirstName;
                    obj.LastName = userObj.LastName;
                    _coffeeDbContext.Users.Update(obj);
                    isSuccess = await _coffeeDbContext.SaveChangesAsync() > 0;
                }
            }
            else
            {
                await _coffeeDbContext.Users.AddAsync(userObj);
                isSuccess = await _coffeeDbContext.SaveChangesAsync() > 0;
            }

            return isSuccess ? userObj : null;
        }

        private async Task<string> generateJwtToken(User user)
        {
            //Generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {

                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                return tokenHandler.CreateToken(tokenDescriptor);
            });

            return tokenHandler.WriteToken(token);
        }
    }
}

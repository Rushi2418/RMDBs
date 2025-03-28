using Microsoft.IdentityModel.Tokens;
using RMDBs_API.Data;
using RMDBs_API.Model;
using RMDBs_API.Model.DTO;
using RMDBs_API.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RMDBs_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private string secretKey;
        public UserRepository(ApplicationDbContext context,
                                IConfiguration configuration)
        {
            _context = context;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public IConfiguration Configuration { get; }

        public bool IsUserUnique(string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email.ToLower() == loginRequestDTO.UserName.ToLower()
            && x.Password == loginRequestDTO.Password);
            if (user == null)
            {

                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }


            // if user was found Genrete JWT token 

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.ID.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };
            return loginResponseDTO;

        }

        public async Task<User> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            User user = new()
            {
                Email = registerationRequestDTO.Email,
                Name = registerationRequestDTO.Name,
                Password = registerationRequestDTO.Password,
                MobileNumber = registerationRequestDTO.Mobilenumber,
                DateJoined = registerationRequestDTO.DateJoined,
                ProfilePicture = registerationRequestDTO.ProfilePicture,
                Role = registerationRequestDTO.Role,
                Address = registerationRequestDTO.Address,
                
                

            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}

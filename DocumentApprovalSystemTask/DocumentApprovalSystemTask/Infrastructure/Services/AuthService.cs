using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using DocumentApprovalSystemTask.Application.DTOs;
using DocumentApprovalSystemTask.Application.Interfaces;
using DocumentApprovalSystemTask.Domain.Entities;
using DocumentApprovalSystemTask.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace DocumentApprovalSystemTask.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IGeneralRepository<Employee> _employeeRepository;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IConfiguration config,
            IMapper mapper,
            IGeneralRepository<Employee> employeeRepository)
        {
            _userManager = userManager;
            _config = config;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task<string> RegisterAsync(RegisterDTO dto)
        {
            var user = _mapper.Map<ApplicationUser>(dto);

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return string.Join(" | ", result.Errors.Select(e => e.Description));
            }

            var employee = new Employee
            {
                UserId = user.Id,
                Name = dto.UserName
            };

            _employeeRepository.Add(employee);
            await _employeeRepository.SaveChangesAsync();


            return "Account Created Successfully";
        }

        public async Task<string?> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return null;

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Iss"],
                audience: _config["JWT:Aud"],
                expires: DateTime.UtcNow.AddHours(1),
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

      
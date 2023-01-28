using Domain.DTO;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserRepository> _logger;
        private readonly ApplicationDbContext _context;

        public UserRepository(UserManager<ApplicationUser> userManager, ILogger<UserRepository> logger, ApplicationDbContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }
        public async Task<bool> CreateUser(UserDto userDto)
        {
            bool result;

            try
            {
                var user = new ApplicationUser
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    UserName = userDto.UserName
                };

                await _userManager.CreateAsync(user, userDto.Password);

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                _logger.LogError(ex.Message);
            }

            return result;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            try
            {
                var users = await _context.Users.Select(x => new UserDto()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    UserName = x.UserName
                }).ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }
    }
}

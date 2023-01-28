using Domain.DTO;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetUsers();
        Task<bool> CreateUser(UserDto user);    
    }
}

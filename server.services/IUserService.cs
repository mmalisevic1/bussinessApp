using server.services.DTOs;
using System.Threading.Tasks;

namespace server.services
{
    public interface IUserService
    {
        Task<UserDTO> GetUser(long id);

        Task<UserDTO> GetUser(string email);

        Task<UserDTO> CreateUser(UserDTO user);

        Task<UserDTO> UpdateUser(UserDTO user);
    }
}

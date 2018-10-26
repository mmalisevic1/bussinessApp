using AutoMapper;
using server.data;
using server.data.Tables;
using server.services.DTOs;
using System;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace server.services
{
    public class UserService : IUserService
    {
        IGlobomanticsContext _globomanticsContext;

        public UserService(IGlobomanticsContext globomanticsContext) => _globomanticsContext = globomanticsContext;

        public async Task<UserDTO> GetUser(long id) => Mapper.Map<UserDTO>(await _globomanticsContext.Users.FindAsync(id));

        public async Task<UserDTO> GetUser(string email) => Mapper.Map<UserDTO>(await _globomanticsContext.Users.FirstOrDefaultAsync(f => f.Email == email));

        public async Task<UserDTO> CreateUser(UserDTO user)
        {
            if (user.Id > 0) {
                throw new ArgumentException("User having Id bigger than 0 is considered an existing user.");
            }

            user.Password = HashPassword(user.Password);
            Users createdUser = _globomanticsContext.Users.Add(Mapper.Map<Users>(user));

            await _globomanticsContext.SaveChangesAsync();
            return Mapper.Map<UserDTO>(createdUser);
        }

        public async Task<UserDTO> UpdateUser(UserDTO user)
        {
            if (user.Id <= 0) {
                throw new ArgumentException("User having Id less or equal to 0 is considered a new user.");
            }

            Users existingUser = await _globomanticsContext.Users.FindAsync(user.Id);
            existingUser.IsActive = user.IsActive;

            await _globomanticsContext.SaveChangesAsync();
            return Mapper.Map<UserDTO>(existingUser);
        }

        private string HashPassword(string password)
        {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("SHA-256")).ComputeHash(encodedPassword);
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
        }
    }
}

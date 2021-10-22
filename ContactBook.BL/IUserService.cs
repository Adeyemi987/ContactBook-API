
using ContactBook.DB.DTOS;
using ContactBook.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactBook.BL
{
    public interface IUserService
    {
        Task<bool> DeleteUser(string userId);
        Task<UserResponseDTO> GetUser(string userId);
        Task<bool> Update(string userId, UpdateRequest updateUser);
        IEnumerable<User> GetAllUsers();
    }
}
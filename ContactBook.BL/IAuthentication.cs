using ContactBook.DB.DTOS;
using System.Threading.Tasks;

namespace ContactBook.BL
{
    public interface IAuthentication
    {
        Task<UserResponseDTO> Login(UserRequest userRequest);
        Task<UserResponseDTO> Register(RegisterationRequest registerationRequest);
    }
}
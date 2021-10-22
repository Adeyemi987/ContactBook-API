using ContactBook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.DB.DTOS.Mappings
{
    public class UserMappings
    {
        public static UserResponseDTO GetUserResponse(User user)
        {
            return new UserResponseDTO
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id

            };

        }

        public static User GetUser(RegisterationRequest request)
        {
            return new User
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = string.IsNullOrWhiteSpace(request.UserName) ? request.Email : request.UserName,

            };
        }
    }
}

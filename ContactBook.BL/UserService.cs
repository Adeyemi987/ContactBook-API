using ContactBook.DB.DTOS;
using ContactBook.DB.DTOS.Mappings;
using ContactBook.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.BL
{
    public class UserService : IUserService
    {
        private readonly UserManager<Model.User> _userManager;

        public UserService(UserManager<Model.User> userManager)
        {
            _userManager = userManager;

        }

        public async Task<bool> Update(string userId, UpdateRequest updateUser)
        {
            Model.User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {

                user.FirstName = string.IsNullOrWhiteSpace(updateUser.FirstName) ? user.FirstName : updateUser.FirstName;
                user.LastName = string.IsNullOrWhiteSpace(updateUser.LastName) ? user.LastName : updateUser.LastName;
                user.PhoneNumber = string.IsNullOrWhiteSpace(updateUser.PhoneNumber) ? user.PhoneNumber : updateUser.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }

                string errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    errors += error.Description + Environment.NewLine;

                }

                throw new MissingMemberException(errors);
            }
            throw new ArgumentException("Resource not found");

        }


        public async Task<bool> DeleteUser(string userId)
        {
            Model.User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }

                string errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    errors += error.Description + Environment.NewLine;
                }

                throw new MissingMemberException(errors);
            }

            throw new ArgumentException("Resource not found");
        }


        //get
        public async Task<UserResponseDTO> GetUser(string userId)
        {
            Model.User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return UserMappings.GetUserResponse(user);
            }

            throw new ArgumentException("Resource not found");
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = _userManager.Users;
            if (users != null)
            {
                return users;
            }

            throw new NullReferenceException("User does not exist");
        }

      
    }
}

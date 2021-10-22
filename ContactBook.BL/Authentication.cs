using ContactBook.DB.DTOS;
using ContactBook.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using ContactBook.DB.DTOS.Mappings;

namespace ContactBook.BL
{
    public class Authentication : IAuthentication
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenGenerator _tokenGenerator;

        public Authentication(UserManager<User> userManager, ITokenGenerator tokenGenerator )
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<UserResponseDTO> Login(UserRequest userRequest)
        {
            User user = await _userManager.FindByEmailAsync(userRequest.Email);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, userRequest.Password))
                {
                   var response =  UserMappings.GetUserResponse(user);
                   response.Token = await _tokenGenerator.GenerateToken(user);

                    return response; 
                }

                throw new AccessViolationException("Invalid Credentials");
            }

            throw new AccessViolationException("Invalid Credentials");

        }


        public async Task<UserResponseDTO> Register(RegisterationRequest registerationRequest)
        {
            User user = UserMappings.GetUser(registerationRequest);

            IdentityResult result = await _userManager.CreateAsync(user, registerationRequest.Password);
            if (result.Succeeded)
            {
                return UserMappings.GetUserResponse(user);
            }

            string errors = string.Empty;

            foreach (var error in result.Errors)
            {
                errors += error.Description + Environment.NewLine;

            }
            throw new MissingFieldException(errors);
        }

    }
}

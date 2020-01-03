using Actio.Common.Auth;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Services.Identity.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IEncrypter _encrypter;

        private readonly IJwtHandler _jwtHandler;
        public UserService(IUserRepository userRepository, IEncrypter encrypter, IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
        }

        public async Task <JsonWebToken> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null) 
            {
                throw new ActioException("user_do_not_exists", "User with given mail do not exists");
            }

            if (!user.ValidatePassword(password, _encrypter)) 
            {
                throw new ActioException("invalid_credentials", "Invalid credentials");
            }

            return _jwtHandler.Create(user.Id);
        }

        public async Task RegisterAsync(string email, string name, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null) 
            {
                throw new ActioException("email_in_use",$"Email {email} already exists");
            }

            user = new User(Guid.NewGuid(),email,name);
            user.setPassword(password, _encrypter);

            await _userRepository.AddAsync(user);
        }
    }
}

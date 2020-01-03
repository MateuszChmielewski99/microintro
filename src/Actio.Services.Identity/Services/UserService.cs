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
        public UserService(IUserRepository userRepository, IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
        }

        public async Task LoginAsync(string email, string password)
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

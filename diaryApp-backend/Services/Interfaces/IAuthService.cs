using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace diaryApp_backend.Services.Interfaces
{
    public interface IAuthService
    {

        public Task<User> Login(string email, string password);
        public Task<User> Register(User newUser, string password);

    }
}

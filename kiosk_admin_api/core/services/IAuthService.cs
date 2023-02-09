using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.models.RequestResults;
namespace core.services
{
    public interface IAuthService
    {
        public Task<Tuple<bool, string>> Login(LoginRequest _loginRequest);
        // implement password decrypting and add password secret to user secrets. 
        public Task<RegisterResult> AddUser(RegisterRequest _registerRequest);
        public Task<bool> Auth(string _userID);
    }
}
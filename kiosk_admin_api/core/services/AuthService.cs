using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using core.models;
using core.models.RequestResults;
using core.dal;
namespace core.services
{
    public class AuthService: IAuthService
    {
        private readonly IConfigurationRoot _appConfig;
        private readonly KioskAdminContext _context;
        public AuthService()
        {
            _context = new KioskAdminContext();    
            _appConfig = new ConfigurationBuilder().AddUserSecrets<AuthService>().Build();
        }
        public async Task<bool> Login(LoginRequest _loginRequest)
        {
            User _user = await _context.Users.SingleOrDefaultAsync(_user => _user.Username == _loginRequest.Username);
            if(_user is null)
                return false;
            string _computedHash = _hash(Encoding.ASCII.GetBytes(_loginRequest.Password));
            if(_user.Password == _computedHash)
                return true;
            return false;
        }
        internal string _hash(byte[] _password)
        {
            byte[] _key = Encoding.ASCII.GetBytes(_appConfig["secret"]);
            HMACSHA256 _hash = new HMACSHA256(_key);
            byte[] _computedHash = _hash.ComputeHash(_password);
            return Encoding.ASCII.GetString(_computedHash);
        }
        public async Task<RegisterResult> AddUser(RegisterRequest _registerRequest)
        {
            if(await _context.Users.SingleOrDefaultAsync(_user => _user.Username == _registerRequest.AuthedBy) is null)
                return new RegisterResult(RegisterResultEnum.NotAuthorizedError);
            if(await _context.Users.SingleOrDefaultAsync(_item => _item.Username == _registerRequest.Username) is not null )
                return new RegisterResult(RegisterResultEnum.UserAlreadyExistsError);
            User _newUser = _userBuilder(_registerRequest.Username, _hash(Encoding.ASCII.GetBytes(_registerRequest.Password)));
            await _context.Users.AddAsync(_newUser);
            await _context.SaveChangesAsync();
            if(await _context.Users.SingleOrDefaultAsync(_user => _user.Username == _registerRequest.Username) is not null)
                return new RegisterResult(RegisterResultEnum.Successful);
            return new RegisterResult(RegisterResultEnum.DatabaseError);
        }
        internal Func<string, string, User> _userBuilder = (string _username, string _password) => {
            User _newUser = new User();
            _newUser.UserId = Guid.NewGuid().ToString("N");
            _newUser.Username = _username;
            _newUser.Password = _password;
            return _newUser;
        };
    }
}
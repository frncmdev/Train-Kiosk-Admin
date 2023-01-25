using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using core.models.DbEntities;
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
            string _computedHash = _hash(Encoding.ASCII.GetBytes(_appConfig["secret"]), Encoding.ASCII.GetBytes(_loginRequest.Password));
            if(_user.Password == _computedHash)
                return true;
            return false;
        }
        private string _hash(byte[] _key, byte[] _password)
        {
            HMACSHA256 _hash = new HMACSHA256(_key);
            byte[] _computedHash = _hash.ComputeHash(_password);
            return Encoding.ASCII.GetString(_computedHash);
        }
        public async Task<bool> addUser()
        {
            User _user = new User();
            string _guid = new Guid().ToString("N");
            _user.UserId = _guid;
            _user.Username = "Kiosk_Admin";
            _user.Password = _hash(Encoding.ASCII.GetBytes(_appConfig["secret"]), Encoding.ASCII.GetBytes("TrainKioskPassword"));
            await _context.Users.AddAsync(_user);
            await _context.SaveChangesAsync();
            if(await _context.Users.SingleOrDefaultAsync(_usr => _usr.UserId == _guid) is not null)
                return true;
            return false;
        }

    }
}
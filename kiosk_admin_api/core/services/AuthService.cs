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
        /*
            This C# method is an asynchronous method named Login that takes a LoginRequest object as an argument. The method returns a Task of Tuple which contains a boolean value and a string.
            The method starts by querying the Users collection in the database context for a user with a Username equal to the Username property of the LoginRequest object. The SingleOrDefaultAsync method is used to retrieve a single user that matches the specified condition or return null if no such user exists.
            If the retrieved user is null, the method returns a tuple with a bool value of false and an empty string. If the user is not null, the method computes a hash of the password using the _hash method and the Encoding.ASCII.GetBytes method. The computed hash is then compared to the Password property of the user.
            If the Password of the user matches the computed hash, the method returns a tuple with a bool value of true and the UserId of the user. If the passwords do not match, the method returns a tuple with a bool value of false and an empty string.
        */
        public async Task<Tuple<bool, string>> Login(LoginRequest _loginRequest)
        {
            User _user = await _context.Users.SingleOrDefaultAsync(_user => _user.Username == _loginRequest.Username);
            if(_user is null)
                return new Tuple<bool, string>(false, "");
            string _computedHash = _hash(Encoding.ASCII.GetBytes(_loginRequest.Password));
            if(_user.Password == _computedHash)
                return new Tuple<bool, string>(true, _user.UserId);
            return new Tuple<bool, string>(false, "");
;
        }
        /*
            This C# method is an internal method named _hash that takes a byte[] object as an argument. The method returns a string.
            The method starts by creating a byte array _key from the ASCII encoding of a secret value stored in the _appConfig dictionary. Then, it creates an instance of the HMACSHA256 class, passing the _key as an argument to its constructor.
            Next, the method computes a hash of the input _password using the ComputeHash method of the HMACSHA256 instance. The computed hash is stored in the _computedHash byte array.
            Finally, the method returns the ASCII encoding of the _computedHash as a string.
            Note that the HMACSHA256 class is a commonly used implementation of a keyed-hash message authentication code (HMAC) algorithm with the SHA-256 hash function. This method is likely used to securely store the password in the database by creating a hash of the password that is difficult to reverse. 
        */
        internal string _hash(byte[] _password)
        {
            byte[] _key = Encoding.ASCII.GetBytes(_appConfig["secret"]);
            HMACSHA256 _hash = new HMACSHA256(_key);
            byte[] _computedHash = _hash.ComputeHash(_password);
            return Encoding.ASCII.GetString(_computedHash);
        }
        /*
            The function AddUser is an asynchronous method in C# that takes in a RegisterRequest object as input and returns a Task of RegisterResult type. The purpose of the function is to add a new user to the database.
            The first thing the function does is check if the user who is making the request to add a new user is authorized to do so. This is done by checking if there is a user in the database with a username matching the AuthedBy property of the RegisterRequest object. If there is no matching user, the function immediately returns a RegisterResult object with the status set to NotAuthorizedError.
            Next, the function checks if the requested username already exists in the database by using the SingleOrDefaultAsync method and checking if the result is not null. If the username already exists, the function returns a RegisterResult object with the status set to UserAlreadyExistsError.
            If both of the previous checks pass, the function uses the _userBuilder method to create a new User object and sets its properties using the provided Username and Password from the RegisterRequest object. The new user is then added to the database using the AddAsync method. The changes are saved to the database using the SaveChangesAsync method.
            Finally, the function checks if the new user was successfully added to the database by using the SingleOrDefaultAsync method and checking if the result is not null. If the new user was added, the function returns a RegisterResult object with the status set to Successful. If not, the function returns a RegisterResult object with the status set to DatabaseError.
        */
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
        /*
            This is a C# delegate named _userBuilder, which is a type of function that can be passed as a parameter or assigned to a variable. The delegate is defined as taking two string parameters, _username and _password, and returning an object of type User.
            The delegate body creates a new User object, assigns a new unique identifier to the UserId property, sets the Username property to the value of _username, and sets the Password property to the value of _password. The created User object is then returned.
            Delegates provide a way to encapsulate a method as a data type, allowing the method to be passed as an argument to other methods or assigned to a variable. They're often used for event handling, multi-casting and method callbacks in C#.
        */
        internal Func<string, string, User> _userBuilder = (string _username, string _password) => {
            User _newUser = new User();
            _newUser.UserId = Guid.NewGuid().ToString("N");
            _newUser.Username = _username;
            _newUser.Password = _password;
            return _newUser;
        };
        

    }
}
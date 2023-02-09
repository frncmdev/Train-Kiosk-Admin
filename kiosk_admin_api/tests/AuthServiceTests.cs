using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace tests
{
    public class AuthServiceTests
    {
        private readonly IAuthService _service = new AuthService();
        [Fact]
        public async Task TestCreate()
        {
            int successes = 0;
            RegisterResult _result = await _service.AddUser(new RegisterRequest("TestAuth", "TestAuth", "RootAuthority"));
            if(_result.result == RegisterResultEnum.Successful)
                successes += 1;
            _result = await _service.AddUser(new RegisterRequest("TestAuth", "TestAuth", "RootAuthority"));
            if(_result.result == RegisterResultEnum.UserAlreadyExistsError)
                successes += 1;
            _result = await _service.AddUser(new RegisterRequest("TestAuth", "TestAuth", ""));
            if(_result.result == RegisterResultEnum.NotAuthorizedError)
                successes += 1;
            if(successes == 3)
                Assert.True(true);
            else 
                Assert.True(false);
        }
        [Fact]
        public async Task TestLogin()
        {
            var request = await _service.Login(new LoginRequest("TestAuth", "TestAuth"));
            int successes = 0;
            if(request.Item1)
                successes +=1;
            request = await _service.Login(new LoginRequest("TestAuth", "TestAuth0"));
            if(!request.Item1)
                successes +=1;
            if(successes == 2)
                Assert.True(true);
            else 
                Assert.True(false);
        }
    }
}
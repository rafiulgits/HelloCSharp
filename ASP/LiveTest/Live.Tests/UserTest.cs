// using System;
using Xunit;
using Live.Controller;

namespace Live.Tests
{
    public class UserTest
    {

        private readonly User _user;
        public UserTest()
        {
            _user = new User();
        }

        [Fact]
        public void IsOddTrue()
        {   
            var result = _user.IsOdd(135);
            Assert.True(result, $"{result} is Odd");
        }
    }
}

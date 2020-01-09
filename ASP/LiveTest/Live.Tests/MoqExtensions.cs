using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;

namespace Live.Tests
{
    public static class MoqExtensions
    {
        public static async Task VerifyWithTimeoutAsync<T>(this Mock<T> mock, Expression<Action<T>> expression, Times times, int timeoutInMs)
        where T : class
        {
            bool hasBeenExecuted = false;
            bool hasTimedOut = false;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while(!hasBeenExecuted && !hasTimedOut)
            {
                if(stopwatch.ElapsedMilliseconds > timeoutInMs)
                {
                    hasTimedOut = true;
                }

                try
                {
                    mock.Verify(expression, times);
                    hasBeenExecuted = true;
                }
                catch(Exception)
                {

                }

                await Task.Delay(20);
            }
        }
    }
}
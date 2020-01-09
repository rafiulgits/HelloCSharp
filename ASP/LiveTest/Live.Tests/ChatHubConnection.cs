using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Moq;
using Live.Models;

namespace Live.Tests
{
    public class ChatHubConnection
    {
        private readonly HubConnection _connection;
        private readonly Dictionary<Type, object> _handlersMap;
        private readonly int _verificationTimeout;

        internal ChatHubConnection(string url, int verificationTimeout = 1000)
        {
            _connection = new HubConnectionBuilder()
                            .WithUrl(url)
                            .Build();
            _verificationTimeout = verificationTimeout;
            _handlersMap = new Dictionary<Type, object>();
        }

        public void Expect<TEvent>(string expectedName)
        {
            var handlerMock = new Mock<Action<TEvent>>();
            RegisterHandler(handlerMock);
            _connection.On(expectedName,handlerMock.Object);
        }

        public void Expect(string expectedName, Type expectedType)
        {
            var expectedMethod = typeof(ChatHubConnection)
                .GetMethods()
                .First(item => item.ContainsGenericParameters && item.Name == nameof(Expect))
                .MakeGenericMethod(new[] {expectedType});

            expectedMethod.Invoke(this, new object[] {expectedName});
        }

        public async Task StartAsync()
        {
            await _connection.StartAsync();
        }

        public async Task VerifyMessageReceived<TEvent>(Expression<Func<TEvent,bool>> predicate, Times times)
        {
            if(!_handlersMap.ContainsKey(typeof(TEvent)))
                throw new HandlerNotRegisteredException(typeof(TEvent));

            var handlersForType = _handlersMap[typeof(TEvent)];
            foreach(var handler in (List<Mock<Action<TEvent>>>) handlersForType)
            {
                await handler.VerifyWithTimeoutAsync(
                    x => x(It.Is(predicate)),
                    times,
                    _verificationTimeout
                );
            }
        }

        private void RegisterHandler<TEvent>(Mock<Action<TEvent>> handler)
        {
            if(!_handlersMap.TryGetValue(typeof(TEvent), out object handlersForType))
            {
                handlersForType = new List<Mock<Action<TEvent>>>();
                _handlersMap[typeof(TEvent)] = handlersForType;
            }

            var handlers = (List<Mock<Action<TEvent>>>)handlersForType;
            handlers.Add(handler);
        }
    }
}
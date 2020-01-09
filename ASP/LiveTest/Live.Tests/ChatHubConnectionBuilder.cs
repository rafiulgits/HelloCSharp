using System;
using System.Collections.Generic;

namespace Live.Tests
{
    public class ChatHubConnectionBuilder
    {
        private List<(Type Type, string Name)> _expectedEventNames;
        private string _hubUrl;

        public ChatHubConnection Build()
        {
            if(string.IsNullOrEmpty(_hubUrl))
                throw new InvalidOperationException($"Use {nameof(OnHub)} to set the hub url.");
        
            if(_expectedEventNames == null || _expectedEventNames.Count == 0)
                throw new InvalidOperationException($"Use {nameof(WithExpectedEvent)} to set the expected event name.");
            
            var testConnection = new ChatHubConnection(_hubUrl);

            foreach(var expected in _expectedEventNames)
            {
                testConnection.Expect(expected.Name, expected.Type);
            }

            Clear();

            return testConnection;
        }


        public ChatHubConnectionBuilder OnHub(string hubUrl)
        {
            _hubUrl = hubUrl;
            return this;
        }

        public ChatHubConnectionBuilder WithExpectedEvent<TEvent>(string eventName)
        {
            if(_expectedEventNames == null)
            {
                _expectedEventNames = new List<(Type Type, string Name)>();
            }
            _expectedEventNames.Add((typeof(TEvent), eventName));
            return this;
        }

        private void Clear()
        {
            _expectedEventNames = null;
            _hubUrl = null;
        }
    }
}
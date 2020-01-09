using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Live.Models;


namespace Live.Tests
{
    public class ChatHubTests
    {
        private readonly AppFixture _fixture;

        private const string Host = "https://localhost:5001";

        public ChatHubTests()
        {
            _fixture = new AppFixture();
        }

        [Fact]
        public async Task ShouldNotifySubscribers()
        {
            var notificationToSend = new Notification{Message = "Test Message"};

            var connection = new ChatHubConnectionBuilder()
                        .OnHub(_fixture.GetCompleteServerUrl("/chathub"))
                        .WithExpectedEvent<Notification>(nameof(Notification))
                        .Build();

            await connection.StartAsync();
            
            // Act
            await _fixture.ExecuteHttpClientAsync(httpClient =>
                httpClient.PostAsJsonAsync($"{Host}/hub/test", notificationToSend)        
            );

            // Assert
            await connection.VerifyMessageReceived<Notification>(
                n => n.Message == notificationToSend.Message,
                Times.Once()
            );
        }
        
    }
}
using AutoBogus;
using Core;
using Core.Domain;
using Core.UseCases.GetClient;
using Core.UseCases.GetClient.Boundaries;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.UseCases
{
    public class GetClientUnitTests
    {
        private readonly GetClient _getClient;
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly Mock<ILogger<GetClient>> _logger;
        public GetClientUnitTests()
        {
            _clientRepository = new Mock<IClientRepository>();
            _logger = new Mock<ILogger<GetClient>>();

            _getClient = new GetClient(_clientRepository.Object, _logger.Object);
        }

        [Fact(DisplayName = "GetClient >> Should Return Success >> When Getting Client")]
        public async Task GetClient_ShouldReturnSuccess_WhenGettingClient()
        {
            //arrange
            var input = new AutoFaker<GetClientInput>()
                                    .RuleFor(s => s.AccountCode, f => f.Random.Int(1))
                                    .Generate();
            var clientExpected = new AutoFaker<Client>()
                                        .RuleFor(s => s.AccountCode, input.AccountCode)
                                        .Generate();

            _clientRepository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(clientExpected);

            //act
            var output = await _getClient.Handle(input);
            var client = (Client)output.Result;

            //assert
            output.IsValid.Should().BeTrue();
            output.ErrorMessages.Should().BeEmpty();
            output.Result.Should().NotBeNull();

            client.AccountCode.Should().Be(input.AccountCode);

            _logger.VerifyLog(s => s.LogInformation("*UseCase Started*"));
            _logger.VerifyLog(s => s.LogInformation("*Client Was Got Successfully*"));
            _logger.VerifyLog(s => s.LogInformation($"*CorrelationId: {input.CorrelationId}*"), Times.Exactly(2));

            _logger.Invocations.Should().HaveCount(2);
        }

        [Fact(DisplayName = "GetClient >> Should Return Error >> When AccountCode Is Lesser Than One")]
        public async Task GetClient_ShouldReturnError_WhenAccountCodeIsLesserThanOne()
        {
            //arrange
            var input = new AutoFaker<GetClientInput>()
                                    .RuleFor(s => s.AccountCode, f => f.Random.Int(int.MinValue, 0))
                                    .Generate();
            var clientExpected = new AutoFaker<Client>()
                                        .RuleFor(s => s.AccountCode, input.AccountCode)
                                        .Generate();

            _clientRepository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(clientExpected);

            //act
            var output = await _getClient.Handle(input);
            var client = (Client)output.Result;

            //assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().Contain("'Account Code' must be greater than '0'.");
            output.ErrorMessages.Should().HaveCount(1);
            output.Result.Should().BeNull();

            client.Should().BeNull();

            _logger.VerifyLog(s => s.LogInformation("*UseCase Started*"));
            _logger.VerifyLog(s => s.LogError("*Validation Error*"));
            _logger.Invocations.Should().HaveCount(2);
        }

        [Fact(DisplayName = "GetClient >> Should Return Error >> When Exception At Getting Client")]
        public async Task GetClient_ShouldReturnError_WhenExceptionAtGettingClient()
        {
            //arrange
            var input = new AutoFaker<GetClientInput>()
                                    .RuleFor(s => s.AccountCode, f => f.Random.Int(1))
                                    .Generate();

            string errorMessage = "errorMessage";

            _clientRepository.Setup(s => s.GetAsync(It.IsAny<int>())).ThrowsAsync(new Exception(errorMessage));

            //act
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _getClient.Handle(input));

            //assert
            exception.Message.Should().Be(errorMessage);

            _logger.VerifyLog(s => s.LogInformation("*UseCase Started*"));

            _logger.Invocations.Should().HaveCount(1);
        }
    }
}
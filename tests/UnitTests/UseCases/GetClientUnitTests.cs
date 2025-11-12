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

        [Fact(DisplayName = "GetClient >> Should Return Error >> When AccountCode Is Zero")]
        public async Task GetClient_ShouldReturnError_WhenAccountCodeIsZero()
        {
            //arrange
            var input = new GetClientInput(Guid.NewGuid(), 0);

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

            _clientRepository.Verify(s => s.GetAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact(DisplayName = "GetClient >> Should Call Repository >> With Correct AccountCode")]
        public async Task GetClient_ShouldCallRepository_WithCorrectAccountCode()
        {
            //arrange
            var accountCode = 12345;
            var input = new GetClientInput(Guid.NewGuid(), accountCode);
            var clientExpected = new AutoFaker<Client>()
                                        .RuleFor(s => s.AccountCode, accountCode)
                                        .Generate();

            _clientRepository.Setup(s => s.GetAsync(accountCode)).ReturnsAsync(clientExpected);

            //act
            var output = await _getClient.Handle(input);

            //assert
            _clientRepository.Verify(s => s.GetAsync(accountCode), Times.Once);
            output.IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "GetClient >> Should Log Correctly >> When Getting Client Successfully")]
        public async Task GetClient_ShouldLogCorrectly_WhenGettingClientSuccessfully()
        {
            //arrange
            var input = new AutoFaker<GetClientInput>()
                                    .RuleFor(s => s.AccountCode, f => f.Random.Int(1))
                                    .Generate();
            var clientExpected = new AutoFaker<Client>()
                                        .RuleFor(s => s.AccountCode, input.AccountCode)
                                        .RuleFor(s => s.Name, "Test Client Name")
                                        .Generate();

            _clientRepository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(clientExpected);

            //act
            var output = await _getClient.Handle(input);

            //assert
            _logger.VerifyLog(s => s.LogInformation($"*CorrelationId: {input.CorrelationId}*"), Times.Exactly(2));
            _logger.VerifyLog(s => s.LogInformation($"*AccountCode: {input.AccountCode}*"), Times.Exactly(2));
            _logger.VerifyLog(s => s.LogInformation($"*Name: {clientExpected.Name}*"));
        }

        [Fact(DisplayName = "GetClient >> Should Return Client With Same Properties >> When Getting Client")]
        public async Task GetClient_ShouldReturnClientWithSameProperties_WhenGettingClient()
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
            client.Should().BeEquivalentTo(clientExpected);
            client.AccountCode.Should().Be(clientExpected.AccountCode);
            client.Name.Should().Be(clientExpected.Name);
        }

        [Theory(DisplayName = "GetClient >> Should Return Error >> When AccountCode Is Invalid")]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-999999)]
        [InlineData(int.MinValue)]
        public async Task GetClient_ShouldReturnError_WhenAccountCodeIsInvalid(int invalidAccountCode)
        {
            //arrange
            var input = new GetClientInput(Guid.NewGuid(), invalidAccountCode);

            //act
            var output = await _getClient.Handle(input);

            //assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().Contain("'Account Code' must be greater than '0'.");
            output.Result.Should().BeNull();

            _clientRepository.Verify(s => s.GetAsync(It.IsAny<int>()), Times.Never);
            _logger.VerifyLog(s => s.LogError("*Validation Error*"));
        }

        [Theory(DisplayName = "GetClient >> Should Return Success >> When AccountCode Is Valid")]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(999999)]
        [InlineData(int.MaxValue)]
        public async Task GetClient_ShouldReturnSuccess_WhenAccountCodeIsValid(int validAccountCode)
        {
            //arrange
            var input = new GetClientInput(Guid.NewGuid(), validAccountCode);
            var clientExpected = new AutoFaker<Client>()
                                        .RuleFor(s => s.AccountCode, validAccountCode)
                                        .Generate();

            _clientRepository.Setup(s => s.GetAsync(validAccountCode)).ReturnsAsync(clientExpected);

            //act
            var output = await _getClient.Handle(input);
            var client = (Client)output.Result;

            //assert
            output.IsValid.Should().BeTrue();
            output.ErrorMessages.Should().BeEmpty();
            client.AccountCode.Should().Be(validAccountCode);

            _clientRepository.Verify(s => s.GetAsync(validAccountCode), Times.Once);
        }

        [Fact(DisplayName = "GetClient >> Should Not Call Repository >> When Validation Fails")]
        public async Task GetClient_ShouldNotCallRepository_WhenValidationFails()
        {
            //arrange
            var input = new GetClientInput(Guid.NewGuid(), -1);

            //act
            var output = await _getClient.Handle(input);

            //assert
            output.IsValid.Should().BeFalse();
            _clientRepository.Verify(s => s.GetAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact(DisplayName = "GetClient >> Should Have Valid Output Structure >> When Getting Client")]
        public async Task GetClient_ShouldHaveValidOutputStructure_WhenGettingClient()
        {
            //arrange
            var input = new AutoFaker<GetClientInput>()
                                    .RuleFor(s => s.AccountCode, f => f.Random.Int(1))
                                    .Generate();
            var clientExpected = new AutoFaker<Client>().Generate();

            _clientRepository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(clientExpected);

            //act
            var output = await _getClient.Handle(input);

            //assert
            output.Should().NotBeNull();
            output.Should().BeOfType<Output>();
            output.Result.Should().NotBeNull();
            output.Result.Should().BeOfType<Client>();
        }
    }
}
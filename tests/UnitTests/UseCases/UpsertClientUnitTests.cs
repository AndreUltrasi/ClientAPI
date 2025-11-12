using AutoBogus;
using Core;
using Core.Domain;
using Core.Domain.Enums;
using Core.Domain.Interfaces.Https;
using Core.Dto;
using Core.UseCases.UpsertClient;
using Core.UseCases.UpsertClient.Boundaries;
using Core.ViewModel;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.UseCases
{
    public class UpsertClientUnitTests
    {
        private readonly UpsertClient _upsertClient;
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly Mock<IAddressService> _addressService;
        private readonly Mock<ILogger<UpsertClient>> _logger;
        public UpsertClientUnitTests()
        {
            _clientRepository = new Mock<IClientRepository>();
            _logger = new Mock<ILogger<UpsertClient>>();
            _addressService = new Mock<IAddressService>();

            _upsertClient = new UpsertClient(_clientRepository.Object, _addressService.Object, _logger.Object);
        }

        [Fact(DisplayName = "UpsertClient >> Should Return Success >> When Inserting Client")]
        public async Task UpsertClient_ShouldReturnSuccess_WhenInsertingClient()
        {
            //arrange
            var clientViewModel = new AutoFaker<ClientViewModel>()
                                    .RuleFor(s => s.AccountCode, f => f.Random.Int(1))
                                    .RuleFor(s => s.Age, f => f.Random.Int(1))
                                    .Generate();

            var input = new AutoFaker<UpsertClientInput>()
                                    .RuleFor(s => s.Client, clientViewModel)
                                    .Generate();

            var addressDto = new AutoFaker<AddressDto>()
                                    .Generate();

            _clientRepository.Setup(s => s.UpsertAsync(It.IsAny<Client>())).ReturnsAsync(UpsertStatus.Inserted);
            _addressService.Setup(s => s.GetAddressAsync(It.IsAny<string>())).ReturnsAsync(new Output(addressDto));

            //act
            var output = await _upsertClient.Handle(input);

            //assert
            output.IsValid.Should().BeTrue();
            output.ErrorMessages.Should().BeEmpty();
            output.Result.Should().NotBeNull();

            _logger.VerifyLog(s => s.LogInformation("*UseCase Started*"));
            _logger.VerifyLog(s => s.LogInformation("*Address Got Successfully From Api*"));
            _logger.VerifyLog(s => s.LogInformation("*Client Was Inserted Successfully*"));

            _logger.Invocations.Should().HaveCount(3);
        }

        [Fact(DisplayName = "UpsertClient >> Should Return Success >> When Updating Client")]
        public async Task UpsertClient_ShouldReturnSuccess_WhenUpdatingClient()
        {
            //arrange
            var clientViewModel = new AutoFaker<ClientViewModel>()
                                    .RuleFor(s => s.AccountCode, f => f.Random.Int(1))
                                    .RuleFor(s => s.Age, f => f.Random.Int(1))
                                    .Generate();

            var input = new AutoFaker<UpsertClientInput>()
                                    .RuleFor(s => s.Client, clientViewModel)
                                    .Generate();

            var addressDto = new AutoFaker<AddressDto>()
                                    .Generate();

            _clientRepository.Setup(s => s.UpsertAsync(It.IsAny<Client>())).ReturnsAsync(UpsertStatus.Updated);
            _addressService.Setup(s => s.GetAddressAsync(It.IsAny<string>())).ReturnsAsync(new Output(addressDto));

            //act
            var output = await _upsertClient.Handle(input);

            //assert
            output.IsValid.Should().BeTrue();
            output.ErrorMessages.Should().BeEmpty();
            output.Result.Should().NotBeNull();

            _logger.VerifyLog(s => s.LogInformation("*UseCase Started*"));
            _logger.VerifyLog(s => s.LogInformation("*Address Got Successfully From Api*"));
            _logger.VerifyLog(s => s.LogInformation("*Client Was Updated Successfully*"));

            _logger.Invocations.Should().HaveCount(3);
        }

        [Fact(DisplayName = "UpsertClient >> Should Return Error >> When Error At Validating Client")]
        public async Task UpsertClient_ShouldReturnError_WhenErrorAtValidatingClient()
        {
            //arrange
            var clientViewModel = new AutoFaker<ClientViewModel>()
                                    .RuleFor(s => s.AccountCode, f => f.Random.Int(int.MinValue, 0))
                                    .RuleFor(s => s.Age, f => f.Random.Int(int.MinValue, 0))
                                    .RuleFor(s => s.Cep, string.Empty)
                                    .RuleFor(s => s.Gender, f => (Gender)f.Random.Int(int.MinValue, int.MaxValue))
                                    .RuleFor(s => s.Name, string.Empty)
                                    .RuleFor(s => s.Number, string.Empty)
                                    .RuleFor(s => s.PersonType, f => (PersonType)f.Random.Int(int.MinValue, int.MaxValue))
                                    .Generate();

            var input = new AutoFaker<UpsertClientInput>()
                                    .RuleFor(s => s.Client, clientViewModel)
                                    .Generate();

            //act
            var output = await _upsertClient.Handle(input);

            //assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().Contain("'Client Account Code' must be greater than '0'.");
            output.ErrorMessages.Should().Contain("'Client Age' must be greater than '0'.");
            output.ErrorMessages.Should().Contain("'Client Cep' must not be empty.");
            output.ErrorMessages.Should().ContainMatch("'Client Gender' has a range of values which does not include*");
            output.ErrorMessages.Should().Contain("'Client Name' must not be empty.");
            output.ErrorMessages.Should().Contain("'Client Number' must not be empty.");
            output.ErrorMessages.Should().ContainMatch("'Client Person Type' has a range of values which does not include*");
            output.ErrorMessages.Should().HaveCount(7);
            output.Result.Should().BeNull();

            _logger.VerifyLog(s => s.LogInformation("*UseCase Started*"));
            _logger.VerifyLog(s => s.LogError("*Validation Error*"), Times.Exactly(7));

            _logger.Invocations.Should().HaveCount(8);
        }

        [Fact(DisplayName = "UpsertClient >> Should Return Error >> When Returns Exception From AddressApi")]
        public async Task UpsertClient_ShouldReturnError_WhenReturnsExceptionFromAddressApi()
        {
            //arrange
            var clientViewModel = new AutoFaker<ClientViewModel>()
                                    .RuleFor(s => s.AccountCode, f => f.Random.Int(1))
                                    .RuleFor(s => s.Age, f => f.Random.Int(1))
                                    .Generate();

            var input = new AutoFaker<UpsertClientInput>()
                                    .RuleFor(s => s.Client, clientViewModel)
                                    .Generate();

            string errorMessage = "errorMessage";

            _addressService.Setup(s => s.GetAddressAsync(It.IsAny<string>())).ThrowsAsync(new Exception(errorMessage));

            //act
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _upsertClient.Handle(input));

            //assert
            exception.Message.Should().Be(errorMessage);

            _logger.VerifyLog(s => s.LogInformation("*UseCase Started*"));

            _logger.Invocations.Should().HaveCount(1);
        }

        [Fact(DisplayName = "UpsertClient >> Should Return Error >> When Throws Exception When Acessing Database")]
        public async Task UpsertClient_ShouldReturnError_WhenThrowsExceptionWhenAcessingDatabase()
        {
            //arrange
            var clientViewModel = new AutoFaker<ClientViewModel>()
                                    .RuleFor(s => s.AccountCode, f => f.Random.Int(1))
                                    .RuleFor(s => s.Age, f => f.Random.Int(1))
                                    .Generate();

            var input = new AutoFaker<UpsertClientInput>()
                                    .RuleFor(s => s.Client, clientViewModel)
                                    .Generate();

            var addressDto = new AutoFaker<AddressDto>()
                                    .Generate();

            string errorMessage = "errorMessage";

            _addressService.Setup(s => s.GetAddressAsync(It.IsAny<string>())).ReturnsAsync(new Output(addressDto));
            _clientRepository.Setup(s => s.UpsertAsync(It.IsAny<Client>())).ThrowsAsync(new Exception(errorMessage));

            //act
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _upsertClient.Handle(input));

            //assert
            exception.Message.Should().Be(errorMessage);

            _logger.VerifyLog(s => s.LogInformation("*UseCase Started*"));
            _logger.VerifyLog(s => s.LogInformation("*Address Got Successfully From Api*"));

            _logger.Invocations.Should().HaveCount(2);
        }
    }
}
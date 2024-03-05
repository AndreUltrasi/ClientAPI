using AutoBogus;
using Core;
using Core.Domain;
using Core.Domain.Enums;
using Core.Domain.Interfaces.Https;
using Core.Dto;
using Core.UseCases.GetClient.Boundaries;
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
            _logger.VerifyLog(s => s.LogInformation("*Client Was Inserted Successfully*"));
            _logger.VerifyLog(s => s.LogInformation($"*CorrelationId: {input.CorrelationId}*"), Times.Exactly(2));

            _logger.Invocations.Should().HaveCount(2);
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
            _logger.VerifyLog(s => s.LogInformation("*Client Was Updated Successfully*"));
            _logger.VerifyLog(s => s.LogInformation($"*CorrelationId: {input.CorrelationId}*"), Times.Exactly(2));

            _logger.Invocations.Should().HaveCount(2);
        }
    }
}
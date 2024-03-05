using AutoBogus;
using Core;
using Core.Domain.Enums;
using Core.UseCases.DeleteClient;
using Core.UseCases.DeleteClient.Boundaries;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.UseCases
{
    public class DeleteClientUnitTests
    {
        private readonly DeleteClient _deleteClient;
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly Mock<ILogger<DeleteClient>> _logger;
        public DeleteClientUnitTests()
        {
            _clientRepository = new Mock<IClientRepository>();
            _logger = new Mock<ILogger<DeleteClient>>();

            _deleteClient = new DeleteClient(_clientRepository.Object, _logger.Object);
        }

        [Fact(DisplayName = "DeleteClient >> Should Return Success >> When Deleting Client")]
        public async Task DeleteClient_ShouldReturnSuccess_WhenDeletingClient()
        {
            //arrange
            var input = new AutoFaker<DeleteClientInput>()
                                    .RuleFor(s => s.AccountCode, f => f.Random.Int(1))
                                    .Generate();

            _clientRepository.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(DeleteStatus.Disabled);

            //act
            var output = await _deleteClient.Handle(input);

            //assert
            output.IsValid.Should().BeTrue();
            output.ErrorMessages.Should().BeEmpty();
            output.Result.Should().BeNull();

            _logger.VerifyLog(s => s.LogInformation("*UseCase Started*"));
            _logger.VerifyLog(s => s.LogInformation("*Client Was Disabled Successfully*"));

            _logger.Invocations.Should().HaveCount(2);
        }

        [Fact(DisplayName = "DeleteClient >> Should Return Success >> When Client Is Already Deleted")]
        public async Task DeleteClient_ShouldReturnSuccess_WhenClientIsAlreadyDeleted()
        {
            //arrange
            var input = new AutoFaker<DeleteClientInput>()
                                    .RuleFor(s => s.AccountCode, f => f.Random.Int(1))
                                    .Generate();

            _clientRepository.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(DeleteStatus.AlreadyDisabled);

            //act
            var output = await _deleteClient.Handle(input);

            //assert
            output.IsValid.Should().BeTrue();
            output.ErrorMessages.Should().BeEmpty();
            output.Result.Should().BeNull();

            _logger.VerifyLog(s => s.LogInformation("*UseCase Started*"));
            _logger.VerifyLog(s => s.LogInformation("*Client Was Already Disabled*"));

            _logger.Invocations.Should().HaveCount(2);
        }

        [Fact(DisplayName = "DeleteClient >> Should Return Error >> When Throws Exception When Accessing Database")]
        public async Task DeleteClient_ShouldReturnError_WhenThrowsExceptionWhenAccessingDatabase()
        {
            //arrange
            var input = new AutoFaker<DeleteClientInput>()
                                    .RuleFor(s => s.AccountCode, f => f.Random.Int(1))
                                    .Generate();

            string errorMessage = "errorMessage";

            _clientRepository.Setup(s => s.DeleteAsync(It.IsAny<int>())).ThrowsAsync(new Exception(errorMessage));

            //act
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _deleteClient.Handle(input));

            //assert
            exception.Message.Should().Be(errorMessage);

            _logger.VerifyLog(s => s.LogInformation("*UseCase Started*"));

            _logger.Invocations.Should().HaveCount(1);
        }
    }
}
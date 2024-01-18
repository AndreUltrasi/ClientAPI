using Core.UseCases.DeleteClient.Boundaries;

namespace Core.Domain
{
    public interface IDeleteClient
    {
        Task Handle(DeleteClientInput input);
    }
}
using Core.UseCases.GetClient.Boundaries;

namespace Core.Domain
{
    public interface IDeleteClient
    {
        Task Handle(DeleteClientInput input);
    }
}
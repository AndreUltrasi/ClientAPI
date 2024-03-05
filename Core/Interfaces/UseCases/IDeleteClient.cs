using Core.UseCases.DeleteClient.Boundaries;

namespace Core.Interfaces.UseCases
{
    public interface IDeleteClient
    {
        Task<Output> Handle(DeleteClientInput input);
    }
}
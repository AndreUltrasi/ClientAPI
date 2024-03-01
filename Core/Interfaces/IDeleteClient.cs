using Core.UseCases.DeleteClient.Boundaries;

namespace Core.Domain
{
    public interface IDeleteClient
    {
        Task<Output> Handle(DeleteClientInput input);
    }
}
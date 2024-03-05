using Core.UseCases.UpsertClient.Boundaries;

namespace Core.Interfaces.UseCases
{
    public interface IUpsertClient
    {
        Task<Output> Handle(UpsertClientInput input);
    }
}
using Core.UseCases.UpsertClient.Boundaries;

namespace Core.Domain
{
    public interface IUpsertClient
    {
        Task<Output> Handle(UpsertClientInput input);
    }
}
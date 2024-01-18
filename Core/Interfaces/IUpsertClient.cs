using Core.UseCases.UpsertClient.Boundaries;

namespace Core.Domain
{
    public interface IUpsertClient
    {
        Task Handle(UpsertClientInput input);
    }
}
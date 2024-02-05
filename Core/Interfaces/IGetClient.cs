using Core.UseCases.GetClient.Boundaries;

namespace Core.Domain
{
    public interface IGetClient
    {
        Task<Output> Handle(GetClientInput input);
    }
}
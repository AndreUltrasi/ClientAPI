using Core.UseCases.GetClient.Boundaries;

namespace Core.Interfaces.UseCases
{
    public interface IGetClient
    {
        Task<Output> Handle(GetClientInput input);
    }
}
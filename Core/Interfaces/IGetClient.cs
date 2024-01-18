using Core.UseCases.GetClient.Boundaries;

namespace Core.Domain
{
    public interface IGetClient
    {
        Task<Client> Handle(GetClientInput input);
    }
}
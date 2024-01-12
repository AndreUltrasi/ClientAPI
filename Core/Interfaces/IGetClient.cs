using Core.UseCases.GetClient.Boundaries;

namespace Core.Domain
{
    public interface IGetClient
    {
        void Handle(DeleteClientInput input);
    }
}
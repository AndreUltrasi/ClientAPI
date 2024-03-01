using Core.Domain;
using Core.ViewModel;

namespace Core.Mappers
{
    public static class ClientViewModelMappers
    {
        public static Client MapToDomain(this ClientViewModel clientViewModel)
        {
            return new Client(clientViewModel.AccountCode, clientViewModel.Active, clientViewModel.Name, clientViewModel.PersonType, clientViewModel.Age, clientViewModel.Gender, 
                              clientViewModel.Address, clientViewModel.Number, clientViewModel.Complement, clientViewModel.City, clientViewModel.Country,
                              clientViewModel.Cep, clientViewModel.Neighbourhood, clientViewModel.Uf);
        }
    }
}

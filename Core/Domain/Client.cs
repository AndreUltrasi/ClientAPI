using Core.Domain.Enums;
using Core.Dto;

namespace Core.Domain
{
    public class Client
    {
        public long AccountCode { get; private set; }
        public bool Active { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public PersonType PersonType { get; private set; }
        public int Age { get; private set; }
        public Gender Gender { get; private set; }
        public string Address { get; private set; } = string.Empty;
        public string Number { get; private set; } = string.Empty;
        public string? Complement { get; private set; }
        public string City { get; private set; } = string.Empty;
        public string Country { get; private set; } = string.Empty;
        public string Cep { get; private set; } = string.Empty;
        public string Uf { get; private set; } = string.Empty;
        public string Neighbourhood { get; private set; } = string.Empty;

        public Client()
        {
        }

        public Client(long accountCode, string name, PersonType personType, int age, Gender gender, string address, string number, string? complement, string city, string country, string cep, string neighbourhood, string uf)
        {
            AccountCode = accountCode;
            Name = name;
            PersonType = personType;
            Age = age;
            Gender = gender;
            Address = address;
            Number = number;
            Complement = complement;
            City = city;
            Country = country;
            Cep = cep;
            Uf = uf;
            Neighbourhood = neighbourhood;
        }

        public Client(long accountCode, bool active, string name, PersonType personType, int age, Gender gender, string number, string? complement, string cep)
        {
            AccountCode = accountCode;
            Active = active;
            Name = name;
            PersonType = personType;
            Age = age;
            Gender = gender;
            Number = number;
            Complement = complement;
            Cep = cep;
        }

        public void AddAddressProperties(AddressDto addressDto)
        {
            Address = string.IsNullOrWhiteSpace(Address) ? addressDto.Address : Address;
            City = string.IsNullOrWhiteSpace(City) ? addressDto.City : City;
            Neighbourhood = string.IsNullOrWhiteSpace(Neighbourhood) ? addressDto.Neighbourhood : Neighbourhood;
            Uf = string.IsNullOrWhiteSpace(Uf) ? addressDto.Uf : Uf;
            Country = string.IsNullOrWhiteSpace(Country) ? "Brasil" : Country;
        }
    }
}
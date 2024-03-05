using Core.Domain.Enums;
using System.ComponentModel;

namespace Core.ViewModel
{
    public class ClientViewModel
    {
        public long AccountCode { get; set; }

        [DefaultValue("")]
        public string Name { get; set; } = string.Empty;
        public PersonType PersonType { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }

        [DefaultValue("")]
        public string Address { get; set; } = string.Empty;
        [DefaultValue("")]
        public string Number { get; set; } = string.Empty;
        [DefaultValue("")]
        public string? Complement { get; set; }
        [DefaultValue("")]
        public string City { get; set; } = string.Empty;
        [DefaultValue("")]
        public string State { get; set; } = string.Empty;
        [DefaultValue("")]
        public string Country { get; set; } = string.Empty;
        [DefaultValue("")]
        public string Cep { get; set; } = string.Empty;
        [DefaultValue("")]
        public string Uf { get; set; } = string.Empty;
        [DefaultValue("")]
        public string Neighbourhood { get; set; } = string.Empty;
    }
}

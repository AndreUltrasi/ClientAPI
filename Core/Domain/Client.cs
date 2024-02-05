namespace Core.Domain
{
    public class Client
    {
        public long AccountCode { get; set; }

        public bool Active { get; set; }

        public string Name { get; set; }

        public PersonType PersonType { get; set; }

        public short Age { get; set; }

        public Gender Gender { get; set; }

        public string Adress { get; set; }

        public string Number { get; set; }

        public string Complement { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Cep { get; set; }
    }
}
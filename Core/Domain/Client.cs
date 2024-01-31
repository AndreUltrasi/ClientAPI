namespace Core.Domain
{
    public class Client
    {
        public Guid Id { get; set; }

        public bool Active { get; set; }

        public string Name { get; set; }

        public int PersonType { get; set; }

        public string Age { get; set; }

        public int Gender { get; set; }

        public string Adress { get; set; }

        public string Number { get; set; }

        public string Complement { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Cep { get; set; }


    }
}
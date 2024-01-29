namespace Core.Domain
{
    public class Address
    {


        public int Guid { get; set; }

        public bool Active { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public string Addresss { get; set; }

        public string Number { get; set; }

        public string Complement { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }
}
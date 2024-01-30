namespace Core.Domain
{
    public class Client
    {
        public Guid Id { get; set; }//Faz por padrão

        public bool Active { get; set; }

        public DateTime CreationDate { get; set; }//Faz por padrão pelo Entity

        public DateTime ModificationDate { get; set; }//Faz por padrão pelo Entity

        public string Name { get; set; }

        public int PersonType { get; set; }

        public string Age { get; set; }

        public int Gender { get; set; }

        public Guid AdressId { get; set; }//Remover

    }
}
using Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain
{
    [Table("CLIENTE")]
    public class ClientModel
    {
        [Key]
        [Column("ID"), Required]
        public Guid Id { get; set; }

        [Column("NUMERO_CONTA"), Required]
        public long AccountCode { get; set; }

        [Column("ATIVO"), Required]
        public bool Active { get; set; }

        [Column("NOME"), Required]
        public string Name { get; set; } = string.Empty;

        [Column("TIPO_PESSOA"), Required]
        public PersonType PersonType { get; set; }

        [Column("IDADE"), Required]
        public int Age { get; set; }

        [Column("GENERO"), Required]
        public Gender Gender { get; set; }

        [Column("ENDERECO"), Required]
        public string Adress { get; set; } = string.Empty;

        [Column("NUMERO"), Required]
        public string Number { get; set; } = string.Empty;

        [Column("COMPLEMENTO")]
        public string? Complement { get; set; }

        [Column("CIDADE"), Required]
        public string City { get; set; } = string.Empty;

        [Column("PAIS"), Required]
        public string Country { get; set; } = string.Empty;

        [Column("CODIGO_POSTAL"), Required]
        public string Cep { get; set; } = string.Empty;

        [Column("BAIRRO"), Required]
        public string Neighbourhood { get; set; } = string.Empty;

        [Column("UF"), Required]
        public string Uf { get; set; } = string.Empty;
    }
}
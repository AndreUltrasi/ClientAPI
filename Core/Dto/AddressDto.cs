using System.Text.Json.Serialization;

namespace Core.Dto
{
    public class AddressDto
    {
        [JsonPropertyName("logradouro")]
        public string Address { get; set; } = string.Empty;
        [JsonPropertyName("localidade")]
        public string City { get; set; } = string.Empty;
        [JsonPropertyName("uf")]
        public string Uf { get; set; } = string.Empty;
        [JsonPropertyName("bairro")]
        public string Neighbourhood { get; set; } = string.Empty;
    }
}

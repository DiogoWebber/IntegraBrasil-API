using System.Text.Json.Serialization;

namespace IntegraBrasilApi.Model{

public class EnderecoModel
{
    public class Root
    {
        [JsonPropertyName("cep")]
        public string? Cep { get; set; }

        [JsonPropertyName("state")]
        public string? Estado { get; set; }

        [JsonPropertyName("city")]
        public string? Cidade { get; set; }

        [JsonPropertyName("neighborhood")]
        public string? Regiao { get; set; }

        [JsonPropertyName("street")]
        public string? Rua { get; set; }

        [JsonPropertyName("service")]
        public string? Servico { get; set; }
    }
}}
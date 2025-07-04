using Newtonsoft.Json;
namespace WebApplication1.Models
{
    public class Vendedor
    {
        public required string name { get; set; } = string.Empty;
        public required string email { get; set; } = string.Empty;
        public required string senha { get; set; } = string.Empty;
        [JsonProperty("data_criacao")]
        public DateTime? data_criacao { get; set; }
        public required string image { get; set; } = string.Empty;
        public required string cnpj { get; set; } = string.Empty;
        public required string nomeloja { get; set; } = string.Empty;
        public required string descricaoloja { get; set; } = string.Empty;
        [JsonIgnore]
        public long vendedor_id { get; set; }
    }
}

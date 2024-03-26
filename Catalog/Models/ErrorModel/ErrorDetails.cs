using System.Text.Json;
using System.Text.Json.Serialization;

namespace Catalog.Models.ErrorModel
{
    public class ErrorDetails
    {
        //classe de modelo que irá servir para armazenarmos os valores dos erros.

        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Trace { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

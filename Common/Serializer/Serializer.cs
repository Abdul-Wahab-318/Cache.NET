using System.Text.Json;
using Common.Interfaces;

namespace Common.Serializer
{
    public class Serializer : ISerializer
    {
        public string Serialize<T>(T body)
        {
            string json_body = JsonSerializer.Serialize<T>(body);

            return json_body;
        }

        public T Deserialize<T>(string body)
        {
            T deserializedBody = JsonSerializer.Deserialize<T>(body);

            return deserializedBody;
        }
    }
}
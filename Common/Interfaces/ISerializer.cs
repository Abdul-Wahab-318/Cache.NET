
namespace Common.Interfaces
{
    public interface ISerializer
    {
        string Serialize<T>(T body);
        T Deserialize<T>(string body);
    }
}
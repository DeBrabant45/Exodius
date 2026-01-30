using System.Text.Json;

namespace AD.Exodius.Networks;

public interface INetworkResponse
{
    public Task<T> GetJsonResponseAsync<T>();
    public Task<byte[]> GetBodyAsync();
    public Task<JsonElement?> GetJsonResponseAsync();
    public Task<JsonElement?> GetPayloadAsync();
    public Task<string> GetRequestUrlAsync();
}

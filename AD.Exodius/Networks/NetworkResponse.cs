using System.Text.Json;

namespace AD.Exodius.Networks;

public class NetworkResponse : INetworkResponse
{
    private readonly Task<IResponse> _response;

    public NetworkResponse(Task<IResponse> response)
    {
        _response = response;
    }

    public async Task<T> GetJsonResponseAsync<T>()
    {
        var response = await _response;
        return await response.JsonAsync<T>();
    }

    public async Task<JsonElement?> GetJsonResponseAsync()
    {
        var response = await _response;
        return await response.JsonAsync();
    }

    public async Task<byte[]> GetBodyAsync()
    {
        var response = await _response;
        return await response.BodyAsync();
    }

    public async Task<JsonElement?> GetPayloadAsync()
    {
        var response = await _response;
        return response.Request?.PostDataJSON();
    }

    public async Task<string> GetRequestUrlAsync()
    {
        var response = await _response;
        return response.Request.Url;
    }
}

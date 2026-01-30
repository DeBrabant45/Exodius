using AD.Exodius.Networks;

namespace AD.Exodius.Drivers.Factories;

public class NetworkResponseFactory : INetworkResponseFactory
{
    public TNetworkResponse Create<TNetworkResponse>(Task<IResponse> response) where TNetworkResponse : INetworkResponse
    {
        var instance = Activator.CreateInstance(typeof(TNetworkResponse), response)
            ?? throw new InvalidOperationException($"Failed to create an instance of {typeof(TNetworkResponse).Name}.");

        return (TNetworkResponse)instance;
    }
}
using AD.Exodius.Components;
using AD.Exodius.Entities.Modals;

namespace AD.Exodius.Tests.Stubs.Entities.Modals;

public class StubInvalidModal : IModalEntity
{
    public void AddComponent<TPageComponent>() where TPageComponent : IEntityComponent
    {
        throw new NotImplementedException();
    }

    public void AssembleGraph()
    {
        throw new NotImplementedException();
    }

    public TPageComponent GetComponent<TPageComponent>() where TPageComponent : IEntityComponent
    {
        throw new NotImplementedException();
    }

    public List<TPageComponent> GetComponents<TPageComponent>() where TPageComponent : IEntityComponent
    {
        throw new NotImplementedException();
    }

    public void InitializeLazyComponents()
    {
        throw new NotImplementedException();
    }

    public void RemoveComponent<TPageComponent>() where TPageComponent : IEntityComponent
    {
        throw new NotImplementedException();
    }
}

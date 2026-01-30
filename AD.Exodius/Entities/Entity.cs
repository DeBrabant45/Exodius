using AD.Exodius.Components;
using AD.Exodius.Components.Attributes;
using AD.Exodius.Components.Factories;
using AD.Exodius.Drivers;
using AD.Exodius.Events;

namespace AD.Exodius.Entities;

public class Entity : IEntity
{
    protected readonly IDriver Driver;
    protected readonly IEventBus EventBus;
    private readonly HashSet<Type> _unregisteredComponentTypes;
    private readonly HashSet<IEntityComponent> _registeredComponents;

    public Entity(IDriver driver, IEventBus eventBus)
    {
        Driver = driver;
        EventBus = eventBus;
        _registeredComponents = [];
        _unregisteredComponentTypes = [];
    }

    public void AddComponent<TPageComponent>() where TPageComponent : IEntityComponent
    {
        _unregisteredComponentTypes.Add(typeof(TPageComponent));
    }

    public void AssembleGraph()
    {
        foreach (var componentType in _unregisteredComponentTypes)
        {
            Resolve(componentType, new Stack<Type>());
        }
        _unregisteredComponentTypes.Clear();
    }

    private void Resolve(Type type, Stack<Type> stack)
    {
        if (_registeredComponents.Any(c => type.IsAssignableFrom(c.GetType())))
            return;

        if (stack.Contains(type))
            throw new InvalidOperationException($"Cyclic dependency detected: {string.Join(" -> ", stack.Append(type).Select(t => t.Name))}");

        stack.Push(type);

        var dependencies = type.GetCustomAttributes(typeof(RequiresAttribute), true)
            .Cast<RequiresAttribute>()
            .Select(attr => attr.DependencyType);

        foreach (var dependencyType in dependencies)
        {
            if (!_registeredComponents.Any(c => dependencyType.IsAssignableFrom(c.GetType())))
            {
                var candidates = _unregisteredComponentTypes.Where(t => dependencyType.IsAssignableFrom(t)).ToList();

                if (candidates.Count == 0)
                    throw new InvalidOperationException($"No component found to satisfy dependency: {dependencyType.Name}");

                if (candidates.Count > 1)
                    throw new InvalidOperationException($"Multiple components found for dependency: {dependencyType.Name}, ambiguity detected.");

                Resolve(candidates[0], stack);
            }
        }

        var component = EntityComponentFactory.Create(type, Driver, this, EventBus);
        _registeredComponents.Add(component);
        _unregisteredComponentTypes.Remove(type);
        stack.Pop();
    }

    public TPageComponent GetComponent<TPageComponent>() where TPageComponent : IEntityComponent
    {
        var matches = _registeredComponents
            .OfType<TPageComponent>()
            .ToList();

        if (matches.Count == 0)
            throw new InvalidOperationException($"{typeof(TPageComponent).Name} does not exist in the registry.");

        if (matches.Count > 1)
            throw new InvalidOperationException($"Multiple components found for {typeof(TPageComponent).Name}. Use GetComponents<T>() instead.");

        return matches[0];
    }

    public List<TPageComponent> GetComponents<TPageComponent>() where TPageComponent : IEntityComponent
    {
        return _registeredComponents.OfType<TPageComponent>().ToList();
    }

    public void InitializeLazyComponents()
    {
        GetComponents<ILazyEntityComponent>()
            .ToList()
            .ForEach(lazyComponent => lazyComponent.Initialize());
    }

    public void RemoveComponent<TPageComponent>() where TPageComponent : IEntityComponent
    {
        _registeredComponents.Remove(GetComponent<TPageComponent>());
    }
}

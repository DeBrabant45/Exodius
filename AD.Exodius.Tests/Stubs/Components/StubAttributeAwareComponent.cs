using System.Reflection;
using AD.Exodius.Components;
using AD.Exodius.Drivers;
using AD.Exodius.Entities;
using AD.Exodius.Events;
using AD.Exodius.Tests.Stubs.Attributes;

namespace AD.Exodius.Tests.Stubs.Components;

public class AttributeAwareComponent : EntityComponent
{
    private readonly string? _attributeLabel;

    public AttributeAwareComponent(IDriver driver, IEntity owner, IEventBus eventBus)
        : base(driver, owner, eventBus) 
    {
        var attribute = Owner.GetType().GetCustomAttribute<MockAttribute>();
        _attributeLabel = attribute?.Label;
    }

    public string? GetMockLabelFromOwner()
    {
        return _attributeLabel;
    }
}

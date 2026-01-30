using NSubstitute;
using AD.Exodius.Components;
using AD.Exodius.Components.Attributes;
using AD.Exodius.Drivers;
using AD.Exodius.Entities;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Components;

public class ComponentExampleTest
{
    private readonly IDriver _mockDriver;
    private readonly IEventBus _mockEventBus;
    private readonly IPageEntity _mockPageEntity;

    public ComponentExampleTest()
    {
        _mockDriver = Substitute.For<IDriver>();
        _mockEventBus = Substitute.For<IEventBus>();
        _mockPageEntity = Substitute.For<IPageEntity>();
        _mockPageEntity.AddComponent<IDoer>();
        _mockPageEntity.AssembleGraph();
    }

    [Fact]
    public void Initialize_Should_SetDoerComponent_InExampleLazyComponent()
    {
        var doerMock = _mockPageEntity.GetComponent<IDoer>();
        var lazyComponent = new ExampleLazyComponent(_mockDriver, _mockPageEntity, _mockEventBus);

        lazyComponent.Initialize();
        lazyComponent.DoAction();

        doerMock.Received(1).DoSomething();
    }

    [Fact]
    public void DoAction_Should_InvokeDoSomething_OnExampleRequiredComponent()
    {
        var requiredComponent = new ExampleRequiredComponent(_mockDriver, _mockPageEntity, _mockEventBus);
        var doerMock = _mockPageEntity.GetComponent<IDoer>();

        requiredComponent.DoAction();

        doerMock.Received(1).DoSomething();
    }

    public interface IDoer : IEntityComponent
    {
        void DoSomething();
    }

    public class ExampleLazyComponent(IDriver driver, IEntity owner, IEventBus eventBus)
        : LazyEntityComponent(driver, owner, eventBus)
    {
        private IDoer? _doerComponent;

        public override void Initialize()
        {
            _doerComponent = Owner.GetComponent<IDoer>();
        }

        public void DoAction()
        {
            _doerComponent?.DoSomething();
        }
    }

    [Requires(typeof(IDoer))]
    public class ExampleRequiredComponent : EntityComponent
    {
        private readonly IDoer _doerComponent;

        public ExampleRequiredComponent(IDriver driver, IEntity owner, IEventBus eventBus)
            : base(driver, owner, eventBus)
        {
            _doerComponent = Owner.GetComponent<IDoer>();
        }

        public void DoAction()
        {
            _doerComponent.DoSomething();
        }
    }
}

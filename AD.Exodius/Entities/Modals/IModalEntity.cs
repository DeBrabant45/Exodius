namespace AD.Exodius.Entities.Modals;

/// <summary>
/// Represents a modal entity within the component graph architecture.
/// A modal entity defines the structure and behavior of modal-driven UI flows,
/// and participates in the same lifecycle and component resolution as page entities.
/// </summary>
/// <remarks>
/// Inherits from <see cref="IEntity"/>, enabling the modal to manage and resolve its own component graph.
/// This ensures modal-specific components can be initialized, orchestrated, and reused independently from page entities.
/// </remarks>
public interface IModalEntity : IEntity
{

}

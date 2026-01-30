using AD.Exodius.Entities.Modals;

namespace AD.Exodius.Components;

/// <summary>
/// Defines a component capable of opening a modal.
/// </summary>
/// <author>Aaron DeBrabant</author>
public interface IModalOpenComponent
{
    /// <summary>
    /// Opens a modal of the specified type.
    /// </summary>
    /// <typeparam name="TModalEntity">The type of the modal entity to open.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation of opening the modal.
    /// The task result contains an instance of the modal entity.
    /// </returns>
    public Task<TModalEntity> OpenModal<TModalEntity>() where TModalEntity : IModalEntity;
}

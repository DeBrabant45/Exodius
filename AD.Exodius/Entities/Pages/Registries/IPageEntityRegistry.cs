namespace AD.Exodius.Entities.Pages.Registries;

public interface IPageEntityRegistry
{
    void RegisterComponents<TPage>(TPage page) where TPage : IPageEntity;
}
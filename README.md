# Exodius

## Introduction
**Exodius** is a modular, extensible UI automation framework built on top of Playwright. It provides a clean, flexible **component-based model** for building test automation using **entities** and **components** rather than traditional rigid page object models.

This architecture decouples UI behaviors from their structure and treats pages, modals, and other composite UI elements as **entities** composed of **components**. It supports event-driven architecture and async workflows to simplify test interactions and improve maintainability.

---

## Features

- Component-based entity system for UI modeling  
- Fully async/await automation flow  
- Event bus support for decoupled behaviors  
- Attribute-driven metadata for DOM and route awareness  
- Flexible factory-based instantiation for components and entities  
- Playwright abstraction with DI-ready driver model  

---

## Getting Started

### Prerequisites

- **.NET SDK 8.0 or later**

### Installation Steps

1. **Clone the Repository**
    ```bash
    git clone https://github.com/DeBrabant45/Exodius.git
    ```

2. **Navigate to the Project Directory**
    ```bash
    cd Exodius
    ```

3. **Restore Dependencies**
    ```bash
    dotnet restore
    ```

4. **Build the Solution**
    ```bash
    dotnet build
    ```

5. **Install Playwright**
    ```bash
    pwsh Exodius/bin/Debug/net8.0/playwright.ps1 install
    ```

---

## Build and Test

### Build the Project
```bash
dotnet build
```

### Run Tests
```bash
dotnet test
```

---

## Example Usage

### Entity Modeling with Navigation

```csharp
[PageEntityMeta(
    Route = "/home.html",
    Name = "Home",
    DomId = "home-link",
    Registry = typeof(HomePageRegistry)
)]
public class HomePage : PageEntity
{ 
    public HomePage(IDriver driver, IEventBus eventBus)
        : base(driver, eventBus)
    {

    }
}

public class NavigationActionComponent(IDriver driver, IEntity owner, IEventBus evenBus)
    : EntityComponent(driver, owner, evenBus), INavigationActionComponent
{
    private ButtonElement BurgerMenuButton => Driver.FindElement<ById, ButtonElement>("react-burger-menu-btn");
    private LabelElement MenuWrapLabel => Driver.FindElement<ByXPath, LabelElement>("//div[@class='bm-menu-wrap']");

    public async Task ClickAction(string item)
    {
        if (await MenuWrapLabel.IsAttributePresent("aria-hidden", "false"))
            return;

        await BurgerMenuButton.Click();
    }
}

public class HomePageRegistry : IEntityRegistry
{
    public void RegisterComponents<TEntity>(TEntity entity) where TEntity : IEntity
    {
        entity.AddComponent<NavigationActionComponent>();
    }
}
```

### Navigation Flow with Entity

```csharp
var driverFactory = new DriverFactory();
var driver = driverFactory.Create(new DriverSettings
{
    BrowserSettings = new BrowserSettings
    {
        BrowserType = BrowserType.Chromium
    },
    ContextSettings = new ContextSettings
    {
        StorageStatePath = "path/to/cookies.json"
    }
});

var navigator = new NavigatorFactory().Create<Navigator>(driver);

await driver.OpenPage();
await driver.GoToUrl("https://example.com");

var productsPage = await navigator.GoTo<ProductsPage, ByAction>();

await driver.ClosePage();
```

---

## Component Architecture

The framework uses the following structure:

- `IEntity` → Any container of components (e.g. page, modal, section)  
- `IEntityComponent` → A reusable unit of behavior (e.g. a header, a button group)  
- `IEntityRegistry` → Registers the components required by an entity  
- `EventBus` → Supports decoupled communication between components and the owning entity  
- `EntityComponentFactory` → Instantiates components, resolving dependencies through the framework  

---

## Why Entity and Component?

This architecture provides clarity and flexibility:

- A **PageEntity** models a screen-level structure with routing info  
- A **ModalEntity** models a popup or dialog with contained components  
- Both are treated as `IEntity`, unifying how we interact with them  
- Avoids tight coupling and brittle code found in traditional Page Object Models  
- Naming allows for generalization, reusability, and expressiveness  

---

## Usage in Client Projects

Use Exodius as the foundation for your test automation:

- Inherit from `PageEntity` or `ModalEntity` to define screens and dialogs  
- Register required `EntityComponents` with attribute-based or registry-based registration  
- Use the built-in factories, navigation strategies, and driver configuration  

---

## Contribute

Contributions are welcome! To contribute:

1. Fork the repository  
2. Create a feature branch  
3. Commit and push your changes  
4. Open a pull request  

For more info, see the [GitHub contribution guidelines](https://docs.github.com/en/get-started/quickstart/contributing-to-projects).
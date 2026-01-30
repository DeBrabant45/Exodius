using Microsoft.Extensions.DependencyInjection;
using Mock.SwagLabs.Utilities;
using NUnit.Framework;

namespace Mock.SwagLabs.Tests.Fixtures;

/// <summary>
/// Provides a base class for test suites that require dependency injection and per-test scoped services.
/// Manages a service scope lifecycle and exposes resolved services to tests.
/// </summary>
public abstract class BaseTestStartup
{
    private IServiceScope _scope = null!;

    /// <summary>
    /// Called once before any tests are executed.
    /// Override to perform global setup logic for the test class.
    /// </summary>
    [OneTimeSetUp]
    public virtual Task OnOneTimeSetUpAsync()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Called once after all tests have finished.
    /// Override to perform global teardown logic for the test class.
    /// </summary>
    [OneTimeTearDown]
    public virtual Task OnOneTimeTearDownAsync()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Called before each test is executed.
    /// Initializes a new service scope and allows per-test setup logic.
    /// </summary>
    [SetUp]
    protected virtual Task OnSetUpAsync()
    {
        _scope = ServiceProviderFactory.CreateScope();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Called after each test has completed.
    /// Disposes the test's service scope and allows per-test cleanup logic.
    /// </summary>
    [TearDown]
    protected virtual Task OnTearDownAsync()
    {
        _scope.Dispose();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Resolves a required service from the current test scope.
    /// </summary>
    /// <typeparam name="TService">The type of service to retrieve.</typeparam>
    /// <returns>The resolved service instance.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the service scope is not initialized or the service is not registered.
    /// </exception>
    protected TService GetService<TService>() where TService : notnull
    {
        if (_scope is null)
        {
            throw new InvalidOperationException("The service scope is not initialized. Ensure OnSetUpAsync was executed.");
        }

        return _scope.ServiceProvider.GetRequiredService<TService>();
    }
}

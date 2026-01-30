using AD.Exodius.Configurations;

namespace AD.Exodius.Drivers.Services;

/// <summary>
/// Represents a service for managing diagnostic capturing processes during tests.
/// </summary>
/// <remarks>
/// This interface defines methods for starting and stopping the diagnostic capturing process
/// based on the test outcome and provides flexibility for storing diagnostics in specified folder paths.
/// </remarks>
public interface IDiagnosticsService
{
    /// <summary>
    /// Starts the diagnostic capturing process for the specified test.
    /// </summary>
    /// <param name="testName">The name of the test for which diagnostics are being captured.</param>
    /// <returns>A task representing the asynchronous operation of starting the diagnostic capturing process.</returns>
    public Task StartDiagnosticsAsync(string testName);

    /// <summary>
    /// Stops the diagnostic capturing process based on the test outcome and folder paths for storing diagnostics.
    /// </summary>
    /// <param name="testResults">An object containing the test results, including whether the test has failed, folder paths for storing diagnostics, and test parameters.</param>
    /// <returns>A task representing the asynchronous operation of stopping the diagnostic capturing process.</returns>
    public Task StopDiagnosticsAsync(TestResults testResults);
}

namespace AD.Exodius.Tests.Helpers;

public static class TaskExtensions
{
    /// <summary>
    /// Chains a task to another asynchronous job, transforming the result type.
    /// </summary>
    /// <typeparam name="TSource">The type of the result from the current task.</typeparam>
    /// <typeparam name="TResult">The type of the result from the next job.</typeparam>
    /// <param name="currentJob">The current task.</param>
    /// <param name="nextJob">The next job to run.</param>
    /// <returns>A task that represents the asynchronous operation, with the transformed result type.</returns>
    public static async Task<TResult> Then<TSource, TResult>(this Task<TSource> currentJob, Func<TSource, Task<TResult>> nextJob)
    {
        var result = await currentJob;
        return await nextJob(result);
    }

    /// <summary>
    /// Chains a task to another job, supporting both async and sync methods (when next job is void).
    /// </summary>
    /// <typeparam name="TSource">The type of the result from the current task.</typeparam>
    /// <param name="currentJob">The current task.</param>
    /// <param name="nextJob">The next job to run, can be either async or sync (void).</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task<TSource> Then<TSource>(this Task<TSource> currentJob, Func<TSource, Task> nextJob)
    {
        var result = await currentJob;

        if (nextJob != null)
        {
            await nextJob(result);
        }

        return result;
    }

    /// <summary>
    /// Chains a synchronous operation to an asynchronous job.
    /// </summary>
    /// <typeparam name="TSource">The type of the result from the sync operation.</typeparam>
    /// <typeparam name="TResult">The type of the result from the async operation.</typeparam>
    /// <param name="syncJob">The synchronous operation.</param>
    /// <param name="nextJob">The next asynchronous operation to run.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task<TResult> Then<TSource, TResult>(this TSource syncJob, Func<TSource, Task<TResult>> nextJob)
    {
        if (nextJob == null)
            throw new ArgumentNullException(nameof(nextJob));

        return await nextJob(syncJob);
    }

    /// <summary>
    /// Chains a synchronous operation to an asynchronous job with a void return type.
    /// </summary>
    /// <typeparam name="TSource">The type of the result from the sync operation.</typeparam>
    /// <param name="syncJob">The synchronous operation.</param>
    /// <param name="nextJob">The next asynchronous operation to run, returning void.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task Then<TSource>(this TSource syncJob, Func<TSource, Task> nextJob)
    {
        if (nextJob == null)
            throw new ArgumentNullException(nameof(nextJob));

        await nextJob(syncJob);
    }
}

using FluentAssertions.Collections;

namespace Mock.SwagLabs.Tests.Assertions;

public static class BeInOrderExtensions
{
    public static void BeInOrder(
        this StringCollectionAssertions assertions,
        SortOrder sortOrder)
    {
        var collection = assertions.Subject;
        if (collection == null)
            throw new ArgumentNullException(nameof(collection));

        var sortedCollection = SortCollection(collection, sortOrder);
        AssertCollectionOrder(collection, sortedCollection);
    }

    public static void BeInOrder<T>(
        this GenericCollectionAssertions<T> assertions,
        SortOrder sortOrder)
        where T : IComparable<T>
    {
        var collection = assertions.Subject;
        if (collection == null)
            throw new ArgumentNullException(nameof(collection));

        var sortedCollection = SortCollection(collection, sortOrder);
        AssertCollectionOrder(collection, sortedCollection);
    }

    private static IEnumerable<T> SortCollection<T>(IEnumerable<T> collection, SortOrder sortOrder) where T : IComparable<T>
    {
        return sortOrder switch
        {
            SortOrder.Ascending => collection.OrderBy(x => x).ToList(),
            SortOrder.Descending => collection.OrderByDescending(x => x).ToList(),
            _ => throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, "Invalid sort order")
        };
    }

    private static void AssertCollectionOrder<T>(IEnumerable<T> collection, IEnumerable<T> sortedCollection)
    {
        collection.Should().BeEquivalentTo(sortedCollection, options => options.WithStrictOrdering());
    }
}

public enum SortOrder
{
    Ascending,
    Descending
}
using System.Collections;
using System.Collections.Generic;

namespace SharpDomain.Querying
{
    public interface IPagedCollection<out T> : IReadOnlyCollection<T>
    {
        int PageIndex { get; }

        int PageSize { get; }

        int TotalPagesCount { get; }

        int TotalItemsCount { get; }

        bool IsFirstPage { get; }
        
        bool IsLastPage { get; }
    }
}

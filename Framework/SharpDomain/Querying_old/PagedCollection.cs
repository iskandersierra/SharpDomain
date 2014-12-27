using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SharpDomain.Querying
{
    public class PagedCollection<T> : IPagedCollection<T>
    {
        private readonly ICollection<T> _content;
        private readonly int _pageIndex;
        private readonly int _pageSize;
        private readonly int _totalPagesCount;
        private readonly int _totalItemsCount;

        public PagedCollection(IEnumerable<T> content, int pageIndex, int pageSize, int totalPagesCount, int totalItemsCount)
        {
            if (content == null) throw new ArgumentNullException("content");
            if (pageIndex < 0) throw new ArgumentOutOfRangeException("pageIndex");
            if (pageSize < 1) throw new ArgumentOutOfRangeException("pageSize");

            _content = content.ToArray();
            _pageIndex = pageIndex;
            _pageSize = pageSize;
            _totalPagesCount = totalPagesCount;
            _totalItemsCount = totalItemsCount;
        }

        public int PageIndex
        {
            get { return _pageIndex; }
        }

        public int PageSize
        {
            get { return _pageSize; }
        }

        public int TotalPagesCount
        {
            get { return _totalPagesCount; }
        }

        public int TotalItemsCount
        {
            get { return _totalItemsCount; }
        }

        public bool IsFirstPage
        {
            get { return PageIndex == 0; }
        }

        public bool IsLastPage
        {
            get { return PageIndex == TotalPagesCount - 1; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _content.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return _content.Count; }
        }
    }
}
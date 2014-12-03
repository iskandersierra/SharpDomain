using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.Querying
{
    public interface IDomainQuery
    {
        IQueryPaging Paging { get; set; }
    }

    public interface IQueryPaging
    {
        int PageIndex { get; }

        int PageSize { get; }
    }

    public class QueryPaging : IQueryPaging
    {
        public const int DefaultPageSize = 20;

        private readonly int _pageIndex;
        private readonly int _pageSize;

        public QueryPaging() : this(0, DefaultPageSize)
        {
        }

        public QueryPaging(int pageSize) : this(0, pageSize)
        {
        }

        public QueryPaging(int pageIndex, int pageSize)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException("pageIndex");
            if (pageSize < 1) throw new ArgumentOutOfRangeException("pageSize");

            _pageIndex = pageIndex;
            _pageSize = pageSize;
        }

        public int PageIndex
        {
            get { return _pageIndex; }
        }

        public int PageSize
        {
            get { return _pageSize; }
        }
    }
}

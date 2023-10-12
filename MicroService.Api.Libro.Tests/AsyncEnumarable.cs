using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace MicroService.Api.Libro.Tests
{
    public class AsyncEnumarable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public AsyncEnumarable(IEnumerable<T> enumarable): base(enumarable) { }

        public AsyncEnumarable(Expression expression) : base(expression) { }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider {
            get { return new AsyncQueryProvider<T>(this); }
        }
    }


}

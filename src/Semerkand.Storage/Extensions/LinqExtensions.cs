using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Storage.Extensions
{
    public static class LinqExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> whereClause)
        {
            if (condition)
            {
                return query.Where(whereClause);
            }
            return query;
        }
    }
}

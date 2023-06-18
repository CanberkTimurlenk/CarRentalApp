using Core.DataAccess.EntityFramework.Extensions;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Core.Entities.Abstract;

namespace DataAccess.Concrete.EntityFramework.Extensions
{
    public static class EfExtensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> source, string orderByQueryString)
            where T : IQuerySortable
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source;    //  null query params

            var orderQuery = OrderQueryBuilder<Car>.CreateOrderQuery(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source;    //  order query params does not match with properties of entity

            return source.OrderBy(orderQuery);

        }
    }
}

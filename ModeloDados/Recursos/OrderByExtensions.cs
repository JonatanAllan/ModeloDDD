using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ModeloDados.Recursos
{
    public static class OrderByExtensions
    {
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> enumerable, string orderBy) => enumerable.AsQueryable().OrderBy(orderBy).AsEnumerable();

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> collection, string orderBy) => ParseOrderBy(orderBy).Aggregate(collection, ApplyOrderBy);

        private static IQueryable<T> ApplyOrderBy<T>(IQueryable<T> collection, OrderByInfo orderByInfo)
        {
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            string[] props = orderByInfo.PropertyName.Split('.');
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }

            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            var methodName = !orderByInfo.Initial && collection is IOrderedQueryable<T>
                ? (orderByInfo.Direction == SortDirection.Ascending ? "ThenBy" : "ThenByDescending")
                : (orderByInfo.Direction == SortDirection.Ascending ? "OrderBy" : "OrderByDescending");

            return (IOrderedQueryable<T>)typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                        && method.IsGenericMethodDefinition
                        && method.GetGenericArguments().Length == 2
                        && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), type)
                .Invoke(null, new object[] { collection, lambda });
        }

        private static IEnumerable<OrderByInfo> ParseOrderBy(string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                yield break;
            }

            bool initial = true;
            string[] items = orderBy.Split(',');
            foreach (string item in items)
            {
                string[] pair = item.Trim().Split(' ');
                if (pair.Length > 2)
                {
                    throw new ArgumentException($"Invalid OrderBy string '{item}'. Order By Format: Property, Property2 ASC, Property2 DESC");
                }

                string prop = pair[0].Trim();
                if (string.IsNullOrEmpty(prop))
                {
                    throw new ArgumentException("Invalid Property. Order By Format: Property, Property2 ASC, Property2 DESC");
                }

                SortDirection dir = SortDirection.Ascending;
                if (pair.Length == 2)
                {
                    dir = "desc".Equals(pair[1].Trim(), StringComparison.OrdinalIgnoreCase)
                        ? SortDirection.Descending
                        : SortDirection.Ascending;
                }

                yield return new OrderByInfo { PropertyName = prop, Direction = dir, Initial = initial };
                initial = false;
            }
        }

        private class OrderByInfo
        {
            public string PropertyName { get; set; }
            public SortDirection Direction { get; set; }
            public bool Initial { get; set; }
        }

        private enum SortDirection
        {
            Ascending = 0,
            Descending = 1
        }
    }
}
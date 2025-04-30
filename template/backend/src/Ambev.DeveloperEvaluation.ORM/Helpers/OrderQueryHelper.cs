using System.Linq.Expressions;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM.Helpers
{
    public static class OrderQueryHelper
    {
        public static IQueryable<T> OrderQuery<T>(IQueryable<T> query, string order)
        {
            if (string.IsNullOrWhiteSpace(order))
                return query;

            var parameters = order.Split(',');

            foreach (var parameter in parameters)
            {
                var command = parameter.Trim().Split(' ');
                if (command.Length != 2)
                    continue;

                var propertyName = command[0].Trim();
                var direction = command[1].Trim().ToLower();

                if (direction != "asc" && direction != "desc")
                    continue;

               query = ApplyOrder(query, propertyName, direction);
            }

            return query;
        }


        private static IQueryable<T> ApplyOrder<T>(IQueryable<T> query, string propertyName, string direction)
        {
            var entityType = typeof(T);
            var propertyInfo = entityType.GetProperty(propertyName,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
                return query;

            var parameter = Expression.Parameter(entityType, "x");
            var propertyAccess = Expression.Property(parameter, propertyInfo);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            string methodName = direction == "asc" ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { entityType, propertyInfo.PropertyType };

            var resultExp = Expression.Call(
                typeof(Queryable),
                methodName,
                types,
                query.Expression,
                Expression.Quote(orderByExp));

            return query.Provider.CreateQuery<T>(resultExp);
        }
    }
}

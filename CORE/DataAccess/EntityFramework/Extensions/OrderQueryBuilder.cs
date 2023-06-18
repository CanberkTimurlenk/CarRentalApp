using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework.Extensions
{
    public class OrderQueryBuilder<T>
    {
        public static string CreateOrderQuery(string orderByQueryString)
        {
           
            var orderParams = orderByQueryString.Trim().Split(',').ToList();

            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(' ')[0];

                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty is null)
                    continue;

                var direction = param.EndsWith(" desc") ? "desc" : "asc";

                orderQueryBuilder.Append($"{objectProperty.Name} {direction},");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            return orderQuery;

        }
    }
}


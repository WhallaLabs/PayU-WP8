using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhallaLabs.Services.PayU.Helpers
{
    public class QueryCore
    {
        protected string _filedSeparator = ",";

        protected string ToQueryString(Dictionary<string, object> data)
        {
            return string.Join("&", data
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }

        protected virtual string SerializeObject(object data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            // Get all properties on the object
            var allPriperties = data.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(data, null) != null)
                .Where(x => !Attribute.IsDefined(x, typeof(QueryIgnoreAttribute)));

            var nonTaggedProperties = allPriperties.Where(x => !Attribute.IsDefined(x, typeof(QueryPropertyAttribute)))
                .ToDictionary(x => x.Name, x => x.GetValue(data, null));

            var taggedProperties = allPriperties.Where(x => Attribute.IsDefined(x, typeof(QueryPropertyAttribute)))
                .ToDictionary(x => ((IEnumerable<QueryPropertyAttribute>)x.GetCustomAttributes(typeof(QueryPropertyAttribute), true)).First().Name, x => x.GetValue(data, null));

            string result = string.Empty;

            if (nonTaggedProperties.Any())
            {
                result += ToQueryString(nonTaggedProperties);
            }

            if (taggedProperties.Any())
            {
                if (!string.IsNullOrEmpty(result))
                    result += "&";

                result += ToQueryString(taggedProperties);
            }

            return result;
        }
    }
}

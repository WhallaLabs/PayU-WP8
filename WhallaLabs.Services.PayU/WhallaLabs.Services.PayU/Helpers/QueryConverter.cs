using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhallaLabs.Services.PayU.Helpers
{
    public class QueryConverter : QueryCore
    {
        public string Serialize(object data)
        {
            return SerializeObject(data);
        }

        public object Deserialize(string data)
        {
            throw new NotImplementedException();
        }
    }
}

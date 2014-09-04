using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhallaLabs.Services.PayU.Helpers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class QueryPropertyAttribute : Attribute
    {
        public string Name { get; set; }
        public QueryPropertyAttribute() { }
        public QueryPropertyAttribute(string name)
        {
            Name = name;
        }
    }
}

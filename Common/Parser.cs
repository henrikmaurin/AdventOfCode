using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public static class Parser
    {
        public static T Parse<T>(this T obj, string data) where T : IParsed
        {
            if (obj.PropertyNames == null)
                throw new Exception("No property names");

            Regex regex = new Regex(obj.DataFormat);
            GroupCollection matches = regex.Match(data).Groups;

            for (int i = 0; i < obj.PropertyNames.Count(); i++)
            {
                PropertyInfo propertyInfo = obj.GetType().GetProperty(obj.PropertyNames[i]);
                string value = matches[i + 1].Value;
                var v = Convert.ChangeType(value, propertyInfo.PropertyType);
              
                propertyInfo.SetValue(obj, v);
            }

            return obj;
        }

        public interface IParsed
        {
            public string DataFormat { get; }
            public string[] PropertyNames { get; }
        }
    }
}
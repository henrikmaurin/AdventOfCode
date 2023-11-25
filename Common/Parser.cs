using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Common
{
    public static class Parser
    {
        public static T Parse<T>(this T obj, string data) where T : IInDataFormat
        {
            if (obj.PropertyNames == null)
                throw new Exception("No property names");

            Regex regex = new Regex(obj.DataFormat);
            GroupCollection matches = regex.Match(data).Groups;

            int offset = 0;

            if (matches.Count > 1 && matches[0].Value == data)
                offset = 1;

            for (int i = 0; i < obj.PropertyNames.Count(); i++)
            {
                PropertyInfo propertyInfo = obj.GetType().GetProperty(obj.PropertyNames[i]);
                string value = matches[i + offset].Value;
                var v = Convert.ChangeType(value, propertyInfo.PropertyType);

                propertyInfo.SetValue(obj, v);
            }

            return obj;
        }

        public static T ParseSingleDataPoint<T>(string data) where T : IInDataFormat
        {
            T obj = CreateObject<T>();

            return Parse<T>(obj, data);
        }

        public static T TransformDataPoint<T, U>(U parsedData) where T : IParsedDataFormat where U : IInDataFormat
        {
            T obj = CreateObject<T>();

            obj.Transform(parsedData);
            return obj;
        }       

        public static IEnumerable<T> ParseLineOfSingleChars2<T, U>(T a1, U a2,string data) where T : IParsedDataFormat where U : IInDataFormat
        {
            List<T> list = new List<T>();

            foreach (char c in data)
            {
                T transformedDataPoint = TransformDataPoint<T, U>(ParseSingleDataPoint<U>($"{c}"));
                list.Add(transformedDataPoint);
            }

            return list;
        }

        public static T ParseSingleData<T,U>(string data) where T: IParsedDataFormat where U : IInDataFormat
        {
            return TransformDataPoint<T,U>(ParseSingleDataPoint<U> (data));
        }

        public static IEnumerable<T> ParseLineOfSingleChars<T, U>(string data) where T : IParsedDataFormat where U : IInDataFormat
        {
            List<T> list = new List<T>();

            foreach (char c in data)
            {
                T transformedDataPoint = TransformDataPoint<T, U>(ParseSingleDataPoint<U>($"{c}"));
                list.Add(transformedDataPoint);
            }

            return list;
        }

        public static IEnumerable<T> ParseLinesDelimitedByNewline<T, U>(string data) where T : IParsedDataFormat where U : IInDataFormat
        {
            List<T> list = new List<T>();

            foreach (string s in data.SplitOnNewline())
            {
                T transformedDataPoint = TransformDataPoint<T, U>(ParseSingleDataPoint<U>(s));
                list.Add(transformedDataPoint);
            }

            return list;
        }

        public static IEnumerable<T> ParseLinesDelimitedByMultipleNewline<T,U>(string data) where T : IParsedDataFormat where U : IInDataFormat
        {
            List<T> list = new List<T>();

            foreach (string s in data.SplitOnDoubleNewline())
            {
                T transformedDataPoint = TransformDataPoint<T, U>(ParseSingleDataPoint<U>(s));
                list.Add(transformedDataPoint);
            }

            return list;
        }

        private static T CreateObject<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        private static object CreateObjectOfType(Type t)
        {
            return Activator.CreateInstance(t);
        }

        private static Type GetIndataType<T>()
        {
            return CreateObject<T>()?.GetType();
        }

        public interface IInDataFormat
        {
            public string DataFormat { get; }
            public string[] PropertyNames { get; }
        }

        public interface IParsedDataFormat
        {
            public void Transform(IInDataFormat data);
            public Type GetReturnType();
        }

        public class SingleIntegers : IParsedDataFormat {
            public class Parsed : IInDataFormat
            {
                public string DataFormat => @"\d+";

                public string[] PropertyNames => new string[] { nameof(Integer) };

                public int Integer { get; set; }
            }

            public Type GetReturnType()
            {
                return typeof(Parsed);
            }

            public int Value { get; set; }

            public void Transform(IInDataFormat data)
            {
                Parsed instructionData = (Parsed)data;
                Value = instructionData.Integer;
            }
        }

        public class SingleString : IParsedDataFormat
        {
            public static SingleString Parse(string data)
            {
                return ParseSingleData<SingleString, Parsed>(data);
            }

            public class Parsed : IInDataFormat
            {
                public string DataFormat => @"\w+";

                public string[] PropertyNames => new string[] { nameof(String) };

                public string String { get; set; }
            }

            public Type GetReturnType()
            {
                return typeof(Parsed);
            }

            public string Value { get; set; }

            public void Transform(IInDataFormat data)
            {
                Parsed instructionData = (Parsed)data;
                Value = instructionData.String;
            }
        }
    }
}
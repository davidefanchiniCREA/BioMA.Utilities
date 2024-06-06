using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JRC.IPSC.MARS.Utilities
{
    public class StringConverterRegistry
    {
        //the key is "class1 type - class2 type"
        private static Dictionary<string, IGenericValueConverter<string>> _converters;

        private static StringConverterRegistry _instance;

        private static object syncLock = new object();

        protected StringConverterRegistry()
        {
            _converters = new Dictionary<string, IGenericValueConverter<string>>();
            _converters.Add(typesToString(typeof(Int16)), new IntConverter());
            _converters.Add(typesToString(typeof(Int32)), new IntConverter());
            _converters.Add(typesToString(typeof(double)), new DoubleConverter());
            _converters.Add(typesToString(typeof(object)), new ObjectConverter());
            _converters.Add(typesToString(typeof(string)), new StringConverter());
            _converters.Add(typesToString(typeof(IList<string>)), new ListStringConverterSemicolumn());
            _converters.Add(typesToString(typeof(List<string>)), new ListStringConverterSemicolumn());
            _converters.Add(typesToString(typeof(IList<int>)), new ListIntConverterSemicolumn());
            _converters.Add(typesToString(typeof(List<int>)), new ListIntConverterSemicolumn());
            _converters.Add(typesToString(typeof(bool)), new BoolConverter());

        }


        /// <summary>
        /// syncronized singleton
        /// </summary>
        /// <returns></returns>
        public static StringConverterRegistry getInstance()
        {
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new StringConverterRegistry();
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// Add a new custom converter for this class type. If the converter already exists, it will be overwritten.
        /// </summary>
        /// <param name="classType">The class type</param>        
        /// <param name="converter">The custom converter</param>
        public void addCustomConverter(Type classType, IGenericValueConverter<string> converter)
        {
            if (_converters.ContainsKey(typesToString(classType)))
            {
                _converters.Remove(typesToString(classType));
            }
            _converters.Add(typesToString(classType), converter);
        }

        /// <summary>
        /// Returns the converter registered for this class type.
        /// If no converter for this type is registered, the ObjectConverter is returned.
        /// </summary>
        /// <param name="classType">The class type</param>      
        /// <returns></returns>
        public IGenericValueConverter<string> getConverter(Type classType)
        {

            IGenericValueConverter<string> c;


            _converters.TryGetValue(typesToString(classType), out  c);
            if (c == null) c = new ObjectConverter();
            return c;
        }


        private static string typesToString(Type t1)
        {
            if(t1==null || t1.AssemblyQualifiedName==null)throw new ArgumentNullException("Impossible to register a converter for a type null");
            return t1.AssemblyQualifiedName;
        }


        private class IntConverter : IGenericValueConverter<string>
        {
            public override object convert(string obj)
            {
                if(obj==null) return null;
                
                return int.Parse(obj);
            }

            public override string reverseConvert(object obj)
            {
                if (obj == null || !(obj is int))
                {
                    return null;
                }
                return ((int)obj).ToString();
            }
        }


        private class DoubleConverter : IGenericValueConverter<string>
        {
            public override object convert(string obj)
            {
                if(obj==null) return null;
                
                return double.Parse(obj);
            }

            public override string reverseConvert(object obj)
            {
                if (obj == null || !(obj is double))
                {
                    return null;
                }
                return ((double)obj).ToString();
            }
        }

        private class StringConverter : IGenericValueConverter<string>
        {
            public override object convert(string obj)
            {
                if (obj == null) return null;
                return obj;
            }

            public override string reverseConvert(object obj)
            {
                if (obj == null || !(obj is string)) return null;
                return obj.ToString();
            }
        }

        private class BoolConverter : IGenericValueConverter<string>
        {
            public override object convert(string obj)
            {
                if(obj!=null && obj.Equals("true"))
                {
                    return true;
                }
                return false;
            }

            public override string reverseConvert(object obj)
            {
                if (obj == null || !(obj is bool)) return null;
                return obj.ToString();
            }
        }

        public class ListStringConverter : IGenericValueConverter<string>
        {
            public ListStringConverter(String separator)
            {
                _separator = separator;
            }

            private string _separator ;

            public override object convert(string obj)
            {
                if (obj == null) return null;
                string[] strings = obj.Split(new string[] { _separator }, StringSplitOptions.RemoveEmptyEntries);
                IList<string> list = new List<string>();
                foreach (var s in strings)
                {
                    list.Add(s);   
                }
                return list;
            }

            public override string reverseConvert(object obj)
            {
                StringBuilder sb=new StringBuilder("");
                if (obj == null || !(obj is IList<string>)) return null;
                IList<string> strings = obj as IList<string>;
                foreach (string  s in strings)
                {
                    sb.Append(_separator);
                    sb.Append(s);
                }
                return sb.ToString();
            }
        }

        private class ListStringConverterSemicolumn : ListStringConverter
        {
            public ListStringConverterSemicolumn()
                : base(";")
            {
            }
        } 

        private class ListIntConverterSemicolumn:ListIntConverter
        {
            public ListIntConverterSemicolumn() : base(";".ToCharArray())
            {
            }
        } 

        public class ListIntConverter : IGenericValueConverter<string>
        {
            public ListIntConverter(char[] separator)
            {
                _separator = separator;
            }

            private char[] _separator;

            public override object convert(string obj)
            {
                if (obj == null) return null;
                string[] strings = obj.Split(_separator, StringSplitOptions.RemoveEmptyEntries);
                IList<int> list = new List<int>();
                foreach (var s in strings)
                {
                    list.Add(int.Parse(s));
                }
                return list;
            }

            public override string reverseConvert(object obj)
            {
                string result = "";
                if (obj == null || !(obj is IList<int>)) return null;
                IList<int> ints = obj as IList<int>;
                foreach (int s in ints)
                {
                    result = result + _separator + s;
                }
                return result;
            }
        }

        private class ObjectConverter : IGenericValueConverter<string>
        {
            public override object convert(string obj)
            {
                return obj;
            }

            public override string reverseConvert(object obj)
            {
                if (obj == null) return null;
                return obj.ToString();
            }
        }
    }
}
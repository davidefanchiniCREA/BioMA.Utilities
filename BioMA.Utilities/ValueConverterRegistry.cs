using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("JRC.IPSC.MARS.Utilities.Test")]
namespace JRC.IPSC.MARS.Utilities
{
    /// <summary>
    /// The class is used as a singleton registry, to register the set of converter to be used to convert domain classes of different datatypes.
    /// The singleton works on the key made by the couple of types: domainclass1 type - domainclass2 type.
    /// 
    /// Default converters:
    ///typeof(Int16), typeof(Int32)             JRC.IPSC.MARS.Utilities.Int16ToInt32Converter           
    ///typeof(Int32), typeof(Int16)             JRC.IPSC.MARS.Utilities.Int32ToInt16Converter          
    ///typeof(Decimal), typeof(Int16)             JRC.IPSC.MARS.Utilities.DecimalToInt16Converter         
    ///typeof(Decimal), typeof(Int64)             JRC.IPSC.MARS.Utilities.DecimalToInt64Converter         
    ///typeof(Decimal), typeof(Int32)             JRC.IPSC.MARS.Utilities.DecimalToIntConverter           
    ///typeof(object), typeof(DateTime)             JRC.IPSC.MARS.Utilities.ObjectToDatetimeConverter           
    ///typeof(decimal), typeof(bool)             JRC.IPSC.MARS.Utilities.DecimalToBoolConverter           
    ///typeof(int), typeof(double)             JRC.IPSC.MARS.Utilities.IntToDoubleConverter           
    ///typeof(int), typeof(double?)             JRC.IPSC.MARS.Utilities.IntToDoubleConverter           
    ///typeof(decimal), typeof(double)             JRC.IPSC.MARS.Utilities.DecimalToDoubleConverter           
    ///typeof(decimal), typeof(double?)             JRC.IPSC.MARS.Utilities.DecimalToDoubleConverter               
    /// </summary>
    public class ValueConverterRegistry
    {

        //the key is "domainclass1 type - domainclass2 type"
        private static Dictionary<string, IValueConverter> _converters;

        private static ValueConverterRegistry _instance;

        private static object syncLock = new object();

        protected ValueConverterRegistry()
        {
            _converters = new Dictionary<string, IValueConverter>();
            _converters.Add(typesToString(typeof(Int16), typeof(Int32)), new Int16ToInt32Converter());
            _converters.Add(typesToString(typeof(Int32), typeof(Int16)), new Int32ToInt16Converter());
            _converters.Add(typesToString(typeof(Decimal), typeof(Int16)), new DecimalToInt16Converter());
            _converters.Add(typesToString(typeof(Decimal), typeof(Int64)), new DecimalToInt64Converter());
            _converters.Add(typesToString(typeof(Decimal), typeof(Int32)), new DecimalToIntConverter());
            _converters.Add(typesToString(typeof(Int32), typeof(Decimal)), new IntToDecimalConverter());
            _converters.Add(typesToString(typeof(object), typeof(DateTime)), new ObjectToDatetimeConverter());
            _converters.Add(typesToString(typeof(decimal), typeof(bool)), new DecimalToBoolConverter());
            _converters.Add(typesToString(typeof(int), typeof(double)), new IntToDoubleConverter());
            _converters.Add(typesToString(typeof(int), typeof(double?)), new IntToDoubleConverter());
            _converters.Add(typesToString(typeof(decimal), typeof(double)), new DecimalToDoubleConverter());
            _converters.Add(typesToString(typeof(decimal), typeof(double?)), new DecimalToDoubleConverter());
            _converters.Add(typesToString(typeof(Decimal), typeof(string)), new DecimalToStringConverter());
            _converters.Add(typesToString(typeof(string), typeof(Decimal)), new StringToDecimalConverter());
            _converters.Add(typesToString(typeof(Int32), typeof(string)), new Int32ToStringConverter());
            _converters.Add(typesToString(typeof(string), typeof(Int32)), new StringToInt32Converter());
            _converters.Add(typesToString(typeof(double), typeof(Int32)), new DoubleToInt32Converter());
            

        }


        /// <summary>
        /// syncronized singleton
        /// </summary>
        /// <returns></returns>
        public static ValueConverterRegistry getInstance()
        {
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ValueConverterRegistry();
                    }
                }
            }
            return _instance;
        }


        /// <summary>
        /// Add a new custom converter for this couple of domainclass1 type and domainclass2 type. If the converter already exists, it will be overwritten.
        /// </summary>
        /// <param name="domainclass1Type">The domainclass1 type</param>
        /// <param name="domainclass2Type">The domainclass2 type</param>
        /// <param name="converter">Tha custom converter</param>
        public void addCustomConverter(Type domainclass1Type, Type domainclass2Type, IValueConverter converter)
        {
            if (_converters.ContainsKey(typesToString(domainclass1Type, domainclass2Type)))
            {
                _converters.Remove(typesToString(domainclass1Type, domainclass2Type));
            }
            _converters.Add(typesToString(domainclass1Type, domainclass2Type), converter);
        }

        /// <summary>
        /// Returns the converter registered for this cuuple of  domainclass1 type and domainclass2 type.
        /// If no converter for this couple is registered, the DoNothingConverter is returned.
        /// </summary>
        /// <param name="domainclass1Type">The domainclass1 type</param>
        /// <param name="domainclass2Type">The domainclass2 type</param>
        /// <returns></returns>
        public IValueConverter getConverter(Type domainclass1Type, Type domainclass2Type)
        {

            IValueConverter c;


            _converters.TryGetValue(typesToString(domainclass1Type, domainclass2Type), out  c);
            if (c == null) c = new DoNothingConverter();
            return c;
        }


        private string typesToString(Type t1, Type t2)
        {
            string uno;
            string due;
            if(t1.IsGenericType)
            {
                uno = t1.GetGenericArguments()[0].Name+t1.Name;
            }
            else
            {
                uno = t1.Name;
            }
            if(t2.IsGenericType)
            {
                due = t2.GetGenericArguments()[0].Name+t2.Name;
            }else
            {
                due = t2.Name;
            }
            return uno  + "-" + due;
        }
    }
}

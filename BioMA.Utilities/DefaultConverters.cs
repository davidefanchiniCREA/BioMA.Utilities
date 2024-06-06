using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace JRC.IPSC.MARS.Utilities
{
  
/// <summary>
    /// Represents a converter that does nothing and returns the input value as is.
    /// </summary>
    internal class DoNothingConverter : IValueConverter
    {
        public object convert(object o)
        {
            if (o == null || DBNull.Value.Equals(o)) return DBNull.Value;
            return o;
        }

        public object reverseConvert(object o)
        {
            if (o == null || DBNull.Value.Equals(o)) return DBNull.Value;
            return o;
        }
    }


    /// <summary>
    /// Represents a converter that converts decimal to string and vice versa.
    /// </summary>
    public class DecimalToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object reverseConvert(object obj)
        {
            string ooo = (string)obj;
            return decimal.Parse(ooo);
        }

        public object convert(object obj)
        {
            decimal ooo = (decimal)obj;
            return ooo.ToString();
        }

        #endregion
    }


    /// <summary>
    /// Represents a converter that converts string to decimal and vice versa.
    /// </summary>
    public class StringToDecimalConverter : IValueConverter
    {
        #region IValueConverter Members

        public object reverseConvert(object obj)
        {
            decimal ooo = (decimal)obj;
            return ooo.ToString();
        }

        public object convert(object obj)
        {
            string ooo = (string)obj;
            return decimal.Parse(ooo);
        }

        #endregion
    }

    /// <summary>
    /// Represents a converter that converts int32 to string and vice versa.
    /// </summary>
    public class Int32ToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object reverseConvert(object obj)
        {
            string ooo = (string)obj;
            return int.Parse(ooo);
        }

        public object convert(object obj)
        {
            decimal ooo = (int)obj;
            return ooo.ToString();
        }

        #endregion
    }


    /// <summary>
    /// Represents a converter that converts string to int32 and vice versa.
    /// </summary>
    public class StringToInt32Converter : IValueConverter
    {
        #region IValueConverter Members

        public object reverseConvert(object obj)
        {
            int ooo = (int)obj;
            return ooo.ToString();
        }

        public object convert(object obj)
        {
            string ooo = (string)obj;
            return int.Parse(ooo);
        }

        #endregion
    }

    /// <summary>
    /// Represents a converter that converts decimal to bool and vice versa.
    /// </summary>
    public class DecimalToBoolConverter : IValueConverter
    {
        #region IValueConverter Members

        public object reverseConvert(object obj)
        {
            bool ooo = (bool)obj;
            if (ooo == true) return (decimal)1;
            else return (decimal)0;
        }

        public object convert(object obj)
        {
            decimal ooo = (decimal)obj;
            if (ooo == 0) return false;
            else return true;
        }

        #endregion
    }


    /// <summary>
    /// Represents a converter that converts decimal to double and vice versa.
    /// </summary>
    public class DecimalToDoubleConverter : IValueConverter
    {
        #region IValueConverter Members

        public object convert(object obj)
        {
            decimal ooo = (decimal)obj;
            return System.Convert.ToDouble(ooo);
        }

        public object reverseConvert(object obj)
        {
            double ooo = (double)obj;
            return System.Convert.ToDecimal(ooo);
        }

        #endregion
    }


    /// <summary>
    /// Represents a converter that converts int32 to double and vice versa.
    /// </summary>
    public class IntToDoubleConverter : IValueConverter
    {
        #region IValueConverter Members

        public object convert(object obj)
        {
            int ooo = (int)obj;
            return System.Convert.ToDouble(ooo);
        }

        public object reverseConvert(object obj)
        {
            double ooo = (double)obj;
            return System.Convert.ToInt32(ooo);
        }

        #endregion
    }


    /// <summary>
    /// Represents a converter that converts double to int32 and vice versa.
    /// </summary>
    public class DoubleToInt32Converter : IValueConverter
    {
        IntToDoubleConverter _opposite;
        public DoubleToInt32Converter()
        {
            _opposite = new IntToDoubleConverter();
        }

        #region IValueConverter Members

        public object convert(object obj)
        {
            return _opposite.reverseConvert(obj);
        }

        public object reverseConvert(object obj)
        {
            return _opposite.convert(obj);
        }

        #endregion
    }

    /// <summary>
    /// Represents a converter that converts decimal to int and vice versa.
    /// </summary>
    internal class DecimalToIntConverter : IValueConverter
    {
        public object convert(object o)
        {
            if (o == null) return null;
            decimal oo = (decimal)o;
            return (int)Math.Ceiling(oo);
        }

        public object reverseConvert(object o)
        {
            if (o == null) return null;
            int oo = (int)o;
            return (decimal)oo;
        }
    }

    /// <summary>
    /// Represents a converter that converts int to decimal and vice versa.
    /// </summary>
    internal class IntToDecimalConverter : IValueConverter
    {
        public object convert(object o)
        {
            if (o == null) return null;
            if (o is Int32)
            {
                int oo = (int)o;
                return (decimal)oo;
            }
            if (o is Int64)
            {
                Int64 oo = (Int64)o;
                return (decimal)oo;
            }
            if (o is long)
            {
                long oo = (long)o;
                return (decimal)oo;
            }
            throw new Exception("IntToDecimalConverter error:value '" + o + "' is not an integer.");
        }

        public object reverseConvert(object o)
        {
            if (o == null) return null;
            decimal oo = (decimal)o;
            return (int)Math.Ceiling(oo);
        }
    }



    /// <summary>
    /// Represents a converter that converts int32 to int16 and vice versa.
    /// </summary>
    internal class Int32ToInt16Converter : IValueConverter
    {
        public object convert(object o)
        {
            Int32 oo = ((Int32)o);
            return (Int16)oo;
        }

        public object reverseConvert(object o)
        {
            Int16 oo = ((Int16)o);
            return (Int32)oo;
        }
    }


    /// <summary>
    /// Represents a converter that converts int16 to int32 and vice versa.
    /// </summary>
    internal class Int16ToInt32Converter : IValueConverter
    {
        public object convert(object o)
        {
            Int16 oo = ((Int16)o);
            return (Int32)oo;
        }

        public object reverseConvert(object o)
        {
            Int32 oo = ((Int32)o);
            return (Int16)oo;
        }
    }


    /// <summary>
    /// Represents a converter that converts decimal to int16 and vice versa.
    /// </summary>
    internal class DecimalToInt16Converter : IValueConverter
    {
        public object convert(object o)
        {
            if (o == null) return null;
            decimal oo = (decimal)o;
            return (Int16)Math.Ceiling(oo);
        }

        public object reverseConvert(object o)
        {
            if (o == null) return null;
            Int16 oo = (Int16)o;
            return (decimal)oo;
        }
    }

    /// <summary>
    /// Represents a converter that converts decimal to int64 and vice versa.
    /// </summary>
    internal class DecimalToInt64Converter : IValueConverter
    {
        public object convert(object o)
        {
            if (o == null) return null;
            decimal oo = (decimal)o;
            return (Int64)Math.Ceiling(oo);
        }

        public object reverseConvert(object o)
        {
            if (o == null) return null;
            Int64 oo = (Int64)o;
            return (decimal)oo;
        }
    }

    /// <summary>
    /// Represents a converter that converts object to DateTime and vice versa.
    /// </summary>
    internal class ObjectToDatetimeConverter : IValueConverter
    {
        public object convert(object o)
        {
            if (o == null) return null;
            DateTime dt = (DateTime)o;
            return dt;
        }

        public object reverseConvert(object o)
        {
            if (o == null) return DBNull.Value;
            return o;
        }
    }

    
}

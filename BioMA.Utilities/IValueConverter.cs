using System;
using System.Collections.Generic;
using System.Text;

namespace JRC.IPSC.MARS.Utilities
{
    /// <summary>
    /// The converter to convert from a domain class to another domain class
    /// 
    /// 
    /// DOMAINCLASS1  type ---> DOMAINCLASS2  type     use method IValueConverter.convert
    /// DOMAINCLASS2  type ---> DOMAINCLASS1  type    use method IValueConverter.reverseConvert
    /// 
    /// 
    /// </summary>
    public interface IValueConverter
    {
        /// <summary>
        /// Convert an object of  DOMAINCLASS1  type, into an object of  domain class DOMAINCLASS2  type
        /// </summary>
        /// <param name="obj">object of the DOMAINCLASS1   type</param>
        /// <returns>object of the DOMAINCLASS2   type</returns>
        object convert(object obj);


        /// <summary>
        /// Convert an object of  DOMAINCLASS2  type, into an object of  domain class DOMAINCLASS1 type
        /// </summary>
        /// <param name="obj">object of  DOMAINCLASS2 type</param>
        /// <returns>object of  DOMAINCLASS1  type</returns>
        object reverseConvert(object obj);

    }

    /// <summary>
    /// The converter to convert from a domain class to another domain class using generics
    /// 
    /// 
    /// DOMAINCLASS1 T1 type ---> DOMAINCLASS2 T2 type     use method IValueConverter.convert
    /// DOMAINCLASS2 T2 type ---> DOMAINCLASS1 T1 type    use method IValueConverter.reverseConvert
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public abstract class IGenericValueConverter<T1,T2>
    {


        /// <summary>
        /// Convert an object of  T1 type, into an object of  domain class T2  type
        /// </summary>
        /// <param name="obj">object of the T1   type</param>
        /// <returns>object of the T2   type</returns>
        public abstract T2 convert(T1 obj);


        /// <summary>
        /// Convert an object of  T2  type, into an object of  domain class T1 type
        /// </summary>
        /// <param name="obj">object of  T2 type</param>
        /// <returns>object of  T1  type</returns>
        public abstract T1 reverseConvert(T2 obj);


   
      

    }


    /// <summary>
    /// The converter to convert from a domain class (of type T1) to another domain class (unspecified type) using generics on the first domain class 
    /// 
    /// 
    /// DOMAINCLASS1 T1 type ---> DOMAINCLASS2  type     use method IValueConverter.convert
    /// DOMAINCLASS2  type ---> DOMAINCLASS1 T1 type    use method IValueConverter.reverseConvert
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>   
    public abstract class IGenericValueConverter<T1>
    {


        /// <summary>
        /// Convert an object of  T1 type, into an object of  domain class T2  type
        /// </summary>
        /// <param name="obj">object of the T1   type</param>
        /// <returns>object of DOMAINCLASS2   type</returns>
        public abstract object convert(T1 obj);


        /// <summary>
        /// Convert an object of  DOMAINCLASS2  type, into an object of  domain class T1 type
        /// </summary>
        /// <param name="obj">object of  DOMAINCLASS2 type</param>
        /// <returns>object of  T1  type</returns>
        public abstract T1 reverseConvert(object obj);


     


    }
}

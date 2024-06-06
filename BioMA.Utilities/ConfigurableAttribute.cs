using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioMA.Utilities.NetFramework
{

    /// <summary>
    /// Specifies that a class is configurable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ConfigurableAttribute : Attribute { }
}

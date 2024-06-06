using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioMA.Utilities.NetFramework
{
    /// <summary>
    /// Specifies that a property implies a configuration setting.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ImpliesConfigurationAttribute : Attribute
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioMA.Utilities.NetFramework
{
    /// <summary>
    /// Represents an attribute used to mark a property as a configuration item.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ConfigurationItemAttribute : Attribute
    {
    }
}

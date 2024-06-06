using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioMA.Utilities.NetFramework
{
    /// <summary>
    /// Specifies that the metadata for the property should be ignored.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreMetadataAttribute : Attribute
    {
    }
}

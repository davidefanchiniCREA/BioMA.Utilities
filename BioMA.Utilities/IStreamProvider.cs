using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioMA.Utilities.NetFramework
{
    /// <summary>
    /// Represents a provider for obtaining streams.
    /// </summary>
    public interface IStreamProvider
    {
        /// <summary>
        /// Gets a stream.
        /// </summary>
        /// <returns>The stream.</returns>
        Stream GetStream();
    }
}

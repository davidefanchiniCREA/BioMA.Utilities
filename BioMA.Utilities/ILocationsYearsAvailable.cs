using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JRC.IPSC.MARS.Utilities
{
    /// <summary>
    /// Identifies a source of lists of locations and years available for a model. Usually data providers implements this interface. For example the weather providers and the soil data providers should implement this interface.
    /// </summary>
    public interface ILocationsYearsAvailable
    {
        ///<summary>
        /// Returns a list of available locations
        ///</summary>
        ///<returns></returns>
        IEnumerable<string> GetAvailableLocations();

        ///<summary>
        /// Returns a list of available years
        ///</summary>
        ///<returns></returns>
        IEnumerable<int> GetAvailableYears();
    }
}
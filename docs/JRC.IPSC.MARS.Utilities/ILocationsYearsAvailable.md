# ILocationsYearsAvailable interface

Identifies a source of lists of locations and years available for a model. Usually data providers implements this interface. For example the weather providers and the soil data providers should implement this interface.

```csharp
public interface ILocationsYearsAvailable
```

## Members

| name | description |
| --- | --- |
| [GetAvailableLocations](ILocationsYearsAvailable/GetAvailableLocations.md)() | Returns a list of available locations |
| [GetAvailableYears](ILocationsYearsAvailable/GetAvailableYears.md)() | Returns a list of available years |

## See Also

* namespace [JRC.IPSC.MARS.Utilities](../BioMA.Utilities.md)

<!-- DO NOT EDIT: generated by xmldocmd for BioMA.Utilities.dll -->
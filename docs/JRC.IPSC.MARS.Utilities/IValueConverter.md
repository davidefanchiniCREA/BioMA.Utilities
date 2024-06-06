# IValueConverter interface

The converter to convert from a domain class to another domain class DOMAINCLASS1 type ---&gt; DOMAINCLASS2 type use method IValueConverter.convert DOMAINCLASS2 type ---&gt; DOMAINCLASS1 type use method IValueConverter.reverseConvert

```csharp
public interface IValueConverter
```

## Members

| name | description |
| --- | --- |
| [convert](IValueConverter/convert.md)(…) | Convert an object of DOMAINCLASS1 type, into an object of domain class DOMAINCLASS2 type |
| [reverseConvert](IValueConverter/reverseConvert.md)(…) | Convert an object of DOMAINCLASS2 type, into an object of domain class DOMAINCLASS1 type |

## See Also

* namespace [JRC.IPSC.MARS.Utilities](../BioMA.Utilities.md)

<!-- DO NOT EDIT: generated by xmldocmd for BioMA.Utilities.dll -->
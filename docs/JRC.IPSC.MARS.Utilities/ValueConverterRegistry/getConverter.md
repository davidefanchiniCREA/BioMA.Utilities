# ValueConverterRegistry.getConverter method

Returns the converter registered for this cuuple of domainclass1 type and domainclass2 type. If no converter for this couple is registered, the DoNothingConverter is returned.

```csharp
public IValueConverter getConverter(Type domainclass1Type, Type domainclass2Type)
```

| parameter | description |
| --- | --- |
| domainclass1Type | The domainclass1 type |
| domainclass2Type | The domainclass2 type |

## See Also

* interface [IValueConverter](../IValueConverter.md)
* class [ValueConverterRegistry](../ValueConverterRegistry.md)
* namespace [JRC.IPSC.MARS.Utilities](../../BioMA.Utilities.md)

<!-- DO NOT EDIT: generated by xmldocmd for BioMA.Utilities.dll -->
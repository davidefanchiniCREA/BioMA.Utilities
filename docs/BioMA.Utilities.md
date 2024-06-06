# BioMA.Utilities assembly

## BioMA.Utilities.NetFramework namespace

| public type | description |
| --- | --- |
| class [ConfigurableAttribute](./BioMA.Utilities.NetFramework/ConfigurableAttribute.md) | Specifies that a class is configurable. |
| class [ConfigurationItemAttribute](./BioMA.Utilities.NetFramework/ConfigurationItemAttribute.md) | Represents an attribute used to mark a property as a configuration item. |
| class [IgnoreMetadataAttribute](./BioMA.Utilities.NetFramework/IgnoreMetadataAttribute.md) | Specifies that the metadata for the property should be ignored. |
| class [ImpliesConfigurationAttribute](./BioMA.Utilities.NetFramework/ImpliesConfigurationAttribute.md) | Specifies that a property implies a configuration setting. |
| interface [ISimulationID](./BioMA.Utilities.NetFramework/ISimulationID.md) | Represents an interface for simulation identification. |
| interface [IStreamProvider](./BioMA.Utilities.NetFramework/IStreamProvider.md) | Represents a provider for obtaining streams. |
| class [MetadataIdAttribute](./BioMA.Utilities.NetFramework/MetadataIdAttribute.md) | Represents an attribute used to mark a property as a metadata identifier. |
| static class [SecurityUtils](./BioMA.Utilities.NetFramework/SecurityUtils.md) |  |

## JRC.IPSC.MARS.Utilities namespace

| public type | description |
| --- | --- |
| class [ClassCopier](./JRC.IPSC.MARS.Utilities/ClassCopier.md) | This class can be used to copy valueType(s) (properties or fields) from a source object to a target object, regardless of each one's type, as long as the fields (properties) to be copied have the same name and are public. This works even if we copy a field to a property or viceversa. The copy can be fail-fast (meaning it immediately throw Exception if something goes wrong with a field copy) or not, ignoring the simple copy that goes wrong and tracing the problem into a log. They can be also configured to copy only a list of fields (properties) with specified names. Finally, if the types of the source field (property) and target field (property) are different, a conversion between the two types is searched in the IValueConverters registered with the ValueConverterRegistry singleton. The IValueConverter can be registered both for convert or reverseConvert directions (i.e.: if we need a conversion from string to int, also a converter from int to string will do), because the suitable conversion is searched in both directions. |
| class [DecimalToBoolConverter](./JRC.IPSC.MARS.Utilities/DecimalToBoolConverter.md) | Represents a converter that converts decimal to bool and vice versa. |
| class [DecimalToDoubleConverter](./JRC.IPSC.MARS.Utilities/DecimalToDoubleConverter.md) | Represents a converter that converts decimal to double and vice versa. |
| class [DecimalToStringConverter](./JRC.IPSC.MARS.Utilities/DecimalToStringConverter.md) | Represents a converter that converts decimal to string and vice versa. |
| class [DoubleToInt32Converter](./JRC.IPSC.MARS.Utilities/DoubleToInt32Converter.md) | Represents a converter that converts double to int32 and vice versa. |
| static class [EnumerableOrderExtension](./JRC.IPSC.MARS.Utilities/EnumerableOrderExtension.md) | Extension methods for ordering enumerable collections. |
| class [HashList](./JRC.IPSC.MARS.Utilities/HashList.md) | Represents a collection of mapper configuration settings. |
| abstract class [IGenericValueConverter&lt;T1&gt;](./JRC.IPSC.MARS.Utilities/IGenericValueConverter-1.md) | The converter to convert from a domain class (of type T1) to another domain class (unspecified type) using generics on the first domain class DOMAINCLASS1 T1 type ---&gt; DOMAINCLASS2 type use method IValueConverter.convert DOMAINCLASS2 type ---&gt; DOMAINCLASS1 T1 type use method IValueConverter.reverseConvert |
| abstract class [IGenericValueConverter&lt;T1,T2&gt;](./JRC.IPSC.MARS.Utilities/IGenericValueConverter-2.md) | The converter to convert from a domain class to another domain class using generics DOMAINCLASS1 T1 type ---&gt; DOMAINCLASS2 T2 type use method IValueConverter.convert DOMAINCLASS2 T2 type ---&gt; DOMAINCLASS1 T1 type use method IValueConverter.reverseConvert |
| interface [ILocationsYearsAvailable](./JRC.IPSC.MARS.Utilities/ILocationsYearsAvailable.md) | Identifies a source of lists of locations and years available for a model. Usually data providers implements this interface. For example the weather providers and the soil data providers should implement this interface. |
| class [Int32ToStringConverter](./JRC.IPSC.MARS.Utilities/Int32ToStringConverter.md) | Represents a converter that converts int32 to string and vice versa. |
| class [IntToDoubleConverter](./JRC.IPSC.MARS.Utilities/IntToDoubleConverter.md) | Represents a converter that converts int32 to double and vice versa. |
| class [ItemOverwriteException&lt;T&gt;](./JRC.IPSC.MARS.Utilities/ItemOverwriteException-1.md) | Thrown by [` OrderedSetOnceFixedKeyEnumerable.Set(TKey, TValue)`](./JRC.IPSC.MARS.Utilities/OrderedSetOnceFixedKeyEnumerable-2/Set.md) when an attempnt is made to overwrite a Value already set under a given key. |
| interface [IValueConverter](./JRC.IPSC.MARS.Utilities/IValueConverter.md) | The converter to convert from a domain class to another domain class DOMAINCLASS1 type ---&gt; DOMAINCLASS2 type use method IValueConverter.convert DOMAINCLASS2 type ---&gt; DOMAINCLASS1 type use method IValueConverter.reverseConvert |
| class [MapperConfigurationSetting](./JRC.IPSC.MARS.Utilities/MapperConfigurationSetting.md) | Represents the configuration settings for the mapper. |
| class [MapperConfigurationSettings](./JRC.IPSC.MARS.Utilities/MapperConfigurationSettings.md) | Represents the configuration settings for the mapper. |
| class [ModelingSolutionsConfig](./JRC.IPSC.MARS.Utilities/ModelingSolutionsConfig.md) |  |
| class [ModelingSolutionsConfigAdd](./JRC.IPSC.MARS.Utilities/ModelingSolutionsConfigAdd.md) |  |
| class [OrderedSetOnceFixedKeyEnumerable&lt;TKey,TValue&gt;](./JRC.IPSC.MARS.Utilities/OrderedSetOnceFixedKeyEnumerable-2.md) | Represents a dictionary in which the keys are fixed at instantiation time, and for which, once a value has been configured for a key, it is not possibile to overwrite it. Guarantees iteration of the keys in the order they are configured in the constructor |
| class [ParametersComponentDescriptionsConfig](./JRC.IPSC.MARS.Utilities/ParametersComponentDescriptionsConfig.md) |  |
| class [ParametersComponentDescriptionsConfigComponentDescription](./JRC.IPSC.MARS.Utilities/ParametersComponentDescriptionsConfigComponentDescription.md) |  |
| static class [ParametersComponentDescriptionsConfigReader](./JRC.IPSC.MARS.Utilities/ParametersComponentDescriptionsConfigReader.md) | Utility to read the modeling solutions configuration file ('ModelingSolutionsConfig.xml' , stored int he application install directory). |
| static class [PathSanitizer](./JRC.IPSC.MARS.Utilities/PathSanitizer.md) | Cleans paths of invalid characters. |
| class [ReadCSVByRowUtil](./JRC.IPSC.MARS.Utilities/ReadCSVByRowUtil.md) | Helper to read _csvStream files row by row (for big files) |
| class [ReadCSVUtil](./JRC.IPSC.MARS.Utilities/ReadCSVUtil.md) | Helper to read csv files and store the file content into a DataTable |
| class [ReadOnlyDictionary&lt;TKey,TValue&gt;](./JRC.IPSC.MARS.Utilities/ReadOnlyDictionary-2.md) | Read-only wrapper for IDictionary&lt;TKey, TValue&gt;, backed by the modifiable IDictionary&lt;TKey, TValue&gt; passed to the constructor. This means that if the wrapped IDictionary&lt;TKey, TValue&gt; is modified, modifications are reflected in this instance. |
| class [ReadOnlyHashSet&lt;T&gt;](./JRC.IPSC.MARS.Utilities/ReadOnlyHashSet-1.md) | Read-only wrapper for HashSet&lt;T&gt;, backed by the modifiable HashSet&lt;T&gt; passed to the constructor. This means that if the wrapped HashSet&lt;T&gt; is modified, modifications are reflected in this instance. |
| class [RelativePathUtility](./JRC.IPSC.MARS.Utilities/RelativePathUtility.md) |  |
| enum [SortDirection](./JRC.IPSC.MARS.Utilities/SortDirection.md) |  |
| class [StringConverterRegistry](./JRC.IPSC.MARS.Utilities/StringConverterRegistry.md) |  |
| class [StringToDecimalConverter](./JRC.IPSC.MARS.Utilities/StringToDecimalConverter.md) | Represents a converter that converts string to decimal and vice versa. |
| class [StringToInt32Converter](./JRC.IPSC.MARS.Utilities/StringToInt32Converter.md) | Represents a converter that converts string to int32 and vice versa. |
| static class [TraceUtilities](./JRC.IPSC.MARS.Utilities/TraceUtilities.md) |  |
| static class [Util](./JRC.IPSC.MARS.Utilities/Util.md) |  |
| class [ValueConverterRegistry](./JRC.IPSC.MARS.Utilities/ValueConverterRegistry.md) | The class is used as a singleton registry, to register the set of converter to be used to convert domain classes of different datatypes. The singleton works on the key made by the couple of types: domainclass1 type - domainclass2 type. Default converters: typeof(Int16), typeof(Int32) JRC.IPSC.MARS.Utilities.Int16ToInt32Converter typeof(Int32), typeof(Int16) JRC.IPSC.MARS.Utilities.Int32ToInt16Converter typeof(Decimal), typeof(Int16) JRC.IPSC.MARS.Utilities.DecimalToInt16Converter typeof(Decimal), typeof(Int64) JRC.IPSC.MARS.Utilities.DecimalToInt64Converter typeof(Decimal), typeof(Int32) JRC.IPSC.MARS.Utilities.DecimalToIntConverter typeof(object), typeof(DateTime) JRC.IPSC.MARS.Utilities.ObjectToDatetimeConverter typeof(decimal), typeof(bool) JRC.IPSC.MARS.Utilities.DecimalToBoolConverter typeof(int), typeof(double) JRC.IPSC.MARS.Utilities.IntToDoubleConverter typeof(int), typeof(double?) JRC.IPSC.MARS.Utilities.IntToDoubleConverter typeof(decimal), typeof(double) JRC.IPSC.MARS.Utilities.DecimalToDoubleConverter typeof(decimal), typeof(double?) JRC.IPSC.MARS.Utilities.DecimalToDoubleConverter |

<!-- DO NOT EDIT: generated by xmldocmd for BioMA.Utilities.dll -->

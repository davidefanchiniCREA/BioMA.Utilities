# BioMA.Utilities

This package provides utilities functionalities to the [BioMA modelling framework](https://en.wikipedia.org/wiki/BioMA).

## Usage
Simply refer to this NuGet package in projects.

## License

This package is licensed under the [MIT License](https://licenses.nuget.org/MIT).

## Examples

The whole documentation is available [here](https://dev.azure.com/BioMA-NET8/BioMA.Utilities/_git/BioMA.Utilities?path=/docs/BioMA.Utilities.md). Follwing are examples on how to use some of the most important objects

## ReadOnlyDictionary

It provides a [ReadOnlyDictionary](BioMA.Utilities\ReadOnlyDictionary.cs) to avoid changing key value pairs:

```
Dictionary<string, int> backDictionary = new Dictionary<string, int>();

ReadOnlyDictionary<string, int> readOnly = new ReadOnlyDictionary(backDictionary);
```

Modifications to `backDictionary` will be reflected in `readOnly`, but it will not be possible to modify entries via the `readOnly` handle.

## Configuration Attributes

In the context of Models configuration serialization, instances and their properties that must be serialized and deserialized have to be flagged with attributes defined in this package, respectively:

1. The [Configurable](BioMA.Utilities\ConfigurableAttribute.cs) attribute for types

```
...
[Configurable]
public class Model {
	...
}
...
```

2. The [ConfigurationItem](BioMA.Utilities\ConfigurationItemAttribute.cs) attribute for properties

```
...
[Configurable]
public class Model {
	...
	[ConfigurationItem]
	public List<int> Years { get; set; }
	...
}
...
```
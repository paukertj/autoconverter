# Autoconverter
Autconverter is library for automatic mapping based on [Roslyn Code Generators](https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview) and [Dependency Injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection).

## :warning: :construction: **Warning** :construction: :warning:
**This package is under development and from that reason it is in pre-release stage. Public API of this package could be changed any time. The package can also contains some untested features and bugs.**

## :dart: Aims
- Provide compile time safe mapping (if mapping is incorrect, code won't compile)
- Create mappers with native code performance

## :wrench: How to use it
### 1. Download package
The package is currently in pre-release stage, so use ``prerelease`` flag or similar option in your nuget client.
```
dotnet add package Paukertj.Autoconverter.Generator --prerelease
```

### 2. Create composition file
Composition file is the place, that allows you 
```csharp
public static partial class DiCompositorAutomapping
{
    public static IServiceCollection AddAutomapping(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoconverter(); // Register autoconverter service
        serviceCollection.AddAutomappingInternal(); // Register generated services

        return serviceCollection;
    }

    [AutoconverterWiringEntrypoint] // This attribute will notify Autoconverter
    static partial void AddAutomappingInternal(this IServiceCollection serviceCollection);
}
```

### 3. Register
Now you can register Autoconverter by your composition file in Dependency Injection container:
```csharp
using IHost host = Host.CreateDefaultBuilder(args)
	.ConfigureServices((_, services) =>
		services
			.AddAutomapping()) // This is the extension method from your composition file
	.Build();
```

### 4. Create map
To start using Autconverter just inject ``IConvertingService``. This service provides ``Convert<TFrom, TTo>`` method, that provides conversion for you. You don't have to create maps, only thing that you have to do is to define source and target type.
```csharp
public class MyDependencyInjectedService : IMyDependencyInjectedService
{
    private readonly IConvertingService _convertingService;
    
    public MyDependencyInjectedService(IConvertingService convertingService)
    {
        _convertingService = convertingService;
    }

    public SomeObjectDto GetDto(SomeObject someObject)
    {
        // Here the conversion happens
        return _convertingService.Convert<SomeObject, SomeObjectDto>(someObject);
    }
}

public record SomeObjectDto
{
    public Guid Id { get; init; }

    public string Name { get; init; }
}

public record SomeObject
{
    public Guid Id { get; init; }

    public string Name { get; init; }
}
```
To use this API, your objects have to be mappable, that means that you have to be able map all properties from ``TFrom`` or you have to be able to map all properties from ``TTo``. So for example:
```csharp
public record From
{
    public Guid Id { get; init; }
}

public record To
{
    public Guid Id { get; init; }

    public string Name { get; init; }
}
```
This you can automatically map, because you can fill all properties from ``From`` record. This example:
```csharp
public record From
{
    public Guid Id { get; init; }

    public string Name { get; init; }
}

public record To
{
    public string Name { get; init; }
}
```
Will also work, because you can map all properties in ``To`` record but situation like this:
```csharp
public record From
{
    public Guid FromId { get; init; }
}

public record To
{
    public Guid ToId { get; init; }
}
```
Will throw ``AM0002`` exception during compilation, because it is not possible to automatically decide, what exactly you want to map.

### 4. Create custom converter
For situation, where you can not use automatic conversion, you can implement custom converter using ``IConverter<TFrom, TTo>`` interface:
```csharp
internal sealed class MyCustomConverter : IConverter<From, To>
{
    public To Convert(From from)
    {
        return new To
        {
            ToId = from.FromId
        };
    }
}

public record From
{
    public Guid FromId { get; init; }
}

public record To
{
    public Guid ToId { get; init; }
}
```
You do not have to register this converter anywhere. Autoconverter analyze your project during compilation and will collect your custom converters. The converter definition works globaly, so Autoconverter will know that it is possible to use your converter in the case of all conversions. It is good to know, that each converter (including the generated converters) is dependency injected service. So you can use them independently if you wish:
```csharp
public class MyDependencyInjectedService : IMyDependencyInjectedService
{
    private readonly IConverter<From, To> _myCustomConverter;
    
    public MyDependencyInjectedService(IConverter<From, To> myCustomConverter)
    {
        _myCustomConverter = myCustomConverter;
    }
}
```
But you can also access them using IConvertingService which is recommended. 

## :round_pushpin: Road map to version 0.1.0
- ``Dictionary`` support :x:
- Better support for ``IList`` and similar structures :x:
- Support for cross type conversion (from ``int`` to ``string`` etc.) :x:
- Support for nullable types :heavy_check_mark:
- Support for basic 1:1 conversion :heavy_check_mark:
- Better API for ignoring properties :x:
- API for trageting mapping (be able to map Property1 to Property2 without custom converter) :x:
- Support for nested types :heavy_check_mark:
- 100% test coverage :x:
- 100% documentation coverage :x:
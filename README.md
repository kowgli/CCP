# Conventional Command line Parser (CCP)

## Installing via NuGet

```
Install-Package CCP
```

[NuGet link](https://www.nuget.org/packages/CCP)

## Changes

### Version 1.5.0: 

1. Added **Description** attribute, making it possible to add textual description in the display help text.

### Version 1.4.1: 

1. Supporting arguments in Operation base classes.

### Version 1.3: 

1. ```Guid```, ```Nullable<Guid>``` and ```Nullable<DateTime>``` support.
1. Operation and property alias support.

## Description

Yet another command line argument parser for .NET. 
What makes it different is that it's basically zero configuration and 100% reflection based.
From a high level it works almost like a deserializer. It creates instances of classes based on the parameters supplied by the user and then runs them.

It is highly based on code and argument syntax conventions in order to minimize (or even remove) and configuration code. 
Yes, it doesn't follow popular syntax for command line arguments, but I've assumed that as a developer of an application you can define those as you wish 
and what's supported is simple and easy to understand.

Looking at it with developer eyes, the syntax is basically:
```
Class1 Property1=Value1 Property2=Value2 Class2 Property3=Value3    etc.
```  
**There shoudn't be any spaces between the name and value of a property.** Strings with whitespace should be enclosed by double quotes, like `"hello world"`.

There is no command line parsing specific code required. All you need is classes implementing the `IOperation` interface, which includes a single `Run` method.

Let's assume you program has two classes:
```
public class RecalculateTax : IOperation
{
    [Required]
    public int YearFrom { get; set; }

    public int? YearTo { get; set; }

    public void Run()
    {
        Console.WriteLine($"Running RecalculateTax with YearFrom={YearFrom}, YearTo={YearTo}");
    }
}

public class UpdateStatistics : IOperation
{
    public string Name { get; set; }

    public void Run()
    {
        Console.WriteLine($"Updating statistic {Name}");
    }
}
```

It can be then executed by running:
```
MyProgram.exe RecalculateTax YearFrom=2015 YearTo=2019 UpdateStatistics Name="my statistic" RecalculateTax YearFrom=2019
```

This will create:
- an instance of the `RecalculateTax` class with `YearFrom = 2015` and `YearTo = 2019`
- an instance of the `UpdateStatistics` class with `Name = "my statistic"`
- an instance of the `RecalculateTax' class `YearFrom = 2019` and `YearTo = null`

... and run them in that order.

So the output will be something like
```
Running RecalculateTax with YearFrom=2015, YearTo=2019
Updating statistic my statistic
Running RecalculateTax with YearFrom=2019, YearTo=
```

That's it. This allows you to quickly create code that can be parameterized by the user.

All the string comparisons done by CCP are **case insensitive**, so it doesn't matter if the user types `ReCALculateTAX yearFROM=2019` or `recalculatetax yearfrom=2019`

**See** the `Samples` folder for more examples. 

## How to use it?

A basic example would be to write your `Main` method like this:

```
private static void Main(string[] args)
{
    try
    {
        CCP.Executor.ExecuteFromArgs(args, typeof(Program).Assembly);
    }
    catch(CCP.CommandParsingException cpe)
    {
        System.Console.WriteLine(cpe.Message);
    }
}
```

`CCP.Executor.ExecuteFromArgs` is where all the "magic" happens. It has two arguments:

1. The actual arguments of the application
2. The assembly in which it should look for the "runnable" classes implementing the `IOperation` interface.

If anything bad happens during parsing (**but not execution - it doesn't catch application specific exceptions**), it will throw an `CommandParsingException`. 
This has two properties of interest:

1. The `Message` will contain a short description of the possible arguments and details of any parsing error (to be shown to the user).
2. The `InnerException` will contain a specialized exception with details about what went wrong (to be used by the developer).

Because running the application with no arguments at all is also considered an error (if not why would you use CCP?), simply running the above code `MyApplication.exe` would output:

```
MyApplication v1.0.0.0

ERROR
-----
Command doesn't have any operations.

POSSIBLE PARAMETERS
-------------------
RecalculateTax
        YearFrom=<Int32> [REQUIRED]
        YearTo=<Int32>

UpdateStatistics
        Name=<String>
```

## Configuration
There are a few simple configuration options, none of which is required.

The first one is the `[CCP.Attributes.Required]` attribute. Which marks a property as required and will throw an error if a value was not provided by the user.

The second is an optional `CCP.Options` class, an instance of which you can pass as a 3rd argument to the `CCP.Executor.ExecuteFromArgs` method. It has the following properties:
* `Locale` - the locale which should be used to parse the arguments (like decimal separators etc.).
The default is **en-US**, so without any configuration decimals should be separated using dots and dates should use the funny US format.
This was done because English is the de facto standard for computing and the en-US locale is also default in most programming languages.
* `DateFormat` - optional "hard coded" date format to be used for parsing dates (using `DateTime.ParseExact`). This could have a value like `"yyyy-MM-dd"` if you prefer the most logical format.
* `ArrayElementSeparator` - defines the separator used to separate array elements. The default value is a semicolon `;`.

## Supported data types
The following basic types found in the .NET framework are supported for properties:

- string
- sbyte
- sbyte?
- short
- short?
- int
- int? 
- long 
- long?
- byte 
- byte?
- ushort
- ushort?
- uint 
- uint?
- ulong
- ulong?
- decimal
- decimal
- float
- float?
- double
- double?
- char 
- char?
- bool 
- bool?
- DateTime
- DateTime?
- Guid
- Guid?

### Arrays

As of version 1.1 arrays are also supported. The default separator for array elements is a semicolon `;`, but it can be changed in the options. A semicolon was chosen instead of a comma `,` because it conflicts less with JSON used for complex types. 
If someone is able to come up with a clever regular expressions which can split a string into chunks, not only ignoring string literals (enclosed in double quotes (`"my text; another text"`) but also the character inside JSON expressions - please make a PR.

### Working with strings

When using a simple string type property it's value can be passed in two ways:
1. If it's a single word (without whitespace) it can be assigned directly, like `MyStringProp=MyValue`. This will assign the value `"MyValue"` to `public string MyStringProp {get; set;}`.
2. Strings containing whitespace need to be enclosed in double quotes like this `MyStringProp="My value"`. This will assign the value `"My value"` to `public string MyStringProp {get; set;}`.


### Complex types

CCP also supports complex types which should be provided in JSON format. It will use [Json.NET](https://github.com/JamesNK/Newtonsoft.Json) to deserialize them.




So if you have:
```
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```

and

```
public class DoSomething : IOperation
{
    public Person Person { get; set; }

    public void Run()
    {        
    }
}
```

It can be run through:
```
MyApp.exe DoSomething Person={Name:"\"John Doe\"",Age:123}
```

Note there cannot be any whitespace between the values in the JSON. Instead of using double quotes, you can also use single quotes inside JSON, which might make it a bit cleaner.

### Operation aliases

Operations can have aliases (alternative names). They are defined by adding the ```Alias`` attribute like this:


```
[Alias(Name = "a")]
[Alias(Name = "some_other_alias")]
public class DoSomething : IOperation
{
    public int SomeNumber { get; set; }

    public void Run()
    {        
    }
}
```

When an alias is defined, the operation may be called either by its full name or by its alias. Operation aliases can also be prefixed with a dash, which is a popular convention.

All the following will have exactly the same result:
```
MyApp.exe DoSomething SomeNumber=123
MyApp.exe a SomeNumber=123
MyApp.exe -a SomeNumber=123
MyApp.exe some_other_alias SomeNumber=123
MyApp.exe -some_other_alias SomeNumber=123
```

> It's important to remember that although duplicated operation names (type names) will be caught by the compiler, duplicated aliases will not. It's the responsibility of the developer to assure they are unique. If not, it can have unexpected consequences - the wrong operation might be executed.

### Property aliases

Similarly to operations also properties can have aliases added like this:

```

public class DoSomething : IOperation
{
    [Alias(Name = "nr")]
    [Alias(Name = "nmbr")]  
    public int SomeNumber { get; set; }

    public void Run()
    {        
    }
}
```

Using the above operation, all the following will have the same result:  
```
MyApp.exe DoSomething SomeNumber=123
MyApp.exe DoSomething nr=123
MyApp.exe DoSomething nmbr=123
```

###  Description

Desciptions can be added to operation classes and properties.

```
[Description("Operation description")]
public class OperationWithDescriptionProp : IOperation
{
    public string Arg1 { get; set; }

    [Description("Test description")]
    public string Arg2 { get; set; }

    [Description(Description = "Test description 2")]
    public string Arg3 { get; set; }

    public void Run()
    {
    }
}
```

Will display the following help text:

```
[Operation description]
OperationWithDescriptionProp
	* Arg1 <String>
	  [Test description]
	* Arg2 <String>
	  [Test description 2]
	* Arg3 <String>
```

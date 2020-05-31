# TypeLITE

TypeLITE is the utility, that generates [TypeScript](http://www.typescriptlang.org/) definitions from .NET classes. 

It's especially useful to keep your [TypeScript](http://www.typescriptlang.org/) classes on the client in sync with your POCO classes on the server.

## Visual Studio 2015 Update 2

Due to unresolved problems with mscorlib, TypeLITE only works in Visual Studio 2015 Update 2 if the project targets full .NET framework. It doesn't work for Portable class libraries.

## Usage

Please check [the project webpage](http://type.litesolutions.net/)

## License

The library is distributed under MIT license.

## Showcase - projects using TypeLITE
* MvcControllerToProxyGenerator ([https://github.com/squadwuschel/MvcControllerToProxyGenerator](https://github.com/squadwuschel/MvcControllerToProxyGenerator))

## Changelog


### Version 1.8.4       (2. 3. 2017)
Fixed       #128 nested inner classes has incorrect module name

### Version 1.8.3       (2. 3. 2017)
Fixed       Nested IEnumerables caused infinite loop
Fixed       Use namespace instead of deprecated module keyword

### Version 1.8.2       (13. 2. 2017)
Fixed       Do not generate empt modules
Fixed       Export constants with const keyword

### Version 1.8.1       (7. 4. 2016)
Added       Added ForReferencedAssembly extension method

### Version 1.8.0       (3. 4. 2016)
Fixed       #118, #113 Issues with Visual Studio 2015 Update 2

Fixed       Error generating documentation with type params

### Version 1.7.0       (27. 3. 2016)
Added       Added alternative generator for KnockoutModels (see https://bitbucket.org/svakinn/typelite/overview)

Fixed       #82 more deterministic ordering of generated code

Fixed       #103 types overridden in converter still appear in generated code

Added       New extension method that register all derived typesTypesDervivedFrom<T>

### Version 1.6.0       (22. 1. 2016)
Fixed       #110 interface for classes with a base class

Added       #109 support for System.Sbyte

### Version 1.5.2       (29. 11. 2015)
Fixed       Error generating JsDoc in case of the name of the assembly contains a space

### Version 1.5.1       (17. 10. 2015)
Fixed       Problem with the binaries version in 1.5.0

### Version 1.5.0       (17. 10. 2015)
Added       Implemented support for interface inheritance. 

Added       Added support for [TsIgnore] attribute on classes

### Version 1.4.0       (7. 8. 2015)
Added       #95, #96 Adds option to generate enums without 'const' modifier. Use TypeScript.AsConstEnums(false) in your TypeLite.tt file.

Fixed       #94 TsGeneratorOutput isn't treated as flag in AppendEnumDefinition

### Version 1.3.1       (22. 7. 2015)
Fixed       #90 export const enum for compatibility with TypeScript 1.5

### Version 1.3.0       (6. 7. 2015)
Added       #89 Added support for Windows Phone 8.1 as target platform

Added       #73 Added support for generating JSDoc comments from XML documentation. Works only in .NET 4, needs XML Doc files. Use .WithJSDoc()

### Version 1.2.0       (1. 7. 2015)
Added       #86 Support for classes outside modules. A TS class is generated outside module if the source .NET class isn't in a namespace or if  [TsClass(Module = "")] attribute is used.

Fixed       #79 ModuleNameForrmater not called in certain cases


### Version 1.1.2.0		(3. 4. 2015)
Fixed		#85 Unable to reuse enums

Fixed       #84 Module name formatter doesn't work for nested namespaces

### Version 1.1.1.0		(1. 3. 2015)
Fixed		#76 Error when renaming modules

### Version 1.1.0.0		(12. 2. 2015)
Added		Better extensibility of TsGenerator, better extensibility of formatters

### Version 1.0.2.0		(17. 11. 2014)
Fixed		#47 Fixed problem with derived generics

### Version 1.0.1.0		(17. 11. 2014)
Fixed		#64 Incorrect definition for KeyValuePair<int, List<string>>

Fixed		#65 Generic property referencing containing type causes StackOverflowException

Added		#49 Better output formating

### Version 1.0.0.0		(29. 10. 2014)
Fixed		#57 Support for generics

### Version 0.9.6.0		(20. 10. 2014)
Fixed		#51 Support for multidimensional arrays

### Version 0.9.5.0		(5. 9. 2014)
Fixed		#52 Support for using [TsEnum] without class

Added		#60 DateTimeOffset generated as Date

Added		#50 Support for generating TypeScript interfaces from .NET interfaces

### Version 0.9.4.1		(3. 9. 2014)
Fixed		#59 Bug in tt files

### Version 0.9.4.0		(20. 8. 2014)
Added		#57 Support public fields

### Version 0.9.3.0		(18. 6. 2014)
Fixed		#48 For<Enum>().ToModule()
Added		#46 Support for inner classes

### Version 0.9.2.0		(16. 6. 2014)
Fixed		#43 Declare keyword on modules with enums

Fixed		#44 Export keyword on enums

Fixed		#45 Empty modules

Added		#27 Support for standalone enums

### Version 0.9.1.9		(10. 6. 2014)
Fixed		#33: Enums not created when using list

Fixed		#41: Combination of generics <T> and Enum throws an exception

Fixed		#42: Duplicate TS interfaces for generic parameters

### Version 0.9.1.8		(8. 6. 2014)
Added		Strong assembly names

### Version 0.9.1.7		(29. 5. 2014)
Added		#17: Enums should go to .ts files

### Version 0.9.1.6		(17. 1. 2014)
Added		MemberTypeFormatter

Fixed		#28: Fluent method for adding references

### Version 0.9.1.5		(10. 11. 2013)
Added		Optional fields

Fixed		#24: Nullable enums

### Version 0.9.1.4		(14. 10. 2013)
Added		Nuget package TypeLITE.Lib without T4 templates

Fixed		Empty modules when type convertor is used

### Version 0.9.1.3		(10. 10. 2013)
Fixed		Incorrect output of type convertor if the module is specified

### Version 0.9.1.2		(9. 10. 2013)
Fixed		#15: Problems with enum underlaying types

Fixed		#18: ModelVisitor visits enums

Added		#7:  Type convertors

Added		#9:  Fluent configuration for classes

### Version 0.9.1.1		(9. 8. 2013)
Added		#12: Generation of enums

### Version 0.9.1beta      	(9. 8. 2013)
Fixed		#13: TypeScript 0.9.1 uses boolean keyword instead of bool

### Version 0.9.0beta	(27. 6. 2013)
Fixed		#11: Typescript 0.9 requires declare keyword in the module definition

Enhancement	#10: Converted project to Portable class library

### Version 0.8.3		(22. 4. 2013)
Fixed		#4: DateTime type conversion results in invalid type definition

Fixed		#5: Generic classes result in invalid interface definitions

Fixed		#6: Properties with System.Guid type result in invalid typescript code	 

### Version 0.8.2		(8. 4. 2013)
Fixed		#2: TsIgnore-attribute doesn't work with properties

Added		Support for nullable value types

Added		Support for .NET 4
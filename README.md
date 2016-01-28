# Ditto.Resolvers

[![Build status](https://ci.appveyor.com/api/projects/status/w545oncssfcafldq?svg=true)](https://ci.appveyor.com/project/MichaelLaw/ditto-resolvers)
[![NuGet release](https://img.shields.io/nuget/vpre/Ditto.Resolvers.Archetype.svg)](https://www.nuget.org/packages/Ditto.Resolvers.Archetype)

Ditto is awesome, we all know that, though it would be nice if we could hook third parties like Archetype and The Grid to the Ditto flow. This wee chunk of code is aiming to achieve just that.

Currently this only has support for Archetypes, but hopefully over the next couple of weeks we can add to that.

## Archetypes

Setup is easy. Below is a Ditto class which maps to my Home document type, this document type has an Archetype property called PriceList (alias: priceList).

```csharp
	public class Home : Page
    {
        public Home(IPublishedContent content) : base(content)
        {
        }

        public string Title { get; set; }

        public HtmlString Body { get; set; }
		 
        [ArchetypeValueResolver]
        public List<PriceList> PriceList { get; set; }
    }
```
PriceList is just a POCO

```csharp
	public class PriceList
    {
        public string Title { get; set; }

        public int Quantity { get; set; }

        public string Price { get; set; }

        [TypeConverter(typeof(DittoContentPickerConverter))]
        public Content AssociatedPage { get; set; }
    }
```

Now when we use Ditto to map the Home document type via the .As<Home>(); extension method, our Archetype will map to our POCO. Nice and easy, eh? This will provide almost all the functionality of Ditto e.g. TypeConverters, LazyLoading to use on your Archetype POCOs.

### Options

#### Alias

If your Archetype property alis on your Home document type doesn't match the Property name, you can set this in the **ArchetypeValueResolverAttribute**.

```csharp
	public class Home : Page
    {
		...

		[ArchetypeValueResolver("HomeDocTypePriceListAlias")]
		public List<PriceList> PriceList { get; set; }
	}
```

You can use a **ArchetypePropertyAttribute** if a property on your Archetype class doesnt match the alias on your configuration of your Archetype property

```csharp
	public class PriceList : IFieldset
    {
        ...

        [ArchetypeProperty("randomAlias")]
        public string Price { get; set; }
    }
```

#### Single items

Your Archetype doesn't have to be a list! Simply use a single Type of your Archetype POCO and it will attempt to map to that.

```csharp
	public class Home : Page
    {
		...

		[ArchetypeValueResolver]
		public PriceList PriceList { get; set; }
	}
```

#### Multiple fieldsets

If you have enabled multiple fieldsets in your Archetype data type this would create a generic list including all of the different types of your POCOs which match an Archetype fieldset alias. The only caveat being you must decorate each POCO with an **ArchetypeContentAttribute** (you can also define an alternative alias on this too) and create an interface or abstract class for your Generic lists first generic Argument 'ISomething' or whatever you like really. This lets us know this POCO has derived types.

```csharp
	public interface ISomething
	{
	}

	[ArchetypeContent]
	public class PriceList : ISomething, IFieldset
    {
	...
	}

	[ArchetypeContent("potentiallySomeOtherArchetypeAlias")]
	public class PriceListNew : ISomething, IFieldset
    {
	...
	}

	public class Home : Page
    {
		...

		[ArchetypeValueResolver]
		public List<ISomething> PriceList { get; set; }
	}
```

## Extras

Archetype has handy => Disabled and => Alias fields on its ArchetypeFieldsetModel, so if you implement the IFieldset interface on your POCO, these fields will be wired up :)

```csharp
	public class PriceList : IFieldset
    {
        public string Title { get; set; }

        public int Quantity { get; set; }

        public string Price { get; set; }

        [TypeConverter(typeof(DittoContentPickerConverter))]
        public Content AssociatedPage { get; set; }

		public string Alias { get; set; }

        public bool Disabled { get; set; }
    }
```

#### Ditto Events

You can also fire on converted or converting methods on your POCO using **DittoOnConvertingAttribute** or **DittoOnConvertedAttribute** attributes. If that isn't granular enough,
why not fire them at a property level using the **ArchetypeOnPropertyConvertingAttribute** or the **ArchetypeOnPropertyConvertedAttribute** attributes.

```csharp
	[ArchetypeContent]
    public class PriceList
    {
        public string Title { get; set; }

		[ArchetypeOnPropertyConverting(nameof(Title))]
        private void MainOnConverting(DittoConversionHandlerContext context, ArchetypeFieldsetModel fieldset)
        {
            var x = context;
        }

        [ArchetypeOnPropertyConverted(nameof(Title))]
        private void MainOnConverted(DittoConversionHandlerContext context, ArchetypeFieldsetModel fieldset)
        {
            var x = context;
        }

        public int Quantity { get; set; }

        public string Price { get; set; }

        [TypeConverter(typeof(DittoContentPickerConverter))]
        public Content AssociatedPage { get; set; }

        [DittoOnConverting]
        internal void OnConverting(DittoConversionHandlerContext context)
        {
            var x = context;
        }

        [DittoOnConverted]
        internal void OnConverted(DittoConversionHandlerContext context)
        {
            var x = context;
        }
    }
```

#### Nested Archetypes

Rejoice. Nested Archetypes should also work so long as you have decorated the child Archetype property on your POCO with the **ArchetypeResolverAttribute**.

```csharp
	public class PriceList
    {
        public string Title { get; set; }

        public int Quantity { get; set; }

        public string Price { get; set; }

        [TypeConverter(typeof(DittoContentPickerConverter))]
        public Content AssociatedPage { get; set; }
        
		[ArchetypeProperty("overrideAliasCheck")]
        [ArchetypeValueResolver("overridenByArchetypeContentAttributeAbove")]
		public List<UrlPicker> Links { get; set; }

		[ArchetypeValueResolver]
		public SomeOtherPOCO NewProperty { get; set; }
    }
```

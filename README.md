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

        [ArchetypePropertyAttribute("randomAlias")]
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
        
		[ArchetypeContent("overrideAliasCheck")]
        [ArchetypeValueResolver("overridenByArchetypeContentAttributeAbove")]
		public List<UrlPicker> Links { get; set; }
    }
```

## Grid

So, it looks like we've made it, the Grid managed via Ditto and here's how. Firstly we create a DocType as we would usually, only his time we add a property which returns a **GridModel** class defined in our new Ditto.Grid assembly. We plug our new **GridResolverAttribute** on top and away Ditto goes, trying to map our Grid to a strongly typed object. Neat eh?

```csharp
	public class TextPage : Page
    {
        public TextPage(IPublishedContent content) : base(content)
        {
        }

        public string Title { get; set; }

        public HtmlString Body { get; set; }

        [GridResolver]
        public GridModel Grid { get; set; }
    }
```
### Options

#### Alias

Much like the Archetype resolver, this one also has an Alias property so if you wanted to map to a specific grid property on your doc type, you could.

```csharp
	[GridResolver("someOtherAlias")]
    public GridModel Grid { get; set; }
```

### Binding

The **GridModel** class maps directly to the output Json of the grid, so all potential model binding on standard fields should hook up as standard using Json.Net, but what about the actual output value rendered in each control, seen as this is the only difficult bit, how can we map this to a strong type you ask? Enter Ditto.

The classes below are the containers for the control and its descriptive editor values, with a little setup we can confgure the field **ConvertedValue** to return a type we expect.

```csharp
    public class Control
    {
        public object value { get; set; }

        public virtual object ConvertedValue { get; set; }

        public Editor editor { get; set; }
    }

    public class Editor
    {
        public string name { get; set; }

        public string alias { get; set; }

        public string view { get; set; }

        public string render { get; set; }

        public string icon { get; set; }
    }
```

### Setup

#### TypeConverters

So if you're familiar with how Ditto works or even familiar with the **TypeConverter** class in .Net at all, then this will seem straight forward to you. You basically create a class which for a given value of a specific type, will return an object of your choosing. So the idea here is that you create (or reuse) your existing **TypeConverters** to bind the types returned via the Grid to strong types. I'm not going to show you a **TypeConverter** here as I'm sure if you dig around Ditto for 5 minutes there will be loads of examples. In fact, here's [how you do a TypeConverter](http://umbraco-ditto.readthedocs.org/en/latest/usage/#advanced-usage-type-converters), don't say I'm not good to you.

#### TypeConverterLocator

So now we have our **TypeConverters** we need a way to let Ditto know which Grid editor alias maps directly to a **TypeConverter**. Thankfully we have created a handy little resolver by the name of **DittoResolverTypeLocator** (rolls right of the tongue eh?), on startup this provides a nice lookup table for us to use in mapping multiple Grid editor alias to **TypeConverters**. See the blow example on how we register them:

```csharp
    protected override void OnApplicationStarting(object sender, EventArgs e)
    {
        ...

        DittoResolverTypeLocator.Register<DittoHtmlStringConverter>("rte");
        DittoResolverTypeLocator.Register<DittoHtmlStringConverter>("embed");
        DittoResolverTypeLocator.Register<GridImageConverter>("media");
    }
```

This simple syntax will store a nice easy lookup where we could even dynamically add or remove aliases, depending on how clever or risky we fancy being.

### The Magic

From there on in it's all magic, when you have your type converters made and registered on startup, anywhere you have a Grid field mapped with our new **GridResolverAttribute** then Ditto and this code will bind it up.

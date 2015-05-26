# Ditto.Resolvers
Ditto is awesome, we all know that. Though it would be nice if we could
hook in to Ditto to add support for Archetypes and the Grid etc, these resolvers hope to fix that.

Currently this only has support for Archetypes, but soon it would be good to add other third parties 
like Grid, Sir Trevor etc

This code is hugely untested in its current form, but seems to work currently for Archetypes.

## Archetypes

Setup is easy. Just decorate your Archetype property with a **ArchetypeResolverAttribute**, optionally if 
your property name is different to the property alias in Umbraco, then yucan define this here.

'''csharp
	[ArchetypeResolver("DifferentPropertyName")]
	public List<CustomArchetype> List { get; set; }
'''

The above code show how this would work for a list of the custom Archetype class **CustomArchetype** 
in my solution. We could easy make this class a single item by not using a List at all and this would 
resolve to a single item of this type.

'''csharp
	[ArchetypeResolver("DifferentPropertyName")]
	public CustomArchetype List { get; set; }
'''

If you have enabled multiple fieldsets in Archetype then this would create a generic list including all
of the different types of Archetypes you have defined on your datatype. The only caveat being your
property would require to look like below.

'''csharp
	[ArchetypeResolver("DifferentPropertyName")]
	public List<ArchetypeFieldsetModel> List { get; set; }
'''

### Archetypes Constraints

There are two contrainsts around POCO Archetypes currently. Each POCO, must inherit from the **ArchetypeFieldsetModel** 
in the Archetype assembly and the Name of your class must match the Archetypes alias on the fieldset alias.  We can work around the 
second issue here in the coming weeks, but the first one is a deal breaker.

Sorry if this offends you!
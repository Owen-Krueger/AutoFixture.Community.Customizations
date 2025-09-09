namespace AutoFixture.Community.Customizations;

/// <summary>
/// Extension methods for <see cref="IFixture"/> models.
/// </summary>
public static class AutoFixtureExtensions
{
    /// <summary>
    /// Adds a customization to the provided <see cref="IFixture"/> model to automatically omit properties with the
    /// <see cref="IgnoreAutoFixtureAttribute"/> attribute, unless explicitly told to be generated.
    /// </summary>
    public static IFixture AddIgnorePropertiesCustomization(this IFixture fixture) => 
        fixture.Customize(new IgnorePropertiesCustomization());
    
    /// <summary>
    /// Adds a customization to the provided <see cref="IFixture"/> model to automatically handle
    /// <see cref="DateOnly"/> properties.
    /// </summary>
    public static IFixture AddDateOnlyCustomization(this IFixture fixture) => 
        fixture.Customize(new DateOnlyCustomization());
    
    /// <summary>
    /// Adds a customization to the provided <see cref="IFixture"/> model to automatically handle
    /// <see cref="TimeOnly"/> properties.
    /// </summary>
    public static IFixture AddTimeOnlyCustomization(this IFixture fixture) => 
        fixture.Customize(new TimeOnlyCustomization());

    /// <summary>
    /// Changes the <see cref="IFixture"/> behavior to omit circular references when creating objects
    /// rather than throw <see cref="ObjectCreationException"/> exceptions.
    /// </summary>
    public static IFixture OmitCircularReferences(this IFixture fixture)
    {
        fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        return fixture;
    }
}
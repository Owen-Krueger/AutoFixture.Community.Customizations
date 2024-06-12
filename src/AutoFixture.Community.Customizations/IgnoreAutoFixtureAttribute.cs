namespace AutoFixture.Community.Customizations;

/// <summary>
/// Instructs AutoFixture to not stand up this property during creation.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class IgnoreAutoFixtureAttribute : Attribute { }
namespace AutoFixture.Customizations;

/// <summary>
/// Customizes AutoFixture to automatically handle <see cref="TimeOnly"/> properties.
/// </summary>
public class TimeOnlyCustomization : ICustomization
{
    /// <summary>
    /// Adds a customization to handle <see cref="TimeOnly"/> properties.
    /// </summary>
    public void Customize(IFixture fixture)
    {
        fixture.Customize<TimeOnly>(x => x.FromFactory((DateTime date) => TimeOnly.FromDateTime(date)));
    }
}
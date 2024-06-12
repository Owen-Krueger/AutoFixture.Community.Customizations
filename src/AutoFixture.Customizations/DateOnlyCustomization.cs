namespace AutoFixture.Customizations;

/// <summary>
/// Customizes AutoFixture to automatically handle <see cref="DateOnly"/> properties.
/// </summary>
public class DateOnlyCustomization : ICustomization
{
    /// <summary>
    /// Adds a customization to handle <see cref="DateOnly"/> properties.
    /// </summary>
    public void Customize(IFixture fixture)
    {
        fixture.Customize<DateOnly>(x => x.FromFactory((DateTime date) => DateOnly.FromDateTime(date)));
    }
}
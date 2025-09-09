namespace AutoFixture.Community.Customizations;

/// <summary>
/// Customizes AutoFixture to automatically handle <see cref="long"/> properties.
/// </summary>
public class LongCustomization : ICustomization
{
    /// <summary>
    /// Adds a customization to handle <see cref="long"/> properties.
    /// </summary>
    public void Customize(IFixture fixture)
    {
        fixture.Customize<long>(x => x.FromFactory((int value) => value));
    }
}
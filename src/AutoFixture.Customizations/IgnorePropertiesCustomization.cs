using System.Reflection;
using AutoFixture.Kernel;

namespace AutoFixture.Customizations;

/// <summary>
/// Customizes AutoFixture to ignore properties with the <see cref="IgnoreAutoFixtureAttribute"/> attribute.
/// </summary>
public class IgnorePropertiesCustomization : ICustomization
{
    /// <summary>
    /// Adds the <see cref="IgnorePropertiesSpecimenBuilder"/> customization to the fixture.
    /// </summary>
    public void Customize(IFixture fixture)
    {
        fixture.Customizations.Add(new IgnorePropertiesSpecimenBuilder());
    }

    /// <summary>
    /// AutoFixture builder that automatically omits properties with the <see cref="IgnoreAutoFixtureAttribute"/> attribute.
    /// </summary>
    private class IgnorePropertiesSpecimenBuilder : ISpecimenBuilder
    {
        /// <summary>
        /// On object creation, ignore properties with the attribute.
        /// </summary>
        public object Create(object request, ISpecimenContext context)
        {
            var propertyInfo = request as PropertyInfo;
            if (propertyInfo != null && propertyInfo.GetCustomAttribute<IgnoreAutoFixtureAttribute>() != null)
            {
                return new OmitSpecimen();
            }

            return new NoSpecimen();
        }
    }
}
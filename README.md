# AutoFixture Community Customizations

[![.NET](https://github.com/Owen-Krueger/AutoFixture.Community.Customizations/actions/workflows/dotnet.yaml/badge.svg)](https://github.com/Owen-Krueger/AutoFixture.Community.Customizations/actions/workflows/dotnet.yaml)

## IgnoreAutoFixture Attribute

The `IgnoreAutoFixture` attribute can be added to properties in a model to instruct AutoFixture to ignore/omit the property during building, unless explicitly instructed to be created.

This is especially useful for standing up models used by Entity Framework. Most of these models utilize circular dependencies, which would cause an exception to be raised by AutoFixture normally.

An example of this can be found below:

``` C#
public class TestModel
{
    public InnerModel? IncludedModel { get; set; } // Will be instantiated by AutoFixture

    [IgnoreAutoFixture]
    public InnerModel? OmittedModel { get; set; } // Will be omitted by AutoFixture
    
    [IgnoreAutoFixture]
    public InnerModel? ExplicitlyIncludedModel { get; set; } // Will also be omitted by AutoFixture, but will be explicitly included (see below)
}

public class Tests
{
    [Test]
    public void AutoFixtureTests()
    {
        var fixture = new Fixture();
        fixture.AddIgnorePropertiesCustomization();
        
        var model = fixture.Build<TestModel>()
            .With(x => x.ExplicitlyIncludedModel, new InnerModel()) // Tells AutoFixture to set this property, instead of ignoring it.
            .Create();
        
        Assert.That(model.IncludedModel, Is.Not.Null);
        Assert.That(model.OmittedModel, Is.Null);
        Assert.That(model.ExplicitlyIncludedModel, Is.Not.Null);
    }
}
```

## DateOnly/TimeOnly Customization

AutoFixture customizations are provided to handle `DateOnly` and `TimeOnly` properties. By default, AutoFixture doesn't handle these types and will throw exceptions.

An example of this can be found below:

``` C#
public class TestModel
{
    public DateOnly? Date { get; set; }

    public TimeOnly? Time { get; set; }
    
}

public class Tests
{
    [Test]
    public void AutoFixtureTests()
    {
        var fixture = new Fixture();
        fixture
            .AddDateOnlyCustomization()
            .AddTimeOnlyCustomization();
        
        var model = fixture.Create<TestModel>();
        
        Assert.That(model.Date, Is.Not.Null);
        Assert.That(model.Time, Is.Not.Null);
    }
}
```

## AutoFixture Extension Methods

To use any of these customizations, you can use the following extension methods:

``` C#
var fixture = new Fixture();
fixture.AddIgnorePropertiesCustomization(); // Ignore properties
fixture.AddDateOnlyCustomization(); // DateOnly
fixture.AddTimeOnlyCustomization(); // TimeOnly
```

You can also chain any of these extensions together:

``` C#
var fixture = new Fixture();
fixture
    .AddIgnorePropertiesCustomization()
    .AddDateOnlyCustomization()
    .AddTimeOnlyCustomization();
```


If you'd rather, you can ignore these extension methods and add the customizations yourself manually:

``` C#
var fixture = new Fixture();
fixture.Customizations.Add(new IgnorePropertiesSpecimenBuilder()); // Ignore properties
fixture.Customize(new DateOnlyCustomization()); // DateOnly
fixture.Customize(new TimeOnlyCustomization()); // TimeOnly
```
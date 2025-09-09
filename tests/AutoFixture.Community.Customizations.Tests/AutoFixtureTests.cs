using AutoFixture.Community.Customizations;

namespace AutoFixture.Customizations.Tests;

public class AutoFixtureTests
{
    [Test]
    public void IgnoreAutoFixtureAttribute_ModelWithAttributes_ExpectedModelInstantiated()
    {
        var fixture = new Fixture();
        fixture.AddIgnorePropertiesCustomization();
        var model = fixture.Build<TestModel>()
            .With(x => x.ExplicitlyIncludedModel, new InnerModel())
            .Create();

        Assert.Multiple(() =>
        {
            Assert.That(model.IncludedModel, Is.Not.Null);
            Assert.That(model.OmittedModel, Is.Null);
            Assert.That(model.ExplicitlyIncludedModel, Is.Not.Null);
        });
    }

    [Test]
    public void DateOnly_ModelWithDateOnly_DateOnlyPropertyStoodUp()
    {
        var fixture = new Fixture();
        fixture.AddDateOnlyCustomization();
        var model = fixture.Create<TestModelWithDateOnly>();

        Assert.That(model.Date, Is.Not.Null);
    }

    [Test]
    public void TimeOnly_ModelWithTimeOnly_TimeOnlyPropertyStoodUp()
    {
        var fixture = new Fixture();
        fixture.AddTimeOnlyCustomization();
        var model = fixture.Create<TestModelWithTimeOnly>();

        Assert.That(model.Time, Is.Not.Null);
    }

    [Test]
    public void OmitCircularReferences_ModelWithCircularDependency_ExpectedModelInstantiated()
    {
        var fixture = new Fixture();
        fixture.OmitCircularReferences();
        var model = fixture.Create<TestModelWithCircularDependency1>();

        Assert.Multiple(() =>
        {
            Assert.That(model.Model, Is.Not.Null);
            Assert.That(model.Model.Model, Is.Null);
        });
    }

    public class TestModel
    {
        public long Test { get; set; }
        
        public InnerModel? IncludedModel { get; set; }

        [IgnoreAutoFixture] public InnerModel? OmittedModel { get; set; }

        [IgnoreAutoFixture] public InnerModel? ExplicitlyIncludedModel { get; set; }
    }

    public class InnerModel
    {
    }

    public class TestModelWithDateOnly
    {
        public DateOnly? Date { get; set; }
    }

    public class TestModelWithTimeOnly
    {
        public TimeOnly? Time { get; set; }
    }

    public class TestModelWithCircularDependency1
    {
        public TestModelWithCircularDependency2 Model { get; set; } = new();
    }

    public class TestModelWithCircularDependency2
    {
        public TestModelWithCircularDependency1 Model { get; set; } = new();
    }
}
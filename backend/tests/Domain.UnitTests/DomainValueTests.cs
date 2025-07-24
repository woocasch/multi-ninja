using Shouldly;
using TestStack.BDDfy;

namespace MultiNinja.Backend.Domain.UnitTests;

public class DomainValueTests
{
    private DomainValue value1 = null!;
    
    private DomainValue value2 = null!;

    private bool comparisonResult;

    [Fact]
    public void WhenIdenticalValuesAreComparedThenResultIsTrue()
    {
        this.Given(t => t.Value1Is(new DomainValue1(1, "1")))
            .And(t => t.Value2Is(new DomainValue1(1, "1")))
            .When(t => t.ValuesAreCompared())
            .Then(t => t.ResultIs(true))
            .BDDfy();
    }

    [Fact]
    public void WhenDifferentIdsAreComparedThenResultIsFalse()
    {
        this.Given(t => t.Value1Is(new DomainValue1(1, "1")))
            .And(t => t.Value2Is(new DomainValue1(2, "1")))
            .When(t => t.ValuesAreCompared())
            .Then(t => t.ResultIs(false))
            .BDDfy();
    }

    [Fact]
    public void WhenDifferentNamesAreComparedThenResultIsFalse()
    {
        this.Given(t => t.Value1Is(new DomainValue1(1, "1")))
            .And(t => t.Value2Is(new DomainValue1(1, "2")))
            .When(t => t.ValuesAreCompared())
            .Then(t => t.ResultIs(false))
            .BDDfy();
    }

    [Fact]
    public void WhenDifferentInstancesAreComparedThenResultIsFalse()
    {
        this.Given(t => t.Value1Is(new DomainValue1(1, "1")))
            .And(t => t.Value2Is(new DomainValue1(2, "2")))
            .When(t => t.ValuesAreCompared())
            .Then(t => t.ResultIs(false))
            .BDDfy();
    }

    [Fact]
    public void WhenIdenticalInstancesOfDifferentTypesAreComparedThenResultIsFalse()
    {
        this.Given(t => t.Value1Is(new DomainValue1(1, "1")))
            .And(t => t.Value2Is(new DomainValue2(1, "1")))
            .When(t => t.ValuesAreCompared())
            .Then(t => t.ResultIs(false))
            .BDDfy();
    }

    [Fact]
    public void WhenSecondInstanceIsNullThenResultIsFalse()
    {
        this.Given(t => t.Value1Is(new DomainValue1(1, "1")))
            .And(t => t.Value2Is(null!))
            .When(t => t.ValuesAreCompared())
            .Then(t => t.ResultIs(false))
            .BDDfy();
    }
    
    private void Value1Is(DomainValue value) => this.value1 = value;
    
    private void Value2Is(DomainValue value) => this.value2 = value;

    private void ValuesAreCompared()
    {
        this.comparisonResult = this.value1.Equals(this.value2);
    }

    private void ResultIs(bool value)
    {
        this.comparisonResult.ShouldBe(value);
    }
    
    private class DomainValue1 : DomainValue
    {
        public DomainValue1(int id, string name) : base(id, name)
        {
        }
    }

    private class DomainValue1_1 : DomainValue1
    {
        public DomainValue1_1(int id, string name) : base(id, name)
        {
        }
    }

    private class DomainValue2 : DomainValue
    {
        public DomainValue2(int id, string name) : base(id, name)
        {
        }
    }
}
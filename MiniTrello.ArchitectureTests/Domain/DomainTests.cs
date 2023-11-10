using FluentAssertions;
using MediatR;
using MiniTrello.Domain.Primitives;
using MiniTrello.Domain.Ticket;
using NetArchTest.Rules;
using System.Reflection;

namespace MiniTrello.ArchitectureTests.Domain;

public class DomainTests
{
    private readonly Assembly DomainAssembly = typeof(Ticket).Assembly;
    
    [Fact]
    public void DomainEvents_ShouldBe_Sealed()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That().ImplementInterface(typeof(IDomainEvent))
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainEvents_Should_HaveDomainEventPostfix()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That().ImplementInterface(typeof(IDomainEvent))
            .Should()
            .HaveNameEndingWith("DomainEvent")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainLayer_ShouldNot_HaveAnyDependency()
    {
        var dependencies = new string[]{ "MiniTrello.Application", "MiniTrello.Infrastructure", "MiniTrello.Web" };

        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOnAll(dependencies)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainEvents_Should_Implement_INotification()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .ImplementInterface(typeof(INotification))
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}

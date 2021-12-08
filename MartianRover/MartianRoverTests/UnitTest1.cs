using System.ComponentModel;
using Xunit;
using FluentAssertions;
namespace MartianRoverTests;

[Trait("Le robot martien","doit")]
public class mars_rover_should
{
    [Fact]
    [DisplayName("être bien positionné à son atterrissage")]
    public void mars_rover()
    {
        var rover = new MarsRover(position:new []{0,0},direction:"N",grid:new [] {50,50});

        rover.Position.Should().BeEquivalentTo(new[] {0, 0});
        rover.Direction.Should().Be("N");
    }
    
    
    [Fact]
    [DisplayName("regarder vers l'ouest lorsque qu'il est regarde au nord puis tourne à gauche")]
    public void mars_rover_look_west()
    {
        var rover = new MarsRover(position:new []{0,0},direction:"N",grid:new [] {50,50});

        rover.Move("l");

        rover.Direction.Should().Be("O");
    }
}

public class MarsRover
{
    private readonly int[] _grid;
    public int[] Position { get; private set; }
    public string Direction { get; private set; }

    public MarsRover(int[] position, string direction, int[] grid)
    {
        _grid = grid;
        Position = position;
        Direction = direction;
    }

    public void Move(string s)
    {
        throw new System.NotImplementedException();
    }
}
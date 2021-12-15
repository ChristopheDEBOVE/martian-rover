using FluentAssertions;
using System.ComponentModel;
using Xunit;
namespace MartianRoverTests;

[Trait("Le robot martien", "doit")]
public class MartianRoverUnitTest
{
    [Fact]
    [DisplayName("être bien positionné à son atterrissage")]
    public void mars_rover_should_initialize_correctly()
    {
        var rover = new MarsRover(position: new[] { 0, 0 }, direction: "N", grid: new[] { 50, 50 });

        rover.Position.Should().BeEquivalentTo(new[] { 0, 0 });
        rover.Direction.Should().Be("N");
    }

    [Theory]
    [InlineData("N", "O")]
    [InlineData("O", "S")]
    [InlineData("S", "E")]
    [InlineData("E", "N")]
    public void mars_rover_look_the_right_direction_when_turn_on_left(string initialDirection, string expectedDirection)
    {
        var rover = new MarsRover(position: new[] { 0, 0 }, direction: initialDirection, grid: new[] { 50, 50 });

        rover.Move("l");

        rover.Direction.Should().Be(expectedDirection);
    }

    [Theory]
    [InlineData("N", "E")]
    [InlineData("O", "N")]
    public void mars_rover_look_the_right_direction_when_turn_on_right(string initialDirection, string expectedDirection)
    {
        var rover = new MarsRover(position: new[] { 0, 0 }, direction: initialDirection, grid: new[] { 50, 50 });

        rover.Move("r");

        rover.Direction.Should().Be(expectedDirection);
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

    public void Move(string command)
    {
        if (command == "r")
        {
            Direction = "E";
            return;
        }


        Direction = Direction switch
        {
            "N" => "O",
            "O" => "S",
            "S" => "E",
            _ => "N"
        };

    }
}
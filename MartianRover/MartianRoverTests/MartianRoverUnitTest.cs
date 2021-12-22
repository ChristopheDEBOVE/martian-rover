using FluentAssertions;
using System.ComponentModel;
using Xunit;
using System.Linq;
using System;

namespace MartianRoverTests;

[Trait("Le robot martien", "doit")]
public class MartianRoverUnitTest
{
    [Fact]
    [DisplayName("être bien positionné à son atterrissage")]
    public void mars_rover_should_initialize_correctly()
    {
        var rover = new MarsRover(position: new[] { 2,1 }, direction: "N", grid: new[] { 50, 51 });

        rover.Position.Should().BeEquivalentTo(new[] { 2, 1 }, a => a.WithStrictOrdering());

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
    /*
     *      N
     *    O   E
     *      S
     */
    [Theory]
    [InlineData("N", "E")]
    [InlineData("O", "N")]
    [InlineData("S", "O")]
    [InlineData("E", "S")]
    public void mars_rover_look_the_right_direction_when_turn_on_right(string initialDirection, string expectedDirection)
    {
        var rover = new MarsRover(position: new[] { 0, 0 }, direction: initialDirection, grid: new[] { 50, 50 });

        rover.Move("r");

        rover.Direction.Should().Be(expectedDirection);
    }

    /// prochain test multicommande ou avant arrière

}


public static class CardinalPointHelper{
    const string DIRECTIONS = "NESO";

    public static string ToRight(string currentDirection){

        var newDirectionIndex = DIRECTIONS.IndexOf(currentDirection)+1;
        if (newDirectionIndex > 3){
            newDirectionIndex = 0;
        }
        return DIRECTIONS[newDirectionIndex].ToString();
    }

    public static string ToLeft(string currentDirection){
        var newDirectionIndex = DIRECTIONS.IndexOf(currentDirection)-1;
        if (newDirectionIndex < 0)
            newDirectionIndex = 3;
        return DIRECTIONS[newDirectionIndex].ToString();
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

    private void Turn(string command)
    {
        if (command == "r")
            Direction = CardinalPointHelper.ToRight(Direction);
        else
            Direction = CardinalPointHelper.ToLeft(Direction);
    }
    

    public void Move(string command)
    {
        Turn(command);
    }
}
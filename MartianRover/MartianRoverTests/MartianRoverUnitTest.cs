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
        var rover = new MarsRover(position: new[] { 2, 1 }, direction: "N", grid: new[] { 50, 51 });

        rover.Position.Should().BeEquivalentTo(new[] { 2, 1 }, a => a.WithStrictOrdering());

        rover.Direction.Should().Be("N");
    }

    /*
     *      N
     *    O   E
     *      S
     */
    [Theory]
    [InlineData("r","N", "E")]
    [InlineData("r","S", "O")]
    [InlineData("r","E", "S")]
    [InlineData("r","O", "N")]
    [InlineData("l","N", "O")]
    [InlineData("l","O", "S")]
    [InlineData("l","S", "E")]
    [InlineData("l","E", "N")]
    [InlineData("rr", "N", "S")]
    [InlineData("rrr", "N", "O")]
    [InlineData("rrrr", "N", "N")]
    [InlineData("rrrr", "O", "O")]
    [InlineData("rl", "N", "N")]
    [InlineData("lr", "N", "N")]
    [InlineData("rrl", "N", "E")]
    [InlineData("rrl", "S", "O")]
    public void mars_rover_should_be_able_to_process_single_or_multiple_consecutive_turn_command(string command, string initialDirection, string expectedDirection)
    {
        var rover = new MarsRover(position: new[] { 0, 0 }, direction: initialDirection, grid: new[] { 50, 50 });

        rover.Move(command);

        rover.Direction.Should().Be(expectedDirection);
    }

    [Theory]
    [InlineData("f", "N", 2, 1, 2, 2,"droit vers le nord 1 fois")]
    [InlineData("f", "S", 2, 1, 2, 0, "droit vers le sud 1 fois")]
    [InlineData("ff", "N", 2, 1, 2, 3, "droit vers le nord 2 fois")]
    [InlineData("ff", "S", 2, 1, 2,-1, "droit vers le sud 2 fois")]
    [InlineData("f", "O", 2, 1, 1, 1, "droit vers l'ouest 1 fois")]
    [InlineData("ff", "O", 2, 1, 0, 1, "droit vers l'ouest 2 fois")]
    [InlineData("f", "E", 2, 1, 3, 1, "droit vers l'est 1 fois")]
    public void mars_rover_should_move_forward(string command, string initialOrientation, int initialXPosition, int initialYPosition, int expectedXPosition, int expectedYPosition, string because)
    {
        var rover = new MarsRover(position: new[] { initialXPosition, initialYPosition }, direction: initialOrientation, grid: new[] { 50, 51 });

        rover.Move(command);

        rover.Position.Should().BeEquivalentTo(new[] { expectedXPosition, expectedYPosition }, a => a.WithStrictOrdering(), because);
    }

    [Theory]
    [InlineData("b", "N", 2, 1, 2, 0, "en arrière  1 fois en regardant vers le nord")]
    [InlineData("b", "S", 2, 1, 2, 2, "en arrière  1 fois en regardant vers le nord")]
    public void mars_rover_should_move_backward(string command, string initialOrientation, int initialXPosition, int initialYPosition, int expectedXPosition, int expectedYPosition, string because)
    {
        var rover = new MarsRover(position: new[] { initialXPosition, initialYPosition }, direction: initialOrientation, grid: new[] { 50, 51 });

        rover.Move(command);

        rover.Position.Should().BeEquivalentTo(new[] { expectedXPosition, expectedYPosition }, a => a.WithStrictOrdering(), because);
    }
}
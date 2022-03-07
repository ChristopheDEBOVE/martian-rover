using FluentAssertions;
using System;
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
        var rover = new MarsRover(position: (2, 1), direction: "N");

        rover.Position.Should().Be((2, 1));

        rover.Direction.Should().Be("N");
    }


    /*
     *      N
     *    O   E
     *      S
     */
    [Theory]
    [InlineData("r", "N", "E")]
    [InlineData("r", "S", "O")]
    [InlineData("r", "E", "S")]
    [InlineData("r", "O", "N")]
    [InlineData("l", "N", "O")]
    [InlineData("l", "O", "S")]
    [InlineData("l", "S", "E")]
    [InlineData("l", "E", "N")]
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
        var rover = new MarsRover(position: (0, 0), direction: initialDirection);

        rover.Move(command);

        rover.Direction.Should().Be(expectedDirection);
    }

    [Theory]
    //  2 |                2 |        A
    //  1 |        A       1 |        
    //  0 |                0 |        
    //     _________          _________
    //     -1 0 1  2          -1 0 1  2
    [InlineData("f", "N", 2, 1, 2, 2, "droit vers le nord 1 fois")]

    //  2 |                2 |        
    //  1 |        V       1 |        
    //  0 |                0 |        V
    //     _________          _________
    //     -1 0 1  2          -1 0 1  2
    [InlineData("f", "S", 2, 1, 2, 0, "se deplacer droit vers le sud 1 fois")]
    [InlineData("ff", "N", 2, 1, 2, 3, "se deplacer droit vers le nord 2 fois")]
    [InlineData("ff", "S", 2, 1, 2, -1, "se deplacer droit vers le sud 2 fois")]
    [InlineData("f", "O", 2, 1, 1, 1, "se deplacer droit vers l'ouest 1 fois")]
    //  2 |                2 |        
    //  1 |        <       1 |   <     
    //  0 |                0 |        
    //     _________          _________
    //     -1 0 1  2          -1 0 1  2
    [InlineData("ff", "O", 2, 1, 0, 1, "se deplacer droit vers l'ouest 2 fois")]
    [InlineData("f", "E", 2, 1, 3, 1, "se deplacer droit vers l'est 1 fois")]
    public void mars_rover_should_be_able_to_move_forward(string command, string initialOrientation, int initialXPosition, int initialYPosition, int expectedXPosition, int expectedYPosition, string because)
    {
        var rover = new MarsRover(position: (initialXPosition, initialYPosition), direction: initialOrientation);

        rover.Move(command);

        rover.Position.Should().Be((expectedXPosition, expectedYPosition), because);
    }

    [Theory]
    //  2 |                2 |      
    //  1 |        A       1 |        
    //  0 |                0 |        A
    //     _________          _________
    //     -1 0 1  2          -1 0 1  2
    [InlineData("b", "N", 2, 1, 2, 0, "se deplacer en arrière 1 fois en regardant vers le nord")]

    //  2 |                2 |        V
    //  1 |        V       1 |        
    //  0 |                0 |        
    //     _________          _________
    //     -1 0 1  2          -1 0 1  2
    [InlineData("b", "S", 2, 1, 2, 2, "se deplacer en arrière 1 fois en regardant vers le sud")]
    public void mars_rover_should_be_able_to_move_backward(string command, string initialOrientation, int initialXPosition, int initialYPosition, int expectedXPosition, int expectedYPosition, string because)
    {
        var rover = new MarsRover(position: (initialXPosition, initialYPosition), direction: initialOrientation);

        rover.Move(command);

        rover.Position.Should().Be((expectedXPosition, expectedYPosition), because);
    }
}
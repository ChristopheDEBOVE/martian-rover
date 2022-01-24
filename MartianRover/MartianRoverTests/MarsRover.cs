using System.Collections.Generic;

namespace MartianRoverTests;
public static class CardinalPointHelper
{
    const string DIRECTIONS = "NESO";

    public static string ToRight(string currentDirection)
    {
        var newDirectionIndex = DIRECTIONS.IndexOf(currentDirection) + 1;
        if (newDirectionIndex > 3)
        {
            newDirectionIndex = 0;
        }
        return DIRECTIONS[newDirectionIndex].ToString();
    }

    public static string ToLeft(string currentDirection)
    {
        var newDirectionIndex = DIRECTIONS.IndexOf(currentDirection) - 1;
        if (newDirectionIndex < 0)
            newDirectionIndex = 3;
        return DIRECTIONS[newDirectionIndex].ToString();
    }
}
public record Position(int X, int Y);
public class MarsRover
{
    private readonly int[] _grid;


    public int[] Position { get { return new[] { _position.X, _position.Y }; } }
    private Position _position;
    public string Direction { get; private set; }

    public MarsRover(int[] position, string direction, int[] grid)
    {
        _grid = grid;
        _position = new Position(position[0], position[1]);
        Direction = direction;
    }

    private void Turn(char command)
    {
        if (command == 'r')
            Direction = CardinalPointHelper.ToRight(Direction);
        if (command == 'l')
            Direction = CardinalPointHelper.ToLeft(Direction);
    }
    readonly Dictionary<string, (int, int)> combinations = new (){
                { "Nf" ,(0, 1)},
                { "Nb", (0, -1)},
                { "Sf", (0, -1)},
                { "Sb", (0, 1)},
                { "Ef" ,(1, 0)},
                { "Eb", (-1, 0)},
                { "Of", (-1, 0)},
                { "Ob", (1, 0)}};

    public void Move(string command)
    {

        foreach (var unitaryCommand in command)
        {
            MoveFB(unitaryCommand);
            Turn(unitaryCommand);
        }
    }

    private void MoveFB(char unitaryCommand)
    {
        var key = Direction + unitaryCommand;
        if (combinations.ContainsKey(key))
        {
            _position = new(_position.X + combinations[key].Item1,
                     _position.Y + combinations[key].Item2);
        }
    }
}
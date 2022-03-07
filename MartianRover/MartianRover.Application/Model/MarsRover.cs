namespace MartianRoverTests;
public class MarsRover
{
    private Position _position;
    public (int x,int y) Position { get { return (_position.X, _position.Y); } }

    public string Direction { get; private set; }

    public MarsRover((int x, int y)position, string direction)
    {
        _position = new Position(position.x, position.y);
        Direction = direction;
    }

    public void Move(string command)
    {        
        foreach (var unitaryCommand in command)
        {
            MoveFB(unitaryCommand);
            Turn(unitaryCommand);
        }
    }

    private void Turn(char command)
    {
        if (command == 'r')
            Direction = CardinalPointHelper.ToRight(Direction);
        if (command == 'l')
            Direction = CardinalPointHelper.ToLeft(Direction);
    }

    private void MoveFB(char unitaryCommand)
    {
        var directionAndWay = Direction + unitaryCommand;
        _position = _position.NextPosition(directionAndWay); 
    }
}
namespace MartianRoverTests;

public struct Position
{

    public readonly int Y;
    public readonly int X;
    public Position(int X, int Y)
    {
        this.Y = Y;
        this.X = X;
    }
    readonly Dictionary<string, (int xOffset, int yOffset)> movesOffset = new()
    {
        { "Nf", (0, 1) },
        { "Nb", (0, -1) },
        { "Sf", (0, -1) },
        { "Sb", (0, 1) },
        { "Ef", (1, 0) },
        { "Eb", (-1, 0) },
        { "Of", (-1, 0) },
        { "Ob", (1, 0) }
    };
    public Position NextPosition(string directionAndWay)
    {
        if (!movesOffset.ContainsKey(directionAndWay))
            return this;


        return new Position(this.X + movesOffset[directionAndWay].xOffset,
                            this.Y + movesOffset[directionAndWay].yOffset);
    }
}


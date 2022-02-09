namespace MartianRover.Application
{
    public interface ICommandValidator
    {
        bool Validate(string command);
    }
}

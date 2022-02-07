namespace BlazorMartianRover.Components
{
    public interface ICommandValidator
    {
        bool Validate(string command);
    }
}

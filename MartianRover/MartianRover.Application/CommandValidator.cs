namespace MartianRover.Application
{
    public class CommandValidator : ICommandValidator
    {
        string _acceptedValues = "fbrl";
        public bool Validate(string command) => command is not null && command.ToCharArray().All(c => _acceptedValues.Contains(c));
    }
}

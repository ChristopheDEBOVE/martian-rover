namespace MartianRover.Application
{
    public static class CommandValidator
    {
        const string _acceptedValues = "fbrl";
        public static bool Validate(string command) => command is not null && command.ToCharArray().All(c => _acceptedValues.Contains(c));
    }
}

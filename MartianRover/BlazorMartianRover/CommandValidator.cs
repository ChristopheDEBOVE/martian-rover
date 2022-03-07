namespace MartianRover.Application
{
    public static class CommandValidator
    {
        const string _acceptedValues = "fbrl";
        public static bool Validate(string command) => !string.IsNullOrWhiteSpace(command) && command.ToCharArray().All(c => _acceptedValues.Contains(c));
    }
}

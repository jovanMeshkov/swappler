namespace Swappler.Utilities
{
    public abstract class RegexPattern
    {
        public const string Email = @".*@.*\..*";
        public const string Username = @"^[a-zA-Z][a-zA-Z0-9]*[!@#$%^&*()_+-/.:]?[a-zA-Z0-9]+$"; // with length limit: @"(?=^.{3,20}$)^[a-zA-Z][a-zA-Z0-9]*[!@#$%^&*()_+-/.:]?[a-zA-Z0-9]+$";
        public const string Password = @".*";
        public const string Phone = @"^[0]?[7][0-9]\/?[0-9]{3}-?[0-9]{3}$";

    }
}
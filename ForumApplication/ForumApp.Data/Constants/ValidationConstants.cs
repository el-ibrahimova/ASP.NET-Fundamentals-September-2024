namespace ForumApp.Infrastructure.Constants
{
    public static class ValidationConstants
    {
        public const int TitleMinLength = 10;
        public const int TitleMaxLength = 50;
        public const int ContentMinLength = 30;
        public const int ContentMaxLength = 1500;

        public const string RequireErrorMessage = "The {0} field is required.";
        public const string StringLengthErrorMessage = "The {0} field must be between {2} and {1} long.";
    }
}

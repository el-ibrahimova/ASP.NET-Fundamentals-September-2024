namespace DeskMarket.Common
{
    public static class EntityConstants
    {
        // Product
        public const int ProductNameMinLength = 2;
        public const int ProductNameMaxLength = 60;
        public const int ProductDescriptionMinLength = 10;
        public const int ProductDescriptionMaxLength = 250;
        public const double ProductPriceMinValue = 1.00;
        public const double ProductPriceMaxValue = 3000.00;
        public const string EntityDateFormat = "dd-MM-yyyy";

        // Category
        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 20;
    }
}

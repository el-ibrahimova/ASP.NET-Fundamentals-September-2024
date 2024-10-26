namespace CinemaApp.Services.Mapping
{
    using AutoMapper;

    public interface IHaveCustomMappings<T>
    {
        void CreateMappings(IProfileExpression configuration);
    }
}

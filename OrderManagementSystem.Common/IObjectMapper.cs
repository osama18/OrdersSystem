namespace OrderManagementSystem.Common.Logging
{
    public interface IObjectMapper
    {
        void Configure();
        TDestination Map<TDestination>(object input);
        TDestination Map<TSource, TDestination>(TSource input);
        TDestination Map<TSource, TDestination>(TSource input, TDestination destination);
    }
}

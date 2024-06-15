namespace PromoCodeFactory.WebHost.Services.Mapping
{
    public interface IMapper // todo stub
    {
        TResult Map<TSource, TResult>(TSource source) where TResult : class;
    }
}
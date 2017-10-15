namespace Core
{
    public interface IMapper
    {
        IMapper Map<TRequested, TActual>();
    }
}

namespace OrleansURLShortener.Interfaces
{
    public interface IUrlShortenerGrain : IGrainWithStringKey
    {
        Task SetUrl(string fullUrl);

        Task<string> GetUrl();
    }
}

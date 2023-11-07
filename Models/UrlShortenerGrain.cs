using Orleans.Runtime;
using OrleansURLShortener.Interfaces;

namespace OrleansURLShortener.Models
{
    public sealed class UrlShortenerGrain : Grain, IUrlShortenerGrain
    {
        private readonly IPersistentState<UrlDetails> _state;

        public UrlShortenerGrain(
            [PersistentState(
            stateName: "url",
            storageName: "urls")]
            IPersistentState<UrlDetails> state) => _state = state;

        public async Task SetUrl(string fullUrl)
        {
            _state.State = new()
            {
                ShortenedRouteSegment = this.GetPrimaryKeyString(),
                FullUrl = fullUrl
            };

            await _state.WriteStateAsync();
        }

        public Task<string> GetUrl() =>
            Task.FromResult(_state.State.FullUrl);
    }

    [GenerateSerializer]
    public record class UrlDetails
    {
        [Id(0)]
        public string FullUrl { get; set; }

        [Id(1)]
        public string ShortenedRouteSegment { get; set; }
    }
}

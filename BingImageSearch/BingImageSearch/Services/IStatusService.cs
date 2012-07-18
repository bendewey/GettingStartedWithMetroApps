namespace BingImageSearch.Services
{
    public interface IStatusService
    {
        bool IsLoading { get; set; }
        string Message { get; set; }
        string TemporaryMessage { get; set; }
        string Title { get; set; }

        void SetNetworkUnavailable();
        void SetBingUnavailable();
    }
}

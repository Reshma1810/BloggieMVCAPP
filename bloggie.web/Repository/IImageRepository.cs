namespace bloggie.web.Repository
{
    public interface IImageRepository
    {
        Task<String> UploadAsync(IFormFile file);
    }
}

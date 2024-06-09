namespace InGreedIoApi.Services;

public interface IUploadService
{
    Task<string> UploadFileAsync(IFormFile file, string fileName);
}


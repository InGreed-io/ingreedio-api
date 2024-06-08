using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
using InGreedIoApi.Configurations;
using Amazon.Runtime;

namespace InGreedIoApi.Services;

public class S3UploadService : IUploadService
{
    private readonly IAmazonS3 _s3Client;
    private readonly S3Settings _s3Settings;
    private readonly string _imagesDir = "images";

    public S3UploadService(IAmazonS3 s3Client, IOptions<S3Settings> s3Settings)
    {
        _s3Client = s3Client;
        _s3Settings = s3Settings.Value;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string fileName)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is empty or null.");
        }

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            var putRequest = new PutObjectRequest
            {
                BucketName = _s3Settings.BucketName,
                Key = $"{_imagesDir}/{fileName}",
                InputStream = stream
            };

            var response = await _s3Client.PutObjectAsync(putRequest);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                string fileUrl = $"http://{_s3Settings.BucketName}.s3.amazonaws.com/{_imagesDir}/{fileName}";
                return fileUrl;
            }
            else
            {
                throw new Exception("Error uploading file.");
            }
        }
    }
}


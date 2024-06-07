using Amazon.S3;
using Amazon.S3.Model;
using Moq;
using InGreedIoApi.Configurations;
using InGreedIoApi.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

public class S3UploadServiceTests
{
    [Trait("Category", "Unit")]
    [Fact]
    public async Task UploadFileAsync_FileUploadedSuccessfully_ReturnsSuccessMessage()
    {
        // Arrange
        var mockS3Client = new Mock<IAmazonS3>();
        var s3Settings = new S3Settings { BucketName = "test-bucket" };
        var s3Service = new S3UploadService(mockS3Client.Object, Options.Create(s3Settings));

        var fileMock = new Mock<IFormFile>();
        var ms = new MemoryStream();
        var writer = new StreamWriter(ms);
        writer.Write("Test file content");
        writer.Flush();
        ms.Position = 0;
        fileMock.Setup(f => f.OpenReadStream()).Returns(ms);
        fileMock.Setup(f => f.FileName).Returns("testfile.txt");
        fileMock.Setup(f => f.Length).Returns(ms.Length);

        mockS3Client.Setup(client => client.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PutObjectResponse { HttpStatusCode = System.Net.HttpStatusCode.OK });

        // Act
        var result = await s3Service.UploadFileAsync(fileMock.Object, "testfileRenamed.txt");

        // Assert
        Assert.Equal($"http://test-bucket.s3.amazonaws.com/images/testfileRenamed.txt", result);
    }
}


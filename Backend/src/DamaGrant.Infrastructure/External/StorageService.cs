namespace DamaGrant.Infrastructure.External;

public interface IStorageService
{
    Task<string> UploadFileAsync(Stream stream, string fileName);
    Task<Stream> DownloadFileAsync(string fileName);
    Task<bool> DeleteFileAsync(string fileName);
    Task<bool> FileExistsAsync(string fileName);
}

public class StorageService : IStorageService
{
    private readonly string _storagePath;
    private readonly long _maxFileSize;
    private readonly string _allowedExtensions;
    private readonly ILogger<StorageService> _logger;

    public StorageService(IConfiguration configuration, ILogger<StorageService> logger)
    {
        var fileUploadSettings = configuration.GetSection("FileUpload");
        _storagePath = fileUploadSettings["StoragePath"] ?? "./uploads";
        _maxFileSize = long.Parse(fileUploadSettings["MaxFileSize"] ?? "52428800");
        _allowedExtensions = fileUploadSettings["AllowedExtensions"] ?? ".pdf,.doc,.docx,.xlsx,.jpg,.png";
        _logger = logger;

        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
        }
    }

    public async Task<string> UploadFileAsync(Stream stream, string fileName)
    {
        ValidateFile(fileName, stream);

        var uploadPath = Path.Combine(_storagePath, fileName);
        var directory = Path.GetDirectoryName(uploadPath);

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using (var fileStream = new FileStream(uploadPath, FileMode.Create))
        {
            await stream.CopyToAsync(fileStream);
        }

        _logger.LogInformation($"File uploaded successfully: {fileName}");
        return uploadPath;
    }

    public async Task<Stream> DownloadFileAsync(string fileName)
    {
        var filePath = Path.Combine(_storagePath, fileName);

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {fileName}");
        }

        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return await Task.FromResult(stream);
    }

    public async Task<bool> DeleteFileAsync(string fileName)
    {
        var filePath = Path.Combine(_storagePath, fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            _logger.LogInformation($"File deleted successfully: {fileName}");
            return await Task.FromResult(true);
        }

        return await Task.FromResult(false);
    }

    public async Task<bool> FileExistsAsync(string fileName)
    {
        var filePath = Path.Combine(_storagePath, fileName);
        return await Task.FromResult(File.Exists(filePath));
    }

    private void ValidateFile(string fileName, Stream stream)
    {
        var extension = Path.GetExtension(fileName);
        if (!_allowedExtensions.Contains(extension, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException($"File extension not allowed: {extension}");
        }

        if (stream.Length > _maxFileSize)
        {
            throw new InvalidOperationException($"File size exceeds maximum allowed size: {_maxFileSize}");
        }
    }
}

using BeautySalon.Common.Dtos;
using BeautySalon.Common.FileStorage.Exceptions;
using BeautySalon.Common.Interfaces;

namespace BeautySalon.RestApi.FileStorage;

public class StorageApp : IMediaService
{
    private readonly IWebHostEnvironment _env;
    private const long MaxFileSize = 5 * 1024 * 1024;
    private readonly IHttpContextAccessor _accessor;

    public StorageApp(IWebHostEnvironment env, IHttpContextAccessor accessor)
    {
        _env = env;
        _accessor = accessor;
    }

    public async Task<MediaDto> SaveMedia(AddMediaDto dto)
    {
        string folderName, extension;

        StopIfFileIsEmpty(dto);
        StopIfExtensionIsInValid(dto, out folderName, out extension);
        StopIfFileSizeIsUnacceptable(dto);

        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", folderName);

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadsFolder, uniqueName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.Media.CopyToAsync(stream);
        }
        var URL = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/uploads/{folderName}/{uniqueName}";

        //var URL = $"/uploads/{folderName}/{uniqueName}";
        return new MediaDto
        {
            ImageName = Path.GetFileNameWithoutExtension(dto.Media.FileName),
            UniqueName = uniqueName,
            Extension = extension,
            URL = URL
        };
    }

    private static void StopIfFileSizeIsUnacceptable(AddMediaDto dto)
    {
        if (dto.Media.Length > MaxFileSize)
            throw new FileSizeNotAcceptableException();
    }

    private static void StopIfFileIsEmpty(AddMediaDto dto)
    {
        if (dto.Media == null || dto.Media.Length == 0)
            throw new FileIsEmptyException();
    }

    private static void StopIfExtensionIsInValid(AddMediaDto dto, out string folderName, out string extension)
    {
        string[] allowedExtensions;
        extension = Path.GetExtension(dto.Media.FileName).ToLowerInvariant();
        if (extension == null)
            throw new InvalidFileTypeException();

        if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif")
        {
            folderName = "images";
            allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        }

        else if (extension == ".pdf")
        {
            folderName = "docs";
            allowedExtensions = new[] { ".pdf" };
        }

        else if (extension == ".mp4" || extension == ".avi" || extension == ".mov" || extension == ".mkv")
        {
            folderName = "videos";
            allowedExtensions = new[] { ".mp4", ".avi", ".mov", ".mkv" };
        }

        else
        {
            throw new InvalidFileTypeException();
        }

        if (!allowedExtensions.Contains(extension))
        {
            throw new InvalidFileTypeException();
        }
    }

    public async Task DeleteMediaByURL(string url)
    {
        var uri = new Uri(url);
        var relativePath = uri.AbsolutePath; 

        var fullPath = Path.Combine(_env.WebRootPath, relativePath.TrimStart('/'));

        if (!File.Exists(fullPath))
            throw new FileDoesNotExistException();

        try
        {
            await Task.Run(() => File.Delete(fullPath));
        }
        catch
        {
            throw new Exception("خطا در حذف فایل");
        }
    }
}

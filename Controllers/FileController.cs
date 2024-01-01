using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using Utopia.ActionFilters;
using Utopia.Models;

namespace Utopia.Controllers;

public class FileController : Controller
{
    private readonly FilePaths _filePaths;
    
    public FileController(IOptions<FilePaths> filePaths)
    {
        _filePaths = filePaths.Value;
    }
    
    [HttpGet("GetAvatar/{id}")]
    public IActionResult GetAvatar(string id)
    {
        var supportedExtensions = new List<string> { ".png", ".jpg", ".jpeg", ".gif" };
        var filePath = $"{_filePaths.Avatars}{id}";

        foreach (var extension in supportedExtensions)
        {
            var fullPath = $"{filePath}{extension}";
            if (System.IO.File.Exists(fullPath))
            {
                return PhysicalFile(fullPath, $"image/{extension.Substring(1)}");
            }
        }

        var defaultImagePath = $"{_filePaths.Avatars}default.jpg";
        if (System.IO.File.Exists(defaultImagePath))
        {
            return PhysicalFile(defaultImagePath, "image/jpeg");
        }

        return StatusCode(404);
    }
    
    //TODO: upload limit
    [HttpPost]
    [CheckLoginStatus]
    public async Task<IActionResult> SaveImage(IFormFile? imageFile, string type = "avatar")
    {
        if (imageFile is not { Length: > 0 })
            return StatusCode(422);
        if (imageFile.Length is < 0 and <= 3145728) return StatusCode(422);
        var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var fileName = "";
        if (type == "avatar")
        {
            fileName = HttpContext.Session.GetString("UserId") + fileExtension;

        }
        else
        {
             fileName = HttpContext.Session.GetString("UserId") + "b" + fileExtension;

        }

        if (!allowedExtensions.Contains(fileExtension) || !imageFile.ContentType.StartsWith("image/"))
            return StatusCode(422);

        var path = Path.Combine(_filePaths.Avatars, fileName);

        using var image = Image.Load(imageFile.OpenReadStream());
        var originalWidth = image.Width;
        var originalHeight = image.Height;
        var offsetX = 0;
        var offsetY = 0;

        if (type == "avatar")
        { 
            var targetSize = Math.Min(originalWidth, originalHeight);
            
            offsetX = (originalWidth - targetSize) / 2;
            offsetY = (originalHeight - targetSize) / 2;
           
           image.Mutate(x => x.Crop(new Rectangle(offsetX, offsetY, targetSize, targetSize)));
           
           var files = Directory.GetFiles(_filePaths.Avatars, HttpContext.Session.GetString("UserId") + ".*");
           foreach (var file in files)
               System.IO.File.Delete(file);

        }
        else
        {
            const double targetRatio = 19.0 / 9.0;

            var currentRatio = (double)originalWidth / originalHeight;

            int targetWidth;
            int targetHeight;

            if (currentRatio > targetRatio)
            {
                targetWidth = (int)(originalHeight * targetRatio);
                targetHeight = originalHeight;
                offsetX = (originalWidth - targetWidth) / 2;
            }
            else
            {
                targetWidth = originalWidth;
                targetHeight = (int)(originalWidth / targetRatio);
                offsetY = (originalHeight - targetHeight) / 2;
            }
            
            var files = Directory.GetFiles(_filePaths.Avatars, HttpContext.Session.GetString("UserId") + "b.*");
            foreach (var file in files)
                System.IO.File.Delete(file);
            image.Mutate(x => x.Crop(new Rectangle(offsetX, offsetY, targetWidth, targetHeight)));
        }
        
        await using (var stream = new FileStream(path, FileMode.Create))
        {
            if (fileExtension == ".gif")
            {
                await image.SaveAsync(stream, new GifEncoder());

            }
            else
            {
                await image.SaveAsync(stream, new JpegEncoder());

            }
        }
        return StatusCode(200);
    }

    // [HttpGet("GetReplay/{id}")]
    // public IActionResult GetReplay(int id)
    // {
    //     var filePath = $"{_filePaths.Replays}{id}.osr";
    //
    //     if (!System.IO.File.Exists(filePath)) return StatusCode(404);
    //     
    //     return PhysicalFile(filePath, "application/x-osu-replay", $"{id}.osr");
    // }
}
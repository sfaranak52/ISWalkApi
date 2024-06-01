using ISWalksApi.Interfaces;
using ISWalksApi.Models.Domain;
using NZWalksApi.Data;

namespace ISWalksApi.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISWalksDbContext _iSWalksDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment
            , IHttpContextAccessor httpContextAccessor
            , ISWalksDbContext iSWalksDbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _iSWalksDbContext = iSWalksDbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilaPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images"
                ,$"{image.FileName}{image.FileExtension}");

            //Upload image in local path
            using var stream = new FileStream(localFilaPath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlfilaPath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.Path}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlfilaPath;

            //Add image to Image table
            await _iSWalksDbContext.Images.AddAsync(image);
            await _iSWalksDbContext.SaveChangesAsync();

            return image;
        }
    }
}

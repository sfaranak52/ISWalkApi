using ISWalksApi.Models.Domain;
using System.Net;

namespace ISWalksApi.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}

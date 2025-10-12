using BeautySalon.Common.Dtos;

namespace BeautySalon.Common.Interfaces;
public interface IMediaService : IService
{
    Task<MediaDto> SaveMedia(AddMediaDto dto);
    Task DeleteMediaByURL(string url);
}

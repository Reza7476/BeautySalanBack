using Microsoft.AspNetCore.Http;

namespace BeautySalon.Application.Banners.Contracts.Dtos;
public class UpdateBannerHandlerDto
{
    public required string Title { get; set; }
    public required IFormFile Image { get; set; }
}

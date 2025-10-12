using Microsoft.AspNetCore.Http;

namespace BeautySalon.Application.Banners.Contracts.Dtos;
public class AddBannerHandlerDto
{
    public required string Title { get; set; }
    public required IFormFile Image { get; set; }
}

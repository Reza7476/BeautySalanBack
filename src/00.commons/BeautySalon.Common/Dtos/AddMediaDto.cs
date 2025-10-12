using Microsoft.AspNetCore.Http;

namespace BeautySalon.Common.Dtos;
public class AddMediaDto
{
    public IFormFile? Media { get; set; }

}

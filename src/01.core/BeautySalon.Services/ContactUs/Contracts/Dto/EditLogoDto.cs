using Microsoft.AspNetCore.Http;

namespace BeautySalon.Services.ContactUs.Contracts.Dto;
public class EditLogoDto
{
    public IFormFile?     Media { get; set; }

}

using BeautySalon.Application.ContactUs.Contacts.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Services.ContactUs.Contracts.Dto;

namespace BeautySalon.Application.ContactUs.Contacts;
public interface ContactUsHandler : IScope
{
    Task<long> Add(AddAboutUsHandlerDto dto);
    Task EditLogo(long id, EditLogoDto dto);
}

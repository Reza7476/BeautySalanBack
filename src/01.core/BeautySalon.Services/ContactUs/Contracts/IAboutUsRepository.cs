using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.ContactUs;
using BeautySalon.Services.ContactUs.Contracts.Dto;

namespace BeautySalon.Services.ContactUs.Contracts;
public interface IAboutUsRepository : IRepository
{
    Task Add(AboutUs contactUs);
    Task<AboutUs?> FindById(long id);
    Task<GetAboutUsDto?> Get();
    Task<GetAboutUsDto?> GetById(long id);
}

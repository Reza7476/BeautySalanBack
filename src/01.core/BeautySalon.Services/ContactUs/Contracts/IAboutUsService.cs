using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Services.ContactUs.Contracts.Dto;

namespace BeautySalon.Services.ContactUs.Contracts;
public interface IAboutUsService : IService
{
    Task<long> Add(AddAboutUsDto dto);
    Task<GetAboutUsDto?> Get();
    Task<GetAboutUsDto?> GetById(long id);
    Task Update(long id, UpdateAboutUsDto dto);
    Task UpdateLogo(long id, ImageDetailsDto dto);
}

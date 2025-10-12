using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Commons;
using BeautySalon.Entities.ContactUs;
using BeautySalon.Services.ContactUs.Contracts;
using BeautySalon.Services.ContactUs.Contracts.Dto;
using BeautySalon.Services.ContactUs.Exceptions;

namespace BeautySalon.Services.ContactUs;
public class AboutUsAppService : IAboutUsService
{
    private readonly IAboutUsRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public AboutUsAppService(
        IAboutUsRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> Add(AddAboutUsDto dto)
    {
        var contactUs = new AboutUs()
        {
            MobileNumber = dto.MobileNumber,
            Address = dto.Address,
            CreateDate = DateTime.UtcNow,
            Description = dto.Description,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Telephone = dto.Telephone,
            Email = dto.Email,
            Instagram = dto.Instagram,
            LogoImage = dto.LogoDetails != null ? new MediaDocument()
            {
                Extension = dto.LogoDetails.Extension,
                ImageName = dto.LogoDetails.ImageName,
                UniqueName = dto.LogoDetails.UniqueName,
                URL = dto.LogoDetails.URL
            } : null
        };

        await _repository.Add(contactUs);
        await _unitOfWork.Complete();

        return contactUs.Id;
    }

    public async Task<GetAboutUsDto?> Get()
    {
        return await _repository.Get();
    }

    public async Task<GetAboutUsDto?> GetById(long id)
    {
        return await _repository.GetById(id);
    }

    public async Task Update(long id, UpdateAboutUsDto dto)
    {
        var aboutUs = await _repository.FindById(id);
        StopIfAboutUsNotFound(aboutUs);

        aboutUs!.Telephone = dto.Telephone;
        aboutUs.Longitude = dto.Longitude;
        aboutUs.Latitude = dto.Latitude;
        aboutUs.MobileNumber = dto.MobileNumber;
        aboutUs.Address = dto.Address;
        aboutUs.Description = dto.Description;
        aboutUs.Email = dto.Email;
        aboutUs.Instagram = dto.Instagram;

        await _unitOfWork.Complete();
    }

    private static void StopIfAboutUsNotFound(AboutUs? aboutUs)
    {
        if (aboutUs == null)
        {
            throw new AboutUsNotFoundException();
        }
    }

    public async Task UpdateLogo(long id, ImageDetailsDto dto)
    {
        var aboutUs = await _repository.FindById(id);

        StopIfAboutUsNotFound(aboutUs);

        if (aboutUs!.LogoImage == null)
        {
            aboutUs.LogoImage = new MediaDocument()
            {
                Extension = dto.Extension,
                ImageName = dto.ImageName,
                UniqueName = dto.UniqueName,
                URL = dto.URL
            };
        }
        else
        {
            aboutUs.LogoImage.UniqueName = dto.UniqueName;
            aboutUs.LogoImage.ImageName = dto.ImageName;
            aboutUs.LogoImage.URL = dto.URL;
            aboutUs.LogoImage.Extension = dto.Extension;

        }
        await _unitOfWork.Complete();
    }
}

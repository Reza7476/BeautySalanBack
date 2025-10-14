using BeautySalon.Application.Banners.Contracts;
using BeautySalon.Application.Banners.Contracts.Dtos;
using BeautySalon.Services.Banners.Contracts;
using BeautySalon.Services.Banners.Contracts.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.RestApi.Controllers.Banners;
[Route("api/banners")]
[ApiController]
public class BannersController : ControllerBase
{
    private readonly IBannerService _service;
    private readonly BannerHandler _bannerHandler;

    public BannersController(
        IBannerService service,
        BannerHandler bannerHandler)
    {
        _service = service;
        _bannerHandler = bannerHandler;
    }

    [HttpPost("add")]
    public async Task<long> Add([FromForm] AddBannerHandlerDto dto)
    {
        return await _bannerHandler.Add(dto);
    }

    [Authorize]
    [HttpGet]
    public async Task<GetBannerDto?> GetBanner()
    {
        return await _service.Get();
    }

    [HttpPatch("{id}")]
    public async Task UpdateT(long id,[FromForm] UpdateBannerHandlerDto dto)
    {
        await _bannerHandler.UpdateBanner(id, dto);
    }

}



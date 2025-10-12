using BeautySalon.Application.ContactUs.Contacts;
using BeautySalon.Application.ContactUs.Contacts.Dtos;
using BeautySalon.Services.ContactUs.Contracts;
using BeautySalon.Services.ContactUs.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.RestApi.Controllers.ContactUs;
[Route("api/contact-us")]
[ApiController]
public class ContactUsController : ControllerBase
{
    private readonly IAboutUsService _service;
    private readonly ContactUsHandler _handler;


    public ContactUsController(IAboutUsService service, ContactUsHandler handler)
    {
        _service = service;
        _handler = handler;
    }

    [HttpPost("add")]
    public async Task<long> Add([FromForm] AddAboutUsHandlerDto dto)
    {
        return await _handler.Add(dto);
    }

    [HttpGet]
    public async Task<GetAboutUsDto?> Get()
    {
        return await _service.Get();
    }

    [HttpPut("{id}")]
    public async Task Update([FromRoute] long id, [FromBody] UpdateAboutUsDto dto )
    {
        await _service.Update(id ,dto);
    }

    [HttpGet("{id}")]
    public async Task<GetAboutUsDto?> GetById([FromRoute]long id)
    {
        return await _service.GetById(id);
    }

    [HttpPatch("{id}/logo")]
    public async Task EditLogo([FromRoute] long id, [FromForm]EditLogoDto dto)
    {
        await _handler.EditLogo(id, dto);
    }

}

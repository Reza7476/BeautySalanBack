using BeautySalon.Application.WhyUsSections.Contracts.Dto;
using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;

namespace BeautySalon.Application.WhyUsSections.Contracts;
public interface IWhyUsSectionHandler : IScope
{
    Task<long> Add(AddWhyUsSectionHandlerDto dto);
    Task UpdateImage(long id, AddMediaDto dto);
}

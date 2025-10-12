using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.WhyUsSections;
using BeautySalon.Services.WhyUsSections.Contracts.Dto;

namespace BeautySalon.Services.WhyUsSections.Contracts;
public interface IWhyUsSectionService : IService
{
    Task<long> Add(AddWhyUsSectionDto dto);
    Task<long> AddQuestion(AddWhyUsQuestionDto dto, long sectionId);
    Task<GetWhyUsSectionDto?> GetWhyUsSection();
    Task<Why_Us_Section?> GetById(long id);
    Task<List<GetWhyUsQuestionsDto>> GetQuestionsBySectionId(long sectionId);
    Task UpdateQuestion(long questionId, UpdateWhyUsQuestionDto dto);
    Task UpdateWhyUsSection(long id, EditTitleAndDescriptionWhyUsSectionDto dto);
    Task DeleteQuestion(long questionId);
    Task<GetWhyUsSectionForEditDto?> GetWhyUsSectionByIdForEdit(long id);
    Task UpdateImage(long id, ImageDetailsDto imageDetailsDto);
    Task<GetWhyUsForLandingDto?> GetForLanding();
}

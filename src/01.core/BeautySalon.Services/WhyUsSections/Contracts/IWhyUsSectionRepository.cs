using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.WhyUsSections;
using BeautySalon.Services.WhyUsSections.Contracts.Dto;

namespace BeautySalon.Services.WhyUsSections.Contracts;
public interface IWhyUsSectionRepository : IRepository
{
    Task Add(Why_Us_Section whyUsSection);
    Task<Why_Us_Section?> FindById(long sectionId);
    Task<Why_Us_Question?> FindWhyUsQuestionById(long questionId);
    Task<GetWhyUsSectionDto?> GetWhyUsSection();
    Task<List<GetWhyUsQuestionsDto>> GetQuestionsBySectionId(long sectionId);
    Task AddQuestion(Why_Us_Question question);
    Task DeleteQuestion(Why_Us_Question question);
    Task<GetWhyUsSectionForEditDto?> GetByIdForEdit(long id);
    Task<GetWhyUsForLandingDto?> GetForLanding();
}

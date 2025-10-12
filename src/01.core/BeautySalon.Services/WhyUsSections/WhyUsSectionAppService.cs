using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Commons;
using BeautySalon.Entities.WhyUsSections;
using BeautySalon.Services.WhyUsSections.Contracts;
using BeautySalon.Services.WhyUsSections.Contracts.Dto;
using BeautySalon.Services.WhyUsSections.Exceptions;

namespace BeautySalon.Services.WhyUsSections;
public class WhyUsSectionAppService : IWhyUsSectionService
{
    private readonly IWhyUsSectionRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public WhyUsSectionAppService(
        IWhyUsSectionRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> Add(AddWhyUsSectionDto dto)
    {
        var whyUsSection = new Why_Us_Section()
        {
            CreateDate = DateTime.UtcNow,
            Title = dto.Title,
            Description=dto.Description,
            Image = new MediaDocument()
            {
                Extension = dto.Media.Extension,
                URL = dto.Media.URL,
                ImageName = dto.Media.ImageName,
                UniqueName = dto.Media.UniqueName
            }
        };

        await _repository.Add(whyUsSection);
        await _unitOfWork.Complete();

        return whyUsSection.Id;
    }

    public async Task<long> AddQuestion(AddWhyUsQuestionDto dto, long sectionId)
    {
        var section = await _repository.FindById(sectionId);
        StopIfWhyUsSectionNotFound(section);
        var question = new Why_Us_Question()
        {
            Question = dto.Question,
            Answer = dto.Answer,
            CreateDate = DateTime.UtcNow,
            SectionId= sectionId
        };
        await _repository.AddQuestion(question);
        await _unitOfWork.Complete();
        return question.Id;       
    }

    private static void StopIfWhyUsSectionNotFound(Why_Us_Section? section)
    {
        if (section == null)
        {
            throw new WhyUsSectionNotFoundException();
        }
    }

    public async Task<List<GetWhyUsQuestionsDto>> GetQuestionsBySectionId(long sectionId)
    {
        return await _repository.GetQuestionsBySectionId(sectionId);
    }

    public async Task<GetWhyUsSectionDto?> GetWhyUsSection()
    {
        return await _repository.GetWhyUsSection();
    }

    public async Task UpdateQuestion(long questionId, UpdateWhyUsQuestionDto dto)
    {
        var question = await _repository.FindWhyUsQuestionById(questionId);

        StopIfWhyUsQuestionNotFound(question);

        question!.Question = dto.Question;
        question.Answer = dto.Answer;
        await _unitOfWork.Complete();
    }

    private static void StopIfWhyUsQuestionNotFound(Why_Us_Question? question)
    {
        if (question == null)
        {
            throw new WhyUsQuestionNotFoundException();
        }
    }

    public async Task<Why_Us_Section?> GetById(long id)
    {
        return await _repository.FindById(id);
    }

    public async Task UpdateWhyUsSection(long id, EditTitleAndDescriptionWhyUsSectionDto dto)
    {
        var section = await _repository.FindById(id);

        StopIfSectionNotFound(section);
        section!.Title = dto.Title;
        section.Description = dto.Description;
        await _unitOfWork.Complete();
    }

    private static void StopIfSectionNotFound(Why_Us_Section? section)
    {
        if (section == null)
        {
            throw new WhyUsSectionNotFoundException();
        }
    }

    public async Task DeleteQuestion(long questionId)
    {
        var question = await _repository.FindWhyUsQuestionById(questionId);
        StopIfWhyUsQuestionNotFound(question);

        await _repository.DeleteQuestion(question!);
        await _unitOfWork.Complete();
    }

    public async Task<GetWhyUsSectionForEditDto?> GetWhyUsSectionByIdForEdit(long id)
    {
        return await _repository.GetByIdForEdit(id);
    }

    public async Task UpdateImage(long id, ImageDetailsDto dto)
    {
        var whyUs=await _repository.FindById(id);

        StopIfSectionNotFound(whyUs);

        whyUs!.Image.UniqueName = dto.UniqueName;
        whyUs.Image.ImageName = dto.ImageName;
        whyUs.Image.URL = dto.URL;
        whyUs.Image.Extension = dto.Extension;
        await _unitOfWork.Complete();
    }

    public async Task<GetWhyUsForLandingDto?> GetForLanding()
    {

        return await _repository.GetForLanding();
    }
}

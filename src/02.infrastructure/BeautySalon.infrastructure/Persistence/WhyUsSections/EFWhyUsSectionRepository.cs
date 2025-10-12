using BeautySalon.Common.Dtos;
using BeautySalon.Entities.WhyUsSections;
using BeautySalon.Services.WhyUsSections.Contracts;
using BeautySalon.Services.WhyUsSections.Contracts.Dto;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.WhyUsSections;
public class EFWhyUsSectionRepository : IWhyUsSectionRepository
{

    private readonly DbSet<Why_Us_Section> _sections;
    private readonly DbSet<Why_Us_Question> _questions;

    public EFWhyUsSectionRepository(EFDataContext context)
    {
        _sections = context.Set<Why_Us_Section>();
        _questions = context.Set<Why_Us_Question>();
    }

    public async Task Add(Why_Us_Section whyUsSection)
    {
        await _sections.AddAsync(whyUsSection);
    }

    public async Task<Why_Us_Section?> FindById(long sectionId)
    {
        return await _sections.Where(_ => _.Id == sectionId).FirstOrDefaultAsync();
    }

    public async Task<Why_Us_Question?> FindWhyUsQuestionById(long questionId)
    {
        return await _questions.FirstOrDefaultAsync(_ => _.Id == questionId);
    }

    public async Task<GetWhyUsSectionDto?> GetWhyUsSection()
    {
        return await _sections.Include(_ => _.Why_Us_Questions).Select(section => new GetWhyUsSectionDto()
        {
            Id = section.Id,
            Title = section.Title,
            Description = section.Description,
            Image = new ImageDetailsDto()
            {
                Extension = section.Image.Extension,
                ImageName = section.Image.ImageName,
                UniqueName = section.Image.UniqueName,
                URL = section.Image.URL,
            },
            Questions = section.Why_Us_Questions.Select(question => new GetWhyUsQuestionsDto
            {
                Answer = question.Answer,
                Question = question.Question,
                Id = question.Id
            }).ToList()
        }).FirstOrDefaultAsync();
    }

    public async Task<List<GetWhyUsQuestionsDto>> GetQuestionsBySectionId(long sectionId)
    {
        return await _questions
            .Where(_ => _.SectionId == sectionId)
            .Select(_ => new GetWhyUsQuestionsDto()
            {
                Answer = _.Answer,
                Question = _.Question,
                Id = _.Id
            }).ToListAsync();
    }

    public async Task AddQuestion(Why_Us_Question question)
    {
        await _questions.AddAsync(question);
    }

    public async Task DeleteQuestion(Why_Us_Question question)
    {
        _questions.Remove(question);
        await Task.CompletedTask;
    }

    public async Task<GetWhyUsSectionForEditDto?> GetByIdForEdit(long id)
    {
        return await _sections.Where(_ => _.Id == id).Select(_ => new GetWhyUsSectionForEditDto()
        {
            Description = _.Description,
            Image = new ImageDetailsDto()
            {
                Extension = _.Image.Extension,
                ImageName = _.Image.ImageName,
                UniqueName = _.Image.UniqueName,
                URL = _.Image.URL
            },
            Title = _.Title,
        }).FirstOrDefaultAsync();
    }

    public async Task<GetWhyUsForLandingDto?> GetForLanding()
    {
        var a = await _sections
            .Include(_ => _.Why_Us_Questions)
            .Select(whyUs => new GetWhyUsForLandingDto()
            {
                Description = whyUs.Description,
                Title = whyUs.Title,
                Image = new ImageDetailsDto()
                {
                    Extension = whyUs.Image.Extension,
                    ImageName = whyUs.Image.ImageName,
                    UniqueName = whyUs.Image.UniqueName,
                    URL = whyUs.Image.URL
                },
                Questions = whyUs.Why_Us_Questions.Select(question => new GetWhyUsQuestionsDto()
                {
                    Id = question.Id,
                    Answer = question.Answer,
                    Question = question.Question
                }).ToList()
            }).FirstOrDefaultAsync();
        return a;
    }
}

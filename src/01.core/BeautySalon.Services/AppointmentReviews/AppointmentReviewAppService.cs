using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.AppointmentReviews;
using BeautySalon.Services.AppointmentReviews.Contracts;
using BeautySalon.Services.AppointmentReviews.Contracts.Dtos;
using BeautySalon.Services.AppointmentReviews.Exceptions;
using BeautySalon.Services.Appointments.Exceptions;

namespace BeautySalon.Services.AppointmentReviews;
public class AppointmentReviewAppService : IAppointmentReviewService
{

    private readonly IAppointmentReviewRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeService _dateTimeService;


    public AppointmentReviewAppService(
        IAppointmentReviewRepository repository,
        IUnitOfWork unitOfWork,
        IDateTimeService dateTimeService)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _dateTimeService = dateTimeService;
    }

    public async Task<string> Add(AddAppointmentReviewDto dto)
    {
        var appointment = await _repository.FindAppointmentById(dto.AppointmentId);

        if (appointment == null)
        {
            throw new AppointmentNotFoundException();
        }
        if (dto.Rate < 0 || dto.Rate > 5)
        {
            throw new RateOutOfRangeException();
        }

        var review = new AppointmentReview()
        {
            Id = Guid.NewGuid().ToString(),
            AppointmentId = dto.AppointmentId,
            ClientId = appointment.ClientId,
            CreatedAt = _dateTimeService.Now,
            Description = dto.Description,
            IsPublished = false,
            Rate = dto.Rate,
            TechnicianId = appointment.TechnicianId,
            TreatmentId = appointment.TreatmentId,
        };

        await _repository.Add(review);
        await _unitOfWork.Complete();
        return review.Id;

    }

    public async Task ChangePublishStatus(ChangeReviewPublishStatusDto dto)
    {
        var review = await _repository.FindById(dto.Id);
        if(review == null)
        {
            throw new AppointmentReviewNotFoundException();
        }

        review.IsPublished = dto.PublishStatus;
        await _unitOfWork.Complete();

    }

    public async Task<IPageResult<GetAllReviewsDto>>
        GetAllCommentsForAdmin(IPagination? pagination = null)
    {
        return await _repository.GetAllCommentsForAdmin(pagination);
    }
}

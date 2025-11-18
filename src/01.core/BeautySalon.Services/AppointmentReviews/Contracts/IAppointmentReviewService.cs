using BeautySalon.Common.Interfaces;
using BeautySalon.Services.AppointmentReviews.Contracts.Dtos;

namespace BeautySalon.Services.AppointmentReviews.Contracts;
public interface IAppointmentReviewService : IService
{
    Task<string> Add(AddAppointmentReviewDto dto);
}

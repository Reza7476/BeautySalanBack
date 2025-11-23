using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.AppointmentReviews;
using BeautySalon.Entities.Appointments;
using BeautySalon.Services.AppointmentReviews.Contracts.Dtos;

namespace BeautySalon.Services.AppointmentReviews.Contracts;
public interface IAppointmentReviewRepository : IRepository
{
    Task Add(AppointmentReview review);
    Task<Appointment?> FindAppointmentById(string appointmentId);
    Task<AppointmentReview?> FindById(string id);
    
    Task<IPageResult<GetAllReviewsDto>> 
        GetAllCommentsForAdmin(IPagination? pagination = null);
    
    Task<IPageResult<GetAllPublishedReviewsDto>>
        GetAllPublishedForLanding(IPagination? pagination=null);
}

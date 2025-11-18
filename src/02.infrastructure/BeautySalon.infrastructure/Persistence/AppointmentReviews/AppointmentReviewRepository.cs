using BeautySalon.Entities.AppointmentReviews;
using BeautySalon.Entities.Appointments;
using BeautySalon.Services.AppointmentReviews.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.AppointmentReviews;
public class AppointmentReviewRepository : IAppointmentReviewRepository
{
    private readonly DbSet<Appointment> _appointments;

    private readonly DbSet<AppointmentReview> _appointmentsReviews;

    public AppointmentReviewRepository(EFDataContext context)
    {
        _appointments = context.Set<Appointment>();
        _appointmentsReviews=context.Set<AppointmentReview>();  
    }

    public async Task Add(AppointmentReview review)
    {
        await _appointmentsReviews.AddAsync(review);    
    }

    public async Task<Appointment?> FindAppointmentById(string appointmentId)
    {
        return await _appointments.FindAsync(appointmentId);
    }
}

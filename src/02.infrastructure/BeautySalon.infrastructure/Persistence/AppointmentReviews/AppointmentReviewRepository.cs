using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.AppointmentReviews;
using BeautySalon.Entities.Appointments;
using BeautySalon.Entities.Clients;
using BeautySalon.Entities.Treatments;
using BeautySalon.Entities.Users;
using BeautySalon.infrastructure.Persistence.Extensions.Paginations;
using BeautySalon.Services.AppointmentReviews.Contracts;
using BeautySalon.Services.AppointmentReviews.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.AppointmentReviews;
public class AppointmentReviewRepository : IAppointmentReviewRepository
{
    private readonly DbSet<Appointment> _appointments;

    private readonly DbSet<AppointmentReview> _appointmentsReviews;

    private readonly DbSet<Treatment> _treatments;

    private readonly DbSet<Client> _clients;
    private readonly DbSet<User> _users;

    public AppointmentReviewRepository(EFDataContext context)
    {
        _appointments = context.Set<Appointment>();
        _appointmentsReviews = context.Set<AppointmentReview>();
        _treatments = context.Set<Treatment>();
        _clients = context.Set<Client>();
        _users = context.Set<User>();
    }

    public async Task Add(AppointmentReview review)
    {
        await _appointmentsReviews.AddAsync(review);
    }

    public async Task<Appointment?> FindAppointmentById(string appointmentId)
    {
        return await _appointments.FindAsync(appointmentId);
    }

    public async Task<IPageResult<GetAllReviewsDto>> 
        GetAllCommentsForAdmin(IPagination? pagination = null)
    {
        var query = (from review in _appointmentsReviews
                     join treatment in _treatments on review.TreatmentId equals treatment.Id
                     join client in _clients on review.ClientId equals client.Id
                     join user in _users on client.UserId equals user.Id
                     select new GetAllReviewsDto
                     {
                         TreatmentTitle = treatment.Title,
                         Comment = review.Description,
                         IsPublished = review.IsPublished,
                         LastName = user.LastName,
                         Name = user.Name,
                         Rate = review.Rate,
                         CreatedAt = review.CreatedAt
                     }).AsQueryable();


        query = query.OrderByDescending(_ => _.CreatedAt);

        return await query.Paginate(pagination ?? new Pagination());
    }
}

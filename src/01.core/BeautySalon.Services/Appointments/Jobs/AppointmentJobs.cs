using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Appointments;
using BeautySalon.Services.Appointments.Contracts;

namespace BeautySalon.Services.Appointments.Jobs;
public class AppointmentJobs
{
    private readonly IAppointmentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public AppointmentJobs(
        IAppointmentRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }


    public async Task ChangeOverdueStatusByHangFire()
    {
        var appointments = await _repository
            .GetOverdueUnfinalizedAppointments();
        if (appointments.Any())
        {
            foreach (var item in appointments)
            {
                item.Status = AppointmentStatus.NoShow;
            }
            await _unitOfWork.Complete();
        }
    }
}

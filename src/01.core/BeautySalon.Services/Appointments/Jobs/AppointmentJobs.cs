using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Appointments;
using BeautySalon.Services.Appointments.Contracts;
using BeautySalon.Utilities;

namespace BeautySalon.Services.Appointments.Jobs;
public class AppointmentJobs
{
    private readonly IAppointmentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly ISMSService _smsService;
    public AppointmentJobs(
        IAppointmentRepository repository,
        IUnitOfWork unitOfWork,
        ISMSService smsService)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _smsService = smsService;
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

    public async Task SendRemindSMSForClients()
    {
        var appointments = await _repository.GetAppointmentRequiringSMS();
        var listIds = new List<string>();
        if (appointments.Any())
        {
            foreach (var item in appointments)
            {
                await _smsService.SendSMS(new SendSMSDto
                {
                    Number = item.ClientNumber!,
                    BodyName = "RemindingAppointment",
                    Args = new List<string>
                    {
                        PersianDateHelper.ToPersianDateString(item.AppointmentData),
                        PersianDateHelper.GetTimeString(item.AppointmentData),
                        item.TreatmentTitle
                    }
                });

                listIds.Add(item.AppointmentId);
            }
        }

        if (listIds.Any())
        {
            var listAppointments = await _repository.GetByIds(listIds);
            foreach (var item in listAppointments)
            {
                item.RemindSMSSent = true;
            }
            await _unitOfWork.Complete();
        }
    }


}

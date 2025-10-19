using BeautySalon.Common.Dtos;

namespace BeautySalon.Common.Interfaces;
public interface ISMSSetting : IScope
{
    SMSInformationDto SMSSettings { get; }
}

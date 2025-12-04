using BeautySalon.Common.Dtos;

namespace BeautySalon.Common.Interfaces;
public interface IFireBaseSettingInfo : IScope
{
    FireBaseSettingInfoDto FireBaseSettingInfo { get; }
}

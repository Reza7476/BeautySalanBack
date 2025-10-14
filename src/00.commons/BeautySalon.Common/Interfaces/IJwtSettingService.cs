using BeautySalon.Common.Dtos;

namespace BeautySalon.Common.Interfaces;
public interface IJwtSettingService : IScope
{
    JwtSettingDto JwtSetting { get; }
}

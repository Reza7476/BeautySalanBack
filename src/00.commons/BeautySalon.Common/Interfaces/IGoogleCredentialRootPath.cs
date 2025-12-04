using BeautySalon.Common.Dtos;

namespace BeautySalon.Common.Interfaces;
public interface IGoogleCredentialRootPath : IScope
{
    GoogleCredentialDto GoogleCredentialPath { get; }
}

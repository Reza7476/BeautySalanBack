using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Users;
using BeautySalon.Services.Users.Contracts;
using BeautySalon.Test.Tool.Infrastructure.UnitTests;
using Moq;

namespace BeautySalon.Service.UnitTest.Users;
public class UserServiceTestMoq : BusinessUnitTest
{
    private readonly IUserService _sut;
    private readonly Mock<IUserRepository> _repository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    public UserServiceTestMoq()
    {
        _repository = new Mock<IUserRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();

        _sut = new UserAppService(_repository.Object, _unitOfWork.Object);

    }


}

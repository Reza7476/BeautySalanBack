namespace BeautySalon.Common.Interfaces;
public interface  IUnitOfWork:IScope
{
    Task Begin();
    Task Commit();
    Task Complete();
    Task RoleBack();

}

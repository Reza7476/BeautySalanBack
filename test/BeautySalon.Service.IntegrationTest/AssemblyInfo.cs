using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCollectionOrderer("Xunit.Sdk.CollectionOrderer", "xunit.core")]

namespace BeautySalon.Service.IntegrationTest;
public class AssemblyInfo
{
}

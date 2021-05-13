using Permitt.Services;
using Shouldly;
using Xunit;

namespace Permitt.Tests
{
    public class PermissionTypeTests
    {
        [Fact]
        public void AllFields()
        {
            PermissionType.Create.ShouldBe("create");
            PermissionType.Read.ShouldBe("read");
            PermissionType.Update.ShouldBe("update");
            PermissionType.Delete.ShouldBe("delete");
        }
    }
}

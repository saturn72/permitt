using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;
using Permitt.Services;

namespace Permitt.Tests
{
    public class DefaultPermissionServiceTests
    {
        #region UserIsPermittedForEntityAsync
        [Theory]
        [MemberData(nameof(UserIsPermittedForEntityAsync_ThrowsOnMissingArguments_DATA))]
        public async Task UserIsPermittedForEntityAsync_ThrowsOnMissingArguments(UserPermittedForEntityRequest req, string msg)
        {
            var srv = new DefaultPermissionService(null);
            var ex = await Should.ThrowAsync<ArgumentNullException>(() => srv.UserIsPermittedForEntityAsync(req));
            ex.Message.ShouldContain(msg);
        }
        public static IEnumerable<object[]> UserIsPermittedForEntityAsync_ThrowsOnMissingArguments_DATA = new[]
        {
            new object[]{ null, "request"},
            new object[]{ new UserPermittedForEntityRequest(), nameof(UserPermittedForEntityRequest.UserId) },
            new object[]{ new UserPermittedForEntityRequest{UserId = "uid"}, nameof(UserPermittedForEntityRequest.EntityName) },
            new object[] { new UserPermittedForEntityRequest { UserId = "uid", EntityName = "en" }, nameof(UserPermittedForEntityRequest.PermissionType) },
            new object[] { new UserPermittedForEntityRequest { UserId = "uid", EntityName = "en", PermissionType = "pt" }, nameof(UserPermittedForEntityRequest.EntityId)},
        };
        [Theory]
        [MemberData(nameof(UserIsPermittedForEntityAsync_NotPermitted_DATA))]
        public async Task UserIsPermittedForEntityAsync_NotPermitted(IEnumerable<PermissionRecord> storeRes)
        {
            var store = new Mock<IPermissionRecordStore>();
            store.Setup(s => s.GetUserPermissionRecordForEntityAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(storeRes);
            var srv = new DefaultPermissionService(store.Object);
            var req = new UserPermittedForEntityRequest { UserId = "uid", EntityName = "en", PermissionType = "pt", EntityId = "eid" };

            var res = await srv.UserIsPermittedForEntityAsync(req);
            res.Permitted.ShouldBeFalse();
        }
        public static IEnumerable<object[]> UserIsPermittedForEntityAsync_NotPermitted_DATA = new[]
        {
            new object[]{ null}, // null result
            new object[]{ Array.Empty<PermissionRecord>()},//empty
            new object[]{ new [] { new PermissionRecord()}}, // not active
        };
        [Fact]
        public async Task UserIsPermittedForEntityAsync_Permitted()
        {
            var store = new Mock<IPermissionRecordStore>();
            store.Setup(s => s.GetUserPermissionRecordForEntityAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new[] { new PermissionRecord { Active = true } });
            var srv = new DefaultPermissionService(store.Object);
            var req = new UserPermittedForEntityRequest { UserId = "uid", EntityName = "en", PermissionType = "pt", EntityId = "eid" };

            var res = await srv.UserIsPermittedForEntityAsync(req);
            res.Permitted.ShouldBeTrue();
        }
        #endregion
        #region UserIsPermittedForAction
        [Theory]
        [MemberData(nameof(UserIsPermittedForAction_ThrowsOnMissingArguments_DATA))]
        public async Task UserIsPermittedForAction_ThrowsOnMissingArguments(UserPermittedForActionRequest req, string msg)
        {
            var srv = new DefaultPermissionService(null);
            var ex = await Should.ThrowAsync<ArgumentNullException>(() => srv.UserIsPermittedForAction(req));
            ex.Message.ShouldContain(msg);
        }
        public static IEnumerable<object[]> UserIsPermittedForAction_ThrowsOnMissingArguments_DATA = new[]
        {
            new object[] { null, "request"},
            new object[] { new UserPermittedForActionRequest(), nameof(UserPermittedForEntityRequest.UserId) },
            new object[] { new UserPermittedForActionRequest {UserId = "uid"}, nameof(UserPermittedForEntityRequest.EntityName) },
            new object[] { new UserPermittedForActionRequest { UserId = "uid", EntityName = "en" }, nameof(UserPermittedForEntityRequest.PermissionType) },
        };
        [Theory]
        [MemberData(nameof(UserIsPermittedForActionAsync_NotPermitted_DATA))]
        public async Task UserIsPermittedForActionAsync_NotPermitted(IEnumerable<PermissionRecord> storeRes)
        {
            var store = new Mock<IPermissionRecordStore>();
            store.Setup(s => s.GetUserPermissionRecordForActionAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(storeRes);
            var srv = new DefaultPermissionService(store.Object);
            var req = new UserPermittedForEntityRequest { UserId = "uid", EntityName = "en", PermissionType = "pt", EntityId = "eid" };

            var res = await srv.UserIsPermittedForEntityAsync(req);
            res.Permitted.ShouldBeFalse();
        }
        public static IEnumerable<object[]> UserIsPermittedForActionAsync_NotPermitted_DATA = new[]
        {
            new object[]{ null}, // null result
            new object[]{ Array.Empty<PermissionRecord>()},//empty
            new object[]{ new [] { new PermissionRecord()}}, // not active
        };
        #endregion
    }
}

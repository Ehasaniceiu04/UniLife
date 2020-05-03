using Semerkand.Server.Managers;
using Semerkand.Shared.DataModels;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace Semerkand.Server.Tests.Managers
{
    [TestFixture]
    class UserProfileMangerTests
    {
        private UserProfileManager _userProfileManager;

        private Mock<IUserProfileStore> _userProfileStore;
        private Mock<IHttpContextAccessor> _httpContextAccessor;

        [SetUp]
        public void SetUp()
        {
            _userProfileStore = new Mock<IUserProfileStore>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();

            _userProfileManager = new UserProfileManager(_userProfileStore.Object, _httpContextAccessor.Object);
        }

        [Test]
        public void SetupWorked()
        {
            Assert.Pass();
        }
    }
}

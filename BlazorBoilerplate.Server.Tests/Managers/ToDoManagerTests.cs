using Semerkand.Server.Managers;
using Semerkand.Shared.DataInterfaces;
using Moq;
using NUnit.Framework;

namespace Semerkand.Server.Tests.Managers
{
    [TestFixture]
    class ToDoManagerTests
    {
        private ToDoManager _toDoManager;

        private Mock<IToDoStore> _toDoStore;

        [SetUp]
        public void SetUp()
        {
            _toDoStore = new Mock<IToDoStore>();

            _toDoManager = new ToDoManager(_toDoStore.Object);
        }

        [Test]
        public void SetupWorked()
        {
            Assert.Pass();
        }
    }
}

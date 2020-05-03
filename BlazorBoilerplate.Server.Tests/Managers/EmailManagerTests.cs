using Semerkand.Server.Managers;
using Semerkand.Shared.DataModels;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semerkand.Server.Tests.Managers
{
    [TestFixture]
    class EmailManagerTests
    {
        private EmailManager _emailManager;
        private Mock<IEmailConfiguration> _emailConfiguration;
        private Mock<ILogger<EmailManager>> _logger;

        [SetUp]
        public void SetUp()
        {
            _emailConfiguration = new Mock<IEmailConfiguration>();
            _logger = new Mock<ILogger<EmailManager>>();

            _emailManager = new EmailManager(_emailConfiguration.Object, _logger.Object);
        }

        [Test]
        public void SetupWorked()
        {
            Assert.Pass();
        }
    }
}
